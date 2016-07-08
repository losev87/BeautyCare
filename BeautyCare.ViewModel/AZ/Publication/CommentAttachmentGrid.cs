using System.Web.Mvc;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ
{
    public class CommentAttachmentGrid : GridModel<CommentAttachment>
    {
        public CommentAttachmentGrid(HtmlHelper html)
        {
            Column.For(m => html.DeleteLink(m.Id)).Attributes(@class => "options").DoNotEncode();
            Column.For(m => m.Id).Named("Id").Attributes(@class => "options");
            Column.For(m => html.EditLink(m.Id, m.Data.FileName+"."+m.Extension, new string[] {"dialog-form"})).Sortable(true).DoNotEncode().Named("Имя файла");
            Column.For(m => m.AttachmentCategory.Name).Named("Тип");
            Column.For(m => m.Data.ContentType).Named("ContentType");
            Column.For(m => m.Data.DateChanged).Named("Дата");
        }
    }
}
