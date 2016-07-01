using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using IntraVision.Data;

namespace IntraVision.Web.Mvc
{
    public class DictionaryControllerFactory : StructureMapControllerFactory
    {
        private List<Type> _dictionaryModelCache;
        protected override Type GetControllerType(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            //If RouteData contains BaseType of the controller use it as the generic type with a controllerName as an entity type
            if (requestContext.RouteData.Values.ContainsKey("BaseType"))
            {
                return ((Type)requestContext.RouteData.Values["BaseType"]).MakeGenericType(GetDictionaryType(requestContext.RouteData.Values["modeltype"].ToString()), GetDictionaryType(requestContext.RouteData.Values["modeltype"].ToString() + "View"));
            }
            return base.GetControllerType(requestContext, controllerName);
        }

        protected Type GetDictionaryType(string typeName)
        {
            if (_dictionaryModelCache == null)
            {
                _dictionaryModelCache = new List<Type>();
                ICollection assemblies = BuildManager.GetReferencedAssemblies();
                foreach (Assembly assembly in assemblies)
                {
                    Type[] typesInAsm;
                    try
                    {
                        typesInAsm = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInAsm = ex.Types;
                    }
                    _dictionaryModelCache.AddRange(typesInAsm.Where(IsIdEntity));
                }
            }
            return _dictionaryModelCache.SingleOrDefault(t => t.Name.ToUpper() == typeName.ToUpper());
        }

        internal static bool IsIdEntity(Type t)
        {
            return
                t != null &&
                t.IsPublic &&
                !t.IsAbstract &&
                typeof(IEntity).IsAssignableFrom(t);
        }
    }
}
