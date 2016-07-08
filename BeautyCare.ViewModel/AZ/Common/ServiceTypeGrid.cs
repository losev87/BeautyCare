using System.Web.Mvc;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ
{
    public class ServiceTypeGrid : GridModel<ServiceType>
    {
        public ServiceTypeGrid(HtmlHelper html)
        {
            Column.For(m => html.DeleteLink(m.Id)).Attributes(@class => "options").DoNotEncode();
            Column.For(m => m.Id).Named("Id").Attributes(@class => "options");
            Column.For(m => html.EditLink(m.Id, m.Name, new[] { "dialog-form" })).Sortable(true).DoNotEncode().Named("Название");
            Column.For(m => m.SysName).Named("Системное название");
        }
    }
}
