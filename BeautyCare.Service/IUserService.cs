using System.Security.Principal;
using BeautyCare.ViewModel.AZ;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.Service
{
    public interface IUserService
    {
        ActionGrid<UserView, UserGrid> Grid(UserGridOptions options, IPrincipal principal);
        void Create(UserCreate create, IPrincipal principal);
        UserEdit Edit(int id, IPrincipal principal);
        void Edit(UserEdit edit, IPrincipal principal);
        void Delete(int id, IPrincipal principal);
    }
}
