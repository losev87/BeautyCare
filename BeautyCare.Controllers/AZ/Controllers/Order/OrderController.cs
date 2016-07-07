using System;
using System.Linq;
using System.Web.Mvc;
using BeautyCare.Model.Entity;
using BeautyCare.Model.Management;
using BeautyCare.Service;
using BeautyCare.ViewModel.AZ;
using BeautyCare.ViewModel.AZ.User;
using IntraVision.Data;
using IntraVision.Web.Mvc;
using IntraVision.Web.Mvc.Services;
using Microsoft.AspNet.Identity;

namespace BeautyCare.Controllers.AZ
{
    public class OrderController : CrudController<Order, Order, Order, OrderGrid, OrderGridOptions>
    {
        public OrderController(IBaseService<Order, Order, Order, OrderGrid, OrderGridOptions> service)
            : base(service)
        {
        }
    }
}
