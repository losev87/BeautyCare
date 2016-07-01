using System.Web.Mvc;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc.Services
{
    public interface IDependentEntityFileService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> 
        : IDependentEntityService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions>
        where TEntity : class
        where TEntityCreate : class
        where TEntityEdit : class
        where TEntityGridOptions : GridOptions
        where TEntityGrid : class, IGridModel<TEntity>
    {
        FileContentResult GetFile(int id);
        bool FileExist(int id);
    }
}
