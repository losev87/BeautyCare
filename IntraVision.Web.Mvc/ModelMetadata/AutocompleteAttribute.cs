using System;

namespace IntraVision.Web.Mvc
{
    public class AutocompleteAttribute : Attribute, IAdditionalValueAttribute
    {
        public string Action { get; set; }
        public string Controller { get; set; }

        public AutocompleteAttribute(string action, string controller)
        {
            Action = action;
            Controller = controller;
        }
    }
}
