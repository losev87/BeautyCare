using System;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Route
{
    public static class AreaRegistrationContextExtension
    {

        public static GenericAreaRegistrationContextInitializer MapDefault<T>(this GenericAreaRegistrationContextInitializer init)
        {
            init.AreaRegistrationContext.MapRoute(
                init.GenericType.Name + typeof (T).Name + "Default",
                init.UrlTemplate.Replace("<>", "{controller}"),
                new
                    {
                        controller = typeof (T).Name, //init.GenericType.Name,
                        controllerGenericType = init.GenericType.MakeGenericType(typeof (T)),
                        action = "Index",
                        id = UrlParameter.Optional
                    }
                );

            return init;
        }

        public static GenericAreaRegistrationContextInitializer Map<T>(this GenericAreaRegistrationContextInitializer init)
        {
            init.AreaRegistrationContext.MapRoute(
                init.GenericType.Name + typeof (T).Name,
                init.UrlTemplate.Replace("<>", typeof (T).Name),
                new
                    {
                        controller = typeof (T).Name, //init.GenericType.Name,
                        controllerGenericType = init.GenericType.MakeGenericType(typeof (T)),
                        action = "Index",
                        id = UrlParameter.Optional
                    }
                );

            return init;
        }

        public static GenericAreaRegistrationContextInitializer Generic(this AreaRegistrationContext context, Type type, string urlTemplate)
        {
            if (!type.IsGenericTypeDefinition)
                throw new ArgumentException("Тип должен быть generic определением");

            return new GenericAreaRegistrationContextInitializer { AreaRegistrationContext = context, UrlTemplate = urlTemplate, GenericType = type };
        }
    }
}
