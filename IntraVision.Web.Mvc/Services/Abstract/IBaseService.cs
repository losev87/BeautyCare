using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc.Services
{
    public interface IBaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, in TEntityGridOptions>
        where TEntity : class
        where TEntityCreate : class
        where TEntityEdit : class
        where TEntityGridOptions : GridOptions
        where TEntityGrid : class, IGridModel<TEntity>
    {
        IList<TEntity> GetList();
        TEntity Get(int id);
        void Create(TEntityCreate create, IPrincipal principal);
        TEntityCreate Create(IPrincipal principal);
        IQueryable<TEntity> GetEnumerableTEntityView(IPrincipal principal);
        ActionGrid<TEntity, TEntityGrid> GetActionGridTEntityList(TEntityGridOptions options, IPrincipal principal);
        void Edit(TEntityEdit edit, IPrincipal principal);
        TEntityEdit Edit(int id, IPrincipal principal);
        void Delete(int id, IPrincipal principal);
        void Sortable(Dictionary<int, int> rows);
        EditWithTab EditWithTab(int id, IPrincipal principal, UrlHelper url);
        EditWithTab DisplayWithTab(int id, IPrincipal principal, UrlHelper url);

        ImageResult Image(string image, int id);
        FileContentResult File(string file, int id);
    }
}
