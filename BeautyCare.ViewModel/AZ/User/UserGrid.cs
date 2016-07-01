using System.Web.Mvc;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ.User
{
    public class UserGrid : GridModel<UserView>
    {
        public UserGrid(HtmlHelper html)
        {
            Column.For(m => html.DeleteLink(m.Id)).Attributes(@class => "options").DoNotEncode();
            Column.For(m => m.Id).Named("Id").Attributes(@class => "options");
            Column.For(m => html.EditLink(m.Id, m.UserName, new[] { "dialog-form editUser" })).Sortable("UserName").DoNotEncode().Named("Login");
            Column.For(m => m.Email).Named("Email");
        }
    }
}
