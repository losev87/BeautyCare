using System;
using System.Web.Mvc;
using IntraVision.Web.Mvc.Autofac;

namespace IntraVision.Web.Mvc.Security
{
    public class LogOnViewDataModelBinder : DefaultModelBinder, IFilteredModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return GetInLifetimeScope.Instance<ILogOnViewData>();
        }

        public bool IsMatch(ModelBindingContext context)
        {
            return typeof(ILogOnViewData).IsAssignableFrom(context.ModelType);
        }
    }
}
