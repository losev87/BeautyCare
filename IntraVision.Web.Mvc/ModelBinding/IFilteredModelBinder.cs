using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public interface IFilteredModelBinder : IModelBinder
    {
        bool IsMatch(ModelBindingContext bindingContext);
    }
}
