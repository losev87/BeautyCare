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
    public class PrivateMessageController : CrudController<PrivateMessage, PrivateMessage, PrivateMessage, PrivateMessageGrid, PrivateMessageGridOptions>
    {
        public PrivateMessageController(IBaseService<PrivateMessage, PrivateMessage, PrivateMessage, PrivateMessageGrid, PrivateMessageGridOptions> service)
            : base(service)
        {
        }
    }
}
