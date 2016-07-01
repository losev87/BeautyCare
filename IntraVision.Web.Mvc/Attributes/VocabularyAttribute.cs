using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;
using IntraVision.Repository;

namespace IntraVision.Web.Mvc
{
    public class VocabularyAttribute : Attribute
   {
        public Type SourceType { get; set; }
        public string NameValue { get; set; }
        public string NameText { get; set; }

        public List<SelectListItem> GetDictionary<TTargetProperty>(TTargetProperty targetProperty)
        {
            if (SourceType != null)
            {
                var lifetimeScope = DependencyResolver.Current.GetService<ILifetimeScope>();
                if (lifetimeScope != null)
                {
                    var repository = lifetimeScope.Resolve(SourceType);
                    if (repository != null && repository.GetType().IsAssignableFrom(typeof (IRepository<>)))
                    {
                        var hdrth = repository.GetType();
                        var dictionary = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
                        //dictionary.AddRange((repository as IRepository<>).GetAll().ToSelectList(c => c.Id, c => c.Name, c => c.Id == PlannedBuyingNewCarId));

                        return dictionary;
                    }
                }
            }
            return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
        }
   }
}
