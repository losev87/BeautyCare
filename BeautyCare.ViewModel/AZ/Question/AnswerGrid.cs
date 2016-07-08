using System.Web.Mvc;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ.User
{
    public class AnswerGrid : GridModel<Answer>
    {
        public AnswerGrid(HtmlHelper html)
        {
            Column.For(m => html.DeleteLink(m.Id)).Attributes(@class => "options").DoNotEncode();
            Column.For(m => m.Id).Named("Id").Attributes(@class => "options");
            Column.For(m => html.EditLink(m.Id, m.Text.Substring(0, 30) + "...", new string[] { })).Sortable(true).DoNotEncode().Named("Текст");
            Column.For(m => m.Rating).Named("Рейтинг");
            Column.For(m => m.QuestionId.HasValue ? "Вопрос: " + m.QuestionId : "Ответ: " + m.ParentAnswerId).Named("Родитель");
            Column.For(m => m.Sender.Id).Named("Id отправителя");
            Column.For(m => m.Sender.LastName).Named("Фамилия отправителя");
            Column.For(m => m.Sender.LastName).Named("Имя отправителя");
            Column.For(m => m.Sender.LastName).Named("Отчество отправителя");
            Column.For(m => m.SendDateTime).Named("Время");
        }
    }
}
