using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace IntraVision.Web.Mvc.Route
{
    public static class RouteCollectionExtension
    {
        public static GenericRoutesInitializer Map<T>(this GenericRoutesInitializer gri)
        {
            gri.RouteCollection.MapRoute(
                gri.GenericType.Name + typeof (T).Name,
                gri.UrlTemplate.Replace("<>", typeof (T).Name),
                new
                    {
                        controller = typeof (T).Name,
                        controllerGenericType = gri.GenericType.MakeGenericType(typeof(T)),
                        action = "Index",
                        id = UrlParameter.Optional
                    }
                );

            return gri;
        }

        public static GenericRoutesInitializer Generic(this RouteCollection routes, Type type, string urlTemplate)
        {
            if (!type.IsGenericTypeDefinition)
                throw new ArgumentException("Тип должен быть generic определением");

            return new GenericRoutesInitializer { RouteCollection = routes, UrlTemplate = urlTemplate, GenericType = type };
        }
    }
}
