using System;
using System.Linq;
using System.Web.Mvc;
using BeautyCare.Model.Entity;
using BeautyCare.Model.Management;
using BeautyCare.Service;
using BeautyCare.ViewModel.AZ;

using IntraVision.Data;
using IntraVision.Web.Mvc;
using IntraVision.Web.Mvc.Services;
using Microsoft.AspNet.Identity;

namespace BeautyCare.Controllers.AZ
{
    public class OrderMessageController : FiltrateCrudController<OrderMessage, OrderMessage, OrderMessage, OrderMessageGrid, OrderMessageGridOptions, OrderMessageFilter>
    {
        public OrderMessageController(IFilterableBaseService<OrderMessage, OrderMessage, OrderMessage, OrderMessageGrid, OrderMessageGridOptions, OrderMessageFilter> service)
            : base(service)
        {
            ViewBag.EntityName = "Сообщения к заказу";
        }
    }
}
