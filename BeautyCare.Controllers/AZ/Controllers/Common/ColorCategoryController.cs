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
    public class ColorCategoryController : CrudController<ColorCategory, ColorCategory, ColorCategory, ColorCategoryGrid, ColorCategoryGridOptions>
    {
        public ColorCategoryController(IBaseService<ColorCategory, ColorCategory, ColorCategory, ColorCategoryGrid, ColorCategoryGridOptions> service)
            : base(service)
        {
        }
    }
}
