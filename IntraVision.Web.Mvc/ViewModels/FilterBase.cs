using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IntraVision.Repository;
using IntraVision.Web.Mvc.Autofac;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc
{
    public class FilterBase<TEntity> : Filter<TEntity>
        where TEntity : class
    {
        protected override Filter<TEntity> Configure()
        {
            var tEntityType = typeof (TEntity);

            foreach (var filter in tEntityType
                                    .GetProperties()
                                    .Select(p => new { property = p, attribute = p.GetCustomAttributes(false).OfType<FilterGridAttribute>().FirstOrDefault() })
                                    .Where(a => a.attribute != null).OrderBy(a => a.attribute.Order))
            {
                var repositoryType = typeof (IRepository<>).MakeGenericType(filter.attribute.Type);

                var tEntityRepository = GetInLifetimeScope.Instance(repositoryType) as IRepository;

                if (tEntityRepository != null)
                {
                    var tEntities = new List<SelectListItem>();

                    tEntities.AddRange(tEntityRepository.GetNamedEntityBases().OrderBy(c => c.Name).ToSelectList(i => i.Id, i => i.Name).ToList());

                    if (tEntities.Count > 0)
                        AddCondition(new SelectFilterCondition<TEntity>(filter.attribute.Column, filter.attribute.Named, tEntities));
                }
            }

            return this;
        }
    }
}
