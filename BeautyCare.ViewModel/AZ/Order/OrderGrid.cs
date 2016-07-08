using System.Web.Mvc;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ.User
{
    public class OrderGrid : GridModel<Order>
    {
        public OrderGrid(HtmlHelper html)
        {
            Column.For(m => html.DeleteLink(m.Id)).Attributes(@class => "options").DoNotEncode();
            Column.For(m => m.Id).Named("Id").Attributes(@class => "options");
            Column.For(m => html.EditLink(m.Id, m.Title, new string[] { })).Sortable(true).DoNotEncode().Named("Заголовок");
            Column.For(m => m.Executor.Id).Named("Id исполнителя");
            Column.For(m => m.Executor.LastName).Named("Фамилия исполнителя");
            Column.For(m => m.Executor.LastName).Named("Имя исполнителя");
            Column.For(m => m.Executor.LastName).Named("Отчество исполнителя");
            Column.For(m => m.Customer.Id).Named("Id заказчика");
            Column.For(m => m.Customer.LastName).Named("Фамилия заказчика");
            Column.For(m => m.Customer.LastName).Named("Имя заказчика");
            Column.For(m => m.Customer.LastName).Named("Отчество заказчика");
            Column.For(m => m.StartDateTime).Named("Время начала");
            Column.For(m => m.EndDateTime).Named("Время окончания");
        }
    }
}
