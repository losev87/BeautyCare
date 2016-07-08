using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc;
using IntraVision.Web.Mvc.Autofac;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ
{
    public class AnswerFilter : Filter<Answer>
    {
        protected override Filter<Answer> Configure()
        {
            AddCondition(new StringFilterCondition<Answer>("Id.ToString()", "Id"));

            #region Question

            var repositoryQuestion = GetInLifetimeScope.Repository<Question>();

            var dictionaryQuestion = new List<SelectListItem>();

            dictionaryQuestion.AddRange(repositoryQuestion.GetQuery().OrderBy(c=>c.Header).ToSelectList(i => i.Id, i => i.Header).ToList());

            if (dictionaryQuestion.Count > 0)
                AddCondition(new SelectFilterCondition<Answer>("QuestionId", "Вопрос", dictionaryQuestion));

            #endregion

            #region Attachments

            AddCondition(new BoolFilterCondition<Answer>("Attachments.Any()", "Наличие вложений"));

            #endregion

            return this;
        }
    }
}
