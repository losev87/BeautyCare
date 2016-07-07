using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IntraVision.Data;
using IntraVision.Web.Mvc;
using IntraVision.Web.Mvc.Controls;
using IntraVision.Web.Mvc.Services;

namespace BeautyCare.Controllers.AZ
{
    public class DependentEntityCRUDController<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> 
        : CrudController<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions>
        where TEntity : class, IEntityBase
        where TEntityCreate : class
        where TEntityEdit : class
        where TEntityGrid : class, IGridModel<TEntity>
        where TEntityGridOptions : GridOptions
    {
        private IDependentEntityService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> _dependentService;

        public DependentEntityCRUDController(IDependentEntityService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> dependentService)
            : base(dependentService)
        {
            _dependentService = dependentService;
        }

        public virtual ActionResult IndexDependent(int id)
        {
            return View("IndexDependent", id);
        }

        [HttpGet]
        public virtual ActionResult CreateDependent(int id)
        {
            return View("Create", _dependentService.CreateDependent(id));
        }

        [HttpPost]
        public virtual ActionResult CreateDependent(TEntityCreate create)
        {
            if (!ViewData.ModelState.IsValid)
                return View("Create", create);

            return ExecuteCommandWithFile(() => _service.Create(create, User));
        }

        public virtual ActionResult GridByReferenceId(TEntityGridOptions options, int id)
        {
            return View("Grid", _dependentService.GridByReferenceId(options, User, id));
        }
    }
}
