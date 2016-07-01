using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using IntraVision.Data;
using IntraVision.Repository;
using IntraVision.Web.Mvc.Autofac;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc.Services
{
    public class BaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> : IBaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions>
        where TEntity : class, IEntityBase, new()
        where TEntityCreate : class, new()
        where TEntityEdit : class, IEntityBase, new()
        where TEntityGridOptions : GridOptions
        where TEntityGrid : class, IGridModel<TEntity>
    {
        public Lazy<IRepository<TEntity>> _repository;

        public BaseService(Lazy<IRepository<TEntity>> repository)
        {
            _repository = repository;
        }

        public virtual IList<TEntity> GetList()
        {
            return _repository.Value.GetQuery().ToList();
        }

        public virtual TEntity Get(int id)
        {
            return _repository.Value.Single(e => e.Id == id);
        }

        public virtual void Create(TEntityCreate createView, IPrincipal principal)
        {
            var create = Mapper.Map<TEntityCreate, TEntity>(createView);

            foreach (var propertyInfo in createView.GetType().GetProperties().Where(p => p.PropertyType == typeof(HttpPostedFileBase)))
            {
                var fileBase = propertyInfo.GetValue(createView);

                if (fileBase != null)
                {
                    var file = fileBase as HttpPostedFileBase;

                    if (file != null)
                    {
                        var imageProperties = propertyInfo.GetCustomAttributes(false).OfType<ImageAttribute>();

                        if (imageProperties.Any())
                        {
                            foreach (var ia in imageProperties)
                            {
                                var targetProperty = create.GetType().GetProperty(ia.PropertyName);

                                if (targetProperty != null)
                                {
                                    var imageMS = new MemoryStream();

                                    #region Scale
                                    
                                    if (ia.Width.HasValue)
                                    {
                                        var image = System.Drawing.Image.FromStream(file.InputStream);

                                        int width = ia.Width.Value;
                                        var imageScale = ImageUtils.ImageUtils.ScaleImage(image, width, (int)Math.Round((image.Height / ((double)image.Width / width)), 0));

                                        imageScale.Save(imageMS, ImageFormat.Jpeg);
                                    }
                                    else
                                    {
                                        file.InputStream.CopyTo(imageMS);
                                    }

                                    #endregion

                                    var newTargetProperty = GetInLifetimeScope.Instance<IEntityBaseFile>(targetProperty.PropertyType);

                                    newTargetProperty.Guid = Guid.NewGuid();
                                    newTargetProperty.ContentType = file.ContentType;
                                    newTargetProperty.Data = imageMS.ToArray();
                                    newTargetProperty.DateChanged = DateTime.Now;
                                    newTargetProperty.FileName = file.FileName;

                                    targetProperty.SetValue(create, newTargetProperty);
                                }
                            }
                        }
                        else
                        {
                            var targetProperty = create.GetType().GetProperty(propertyInfo.Name.Replace("HttpPosted", ""));
                            if (targetProperty != null)
                            {
                                var extension = Path.GetExtension(file.FileName);
                                var extensionProperty = create.GetType().GetProperty("Extension");
                                if (extensionProperty != null)
                                {
                                    extensionProperty.SetValue(create, extension);
                                }

                                var fileNameProperty = create.GetType().GetProperty("FileName");
                                if (fileNameProperty != null)
                                {
                                    var fileNamePropertyValue = fileNameProperty.GetValue(create);
                                    if (fileNameProperty != null && (fileNamePropertyValue == null || fileNamePropertyValue == ""))
                                    {
                                        fileNameProperty.SetValue(create, file.FileName.Replace(extension, ""));
                                    }
                                }

                                var nameProperty = create.GetType().GetProperty("Name");
                                if (nameProperty != null)
                                {
                                    var namePropertyValue = nameProperty.GetValue(create);
                                    if (namePropertyValue == null || namePropertyValue == "")
                                    {
                                        nameProperty.SetValue(create, file.FileName.Replace(extension, ""));
                                    }
                                }

                                var newTargetProperty = GetInLifetimeScope.Instance<IEntityBaseFile>(targetProperty.PropertyType);

                                newTargetProperty.Guid = Guid.NewGuid();
                                newTargetProperty.ContentType = file.ContentType;
                                newTargetProperty.Data = RequestHelper.GetPostedFileContent(file);
                                newTargetProperty.DateChanged = DateTime.Now;
                                newTargetProperty.FileName = file.FileName;

                                targetProperty.SetValue(create, newTargetProperty);
                            }
                        }
                    }
                }
            }

            var type = typeof(TEntityCreate);

            foreach (var selectedEntities in type
                                    .GetProperties()
                                    .Select(p => new { property = p, attribute = p.GetCustomAttributes(false).OfType<SelectedEntitiesAttribute>().FirstOrDefault() })
                                    .Where(a => a.attribute != null))
            {
                var ids = selectedEntities.property.GetValue(createView) as IEnumerable<int>;

                var repositoryType = typeof(IRepository<>).MakeGenericType(selectedEntities.attribute.Type);

                var tEntityRepository = GetInLifetimeScope.Instance(repositoryType) as IRepository;

                if (ids != null)
                {
                    var targetProp = create.GetType().GetProperties()
                                         .FirstOrDefault(p => p.PropertyType.IsAssignableFrom(typeof(ICollection<>).MakeGenericType(selectedEntities.attribute.Type)));

                    if (targetProp != null)
                    {
                        targetProp.SetValue(create, tEntityRepository.GetEntitiesByIds(ids));
                    }
                }
            }

            _repository.Value.Add(create);
            _repository.Value.SaveChanges();
        }

        public virtual TEntityCreate Create(IPrincipal principal)
        {
            return new TEntityCreate();
        }

        public virtual IQueryable<TEntity> GetEnumerableTEntityView(IPrincipal principal)
        {
            return _repository.Value.GetQuery();
        }

        public virtual ActionGrid<TEntity, TEntityGrid> GetActionGridTEntityList(TEntityGridOptions options, IPrincipal principal)
        {
            var query = GetEnumerableTEntityView(principal);
            
            return new ActionGrid<TEntity, TEntityGrid>(query, options);
        }

        public virtual void Edit(TEntityEdit editView, IPrincipal principal)
        {
            var edit = _repository.Value.Update(Mapper.Map<TEntityEdit, TEntity>(editView));

            foreach (var propertyInfo in editView.GetType().GetProperties().Where(p => p.PropertyType == typeof(HttpPostedFileBase)))
            {
                var fileBase = propertyInfo.GetValue(editView);

                if (fileBase != null)
                {
                    var file = fileBase as HttpPostedFileBase;

                    if (file != null)
                    {
                        var imageProperties = propertyInfo.GetCustomAttributes(false).OfType<ImageAttribute>();

                        if (imageProperties.Any())
                        {
                            foreach (var ia in imageProperties)
                            {
                                var targetProperty = edit.GetType().GetProperty(ia.PropertyName);

                                if (targetProperty != null)
                                {
                                    var imageMS = new MemoryStream();

                                    #region Scale

                                    if (ia.Width.HasValue)
                                    {
                                        var image = System.Drawing.Image.FromStream(file.InputStream);

                                        int width = ia.Width.Value;
                                        var imageScale = ImageUtils.ImageUtils.ScaleImage(image, width, (int)Math.Round((image.Height / ((double)image.Width / width)), 0));

                                        imageScale.Save(imageMS, ImageFormat.Jpeg);
                                    }
                                    else
                                    {
                                        file.InputStream.CopyTo(imageMS);
                                    }

                                    #endregion

                                    var value = targetProperty.GetValue(edit) as IEntityBaseFile;

                                    if (value != null)
                                    {
                                        value.Guid = Guid.NewGuid();
                                        value.ContentType = file.ContentType;
                                        value.Data = imageMS.ToArray();
                                        value.DateChanged = DateTime.Now;
                                    }
                                    else
                                    {
                                        var newTargetProperty = GetInLifetimeScope.Instance<IEntityBaseFile>(targetProperty.PropertyType);

                                        newTargetProperty.Guid = Guid.NewGuid();
                                        newTargetProperty.ContentType = file.ContentType;
                                        newTargetProperty.Data = imageMS.ToArray();
                                        newTargetProperty.DateChanged = DateTime.Now;

                                        targetProperty.SetValue(edit, newTargetProperty);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var targetProperty = edit.GetType().GetProperty(propertyInfo.Name.Replace("HttpPosted", ""));
                            if (targetProperty != null)
                            {
                                var extension = Path.GetExtension(file.FileName);
                                var extensionProperty = edit.GetType().GetProperty("Extension");
                                if (extensionProperty != null)
                                {
                                    extensionProperty.SetValue(edit, extension);
                                }

                                var fileNameProperty = edit.GetType().GetProperty("FileName");
                                if (fileNameProperty != null)
                                {
                                    var fileNamePropertyValue = fileNameProperty.GetValue(edit);
                                    if (fileNameProperty != null && (fileNamePropertyValue == null || fileNamePropertyValue == ""))
                                    {
                                        fileNameProperty.SetValue(edit, file.FileName.Replace(extension, ""));
                                    }
                                }

                                var value = targetProperty.GetValue(edit) as IEntityBaseFile;

                                if (value != null)
                                {
                                    value.Guid = Guid.NewGuid();
                                    value.ContentType = file.ContentType;
                                    value.Data = RequestHelper.GetPostedFileContent(file);
                                    value.DateChanged = DateTime.Now;
                                }
                                else
                                {
                                    var newTargetProperty = GetInLifetimeScope.Instance<IEntityBaseFile>(targetProperty.PropertyType);

                                    newTargetProperty.Guid = Guid.NewGuid();
                                    newTargetProperty.ContentType = file.ContentType;
                                    newTargetProperty.Data = RequestHelper.GetPostedFileContent(file);
                                    newTargetProperty.DateChanged = DateTime.Now;

                                    targetProperty.SetValue(edit, newTargetProperty);
                                }
                            }
                        }
                    }
                }
            }

            var type = typeof (TEntityEdit);

            foreach (var selectedEntities in type
                                    .GetProperties()
                                    .Select(p => new { property = p, attribute = p.GetCustomAttributes(false).OfType<SelectedEntitiesAttribute>().FirstOrDefault() })
                                    .Where(a => a.attribute != null))
            {
                var ids = selectedEntities.property.GetValue(editView) as IEnumerable<int>;

                var repositoryType = typeof(IRepository<>).MakeGenericType(selectedEntities.attribute.Type);

                var tEntityRepository = GetInLifetimeScope.Instance(repositoryType) as IRepository;

                var targetProp = edit.GetType().GetProperties()
                                         .FirstOrDefault(p => p.PropertyType.IsAssignableFrom(typeof(ICollection<>).MakeGenericType(selectedEntities.attribute.Type)));

                if (targetProp != null)
                {
                    var value = targetProp.GetValue(edit);
                    var expression = Expression.Call(Expression.Constant(value), "Clear", null, null);
                    var lambda = Expression.Lambda(expression);
                    lambda.Compile();

                    targetProp.SetValue(edit, tEntityRepository.GetEntitiesByIds(ids ?? new List<int>()));
                }
            }

            _repository.Value.SaveChanges();
        }

        public virtual TEntityEdit Edit(int id, IPrincipal principal)
        {
            var entity = Get(id);

            var edit = Mapper.Map<TEntity, TEntityEdit>(entity);

            return edit;
        }

        public virtual void Delete(int id, IPrincipal principal)
        {
            var delete = Get(id);
            _repository.Value.Delete(delete);
            _repository.Value.SaveChanges();
        }

        public void Sortable(Dictionary<int, int> rows)
        {
            if (typeof(ISortableEntity).IsAssignableFrom(typeof(TEntity)))
            {
                foreach (var row in rows)
                {
                    var entity = Get(row.Key) as ISortableEntity;
                    if (entity != null)
                        entity.SortOrder = row.Value;
                }
            }
            _repository.Value.SaveChanges();
        }

        public ImageResult Image(string image, int id)
        {
            var entity = _repository.Value.FirstOrDefault(id);
            if (entity != null)
            {
                var imgProp = entity.GetType().GetProperty(image);
                var imgVaue = imgProp.GetValue(entity);

                if (imgVaue != null && imgVaue is IEntityBaseFile)
                {
                    var img = imgVaue as IEntityBaseFile;

                    return new ImageResult(new MemoryStream(img.Data), img.ContentType);
                }
            }
            return null;
        }

        public FileContentResult File(string file, int id)
        {
            var entity = _repository.Value.FirstOrDefault(id);
            if (entity != null)
            {
                var fileProp = entity.GetType().GetProperty(file);
                var fileVaue = fileProp.GetValue(entity);

                if (fileVaue != null && fileVaue is IEntityBaseFile)
                {
                    var fileEntity = fileVaue as IEntityBaseFile;
                    
                    return new FileContentResult(fileEntity.Data, fileEntity.ContentType) { FileDownloadName = fileEntity.FileName };
                }
            }
            return null;
        }

        public EditWithTab EditWithTab(int id, IPrincipal principal, UrlHelper url)
        {
            var result = new List<SelectListItem>();

            result.Add(new SelectListItem { Text = "Главная", Value = url.Action("Edit", new { id }) });

            var typeEntity = typeof (TEntityEdit);

            foreach (var tab in typeEntity
                                    .GetProperties()
                                    .Select(p => new { property = p, attribute = p.GetCustomAttributes(false).OfType<EditTabAttribute>().FirstOrDefault() })
                                    .Where(a => a.attribute != null).OrderBy(a => a.attribute.Order))
            {
                var genericArguments = tab.property.PropertyType.GetGenericArguments();

                if (genericArguments.Any())
                {
                    result.Add(new SelectListItem
                    {
                        Text = tab.attribute.TabName,
                        Value = url.Action(tab.attribute.ActionName, genericArguments.First().Name, new { id })
                    });
                }
            }

            return new EditWithTab {Id = id, Name =  new TEntity().ToString(), Tabs = result};
        }

        public EditWithTab DisplayWithTab(int id, IPrincipal principal, UrlHelper url)
        {
            var result = new List<SelectListItem>();

            result.Add(new SelectListItem { Text = "Главная", Value = url.Action("Display", new { id }) });

            var typeEntity = typeof(TEntityEdit);

            foreach (var tab in typeEntity
                                    .GetProperties()
                                    .Select(p => new { property = p, attribute = p.GetCustomAttributes(false).OfType<EditTabAttribute>().FirstOrDefault() })
                                    .Where(a => a.attribute != null).OrderBy(a => a.attribute.Order))
            {
                var genericArguments = tab.property.PropertyType.GetGenericArguments();

                if (genericArguments.Any())
                {
                    result.Add(new SelectListItem
                    {
                        Text = tab.attribute.TabName,
                        Value = url.Action(tab.attribute.ActionName, genericArguments.First().Name, new { id })
                    });
                }
            }

            return new EditWithTab { Id = id, Name = new TEntity().ToString(), Tabs = result };
        }
    }
}
