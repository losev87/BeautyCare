using System.Web.Mvc;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ
{
    public class CommentGrid : GridModel<Comment>
    {
        public CommentGrid(HtmlHelper html)
        {
            Column.For(m => html.DeleteLink(m.Id)).Attributes(@class => "options").DoNotEncode();
            Column.For(m => m.Id).Named("Id").Attributes(@class => "options");
            Column.For(m => html.EditLink(m.Id, m.Text.Substring(0, 30) + "...", new string[] { })).Sortable(true).DoNotEncode().Named("Текст");
            Column.For(m => m.Rating).Named("Рейтинг");
            Column.For(m => m.PublicationId.HasValue ? "Публикация: " + m.PublicationId : "Комментарий: " + m.ParentCommentId).Named("Родитель");
            Column.For(m => m.Sender.Id).Named("Id отправителя");
            Column.For(m => m.Sender.LastName).Named("Фамилия отправителя");
            Column.For(m => m.Sender.LastName).Named("Имя отправителя");
            Column.For(m => m.Sender.LastName).Named("Отчество отправителя");
            Column.For(m => m.SendDateTime).Named("Время");
        }
    }
}
