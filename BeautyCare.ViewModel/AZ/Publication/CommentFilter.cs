using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc;
using IntraVision.Web.Mvc.Autofac;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ.User
{
    public class CommentFilter : Filter<Comment>
    {
        protected override Filter<Comment> Configure()
        {
            AddCondition(new StringFilterCondition<Comment>("Id.ToString()", "Id"));

            #region Publication

            var repositoryPublication = GetInLifetimeScope.Repository<Publication>();

            var dictionaryPublication = new List<SelectListItem>();

            dictionaryPublication.AddRange(repositoryPublication.GetQuery().OrderBy(c => c.Header).ToSelectList(i => i.Id, i => i.Header).ToList());

            if (dictionaryPublication.Count > 0)
                AddCondition(new SelectFilterCondition<Comment>("PublicationId", "Публикация", dictionaryPublication));

            #endregion

            #region Attachments

            AddCondition(new BoolFilterCondition<Comment>("Attachments.Any()", "Наличие вложений"));

            #endregion

            return this;
        }
    }
}
