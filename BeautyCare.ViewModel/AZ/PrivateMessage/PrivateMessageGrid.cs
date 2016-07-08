using System.Web.Mvc;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ
{
    public class PrivateMessageGrid : GridModel<PrivateMessage>
    {
        public PrivateMessageGrid(HtmlHelper html)
        {
            Column.For(m => html.DeleteLink(m.Id)).Attributes(@class => "options").DoNotEncode();
            Column.For(m => m.Id).Named("Id").Attributes(@class => "options");
            Column.For(m => html.EditLink(m.Id, m.Text.Substring(0, 30) + "...", new string[] { })).Sortable(true).DoNotEncode().Named("Сообщение");
            Column.For(m => m.Recipient.Id).Named("Id получателя");
            Column.For(m => m.Recipient.LastName).Named("Фамилия получателя");
            Column.For(m => m.Recipient.LastName).Named("Имя получателя");
            Column.For(m => m.Recipient.LastName).Named("Отчество получателя");
            Column.For(m => m.Sender.Id).Named("Id отправителя");
            Column.For(m => m.Sender.LastName).Named("Фамилия отправителя");
            Column.For(m => m.Sender.LastName).Named("Имя отправителя");
            Column.For(m => m.Sender.LastName).Named("Отчество отправителя");
            Column.For(m => m.SendDateTime).Named("Время");
        }
    }
}
