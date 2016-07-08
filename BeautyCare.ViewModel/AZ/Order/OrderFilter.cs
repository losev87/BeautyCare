using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BeautyCare.Model.Entity;
using IntraVision.Web.Mvc;
using IntraVision.Web.Mvc.Autofac;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.ViewModel.AZ.User
{
    public class OrderMessageFilter : Filter<OrderMessage>
    {
        protected override Filter<OrderMessage> Configure()
        {
            AddCondition(new StringFilterCondition<OrderMessage>("Id.ToString()", "Id"));

            #region Order

            var repositoryOrder = GetInLifetimeScope.Repository<Order>();

            var dictionaryOrder = new List<SelectListItem>();

            dictionaryOrder.AddRange(repositoryOrder.GetQuery().OrderBy(c => c.Title).ToSelectList(i => i.Id, i => i.Title).ToList());

            if (dictionaryOrder.Count > 0)
                AddCondition(new SelectFilterCondition<OrderMessage>("OrderId", "Заказ", dictionaryOrder));

            #endregion

            #region Attachments

            AddCondition(new BoolFilterCondition<OrderMessage>("Attachments.Any()", "Наличие вложений"));

            #endregion

            return this;
        }
    }
}
