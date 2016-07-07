using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IntraVision.Data;
using IntraVision.Web.Mvc.Controls;
using IntraVision.Web.Mvc.Services;

namespace BeautyCare.Controllers.AZ
{
    public class DependentEntityFileCRUDController<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions>
        : DependentEntityCRUDController<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions>
        where TEntity : class, IEntityBase
        where TEntityCreate : class
        where TEntityEdit : class
        where TEntityGrid : class, IGridModel<TEntity>
        where TEntityGridOptions : GridOptions
    {
        public DependentEntityFileCRUDController(IDependentEntityService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> dependentService)
            : base(dependentService)
        {
        }

        public override ActionResult CreateDependent(TEntityCreate create)
        {
            foreach (var prop in create.GetType().GetProperties().Where(p => p.PropertyType == typeof(HttpPostedFileBase)))
            {
                if (prop.GetValue(create) == null)
                {
                    ViewData.ModelState.AddModelError("", "Укажите файл!");
                }
            }
            return base.CreateDependent(create);
        }
    }
}
