using System;
using System.Web;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Owin;
using IntraVision.Data;
using IntraVision.Repository;

namespace IntraVision.Web.Mvc.Autofac
{
    public static class GetInLifetimeScope
    {
        public static IRepository<TEntity> Repository<TEntity>()
            where TEntity : class, IEntityBase
        {
            var lifetimeScope = LifetimeScope();
            if (lifetimeScope != null)
            {
                var tEntityRepository = lifetimeScope.Resolve<IRepository<TEntity>>();
                if (tEntityRepository != null)
                {
                    return tEntityRepository;
                }
            }
            return null;
        }

        public static TInstance Instance<TInstance>(params Parameter[] parameters)
            where TInstance : class
        {
            var lifetimeScope = LifetimeScope();
            if (lifetimeScope != null)
            {
                var tInstance = lifetimeScope.Resolve<TInstance>(parameters);
                if (tInstance != null)
                {
                    return tInstance;
                }
            }
            return null;
        }

        public static object Instance(Type type, params Parameter[] parameters)
        {
            var lifetimeScope = LifetimeScope();
            if (lifetimeScope != null)
            {
                return lifetimeScope.Resolve(type, parameters);
            }
            return null;
        }

        public static T Instance<T>(Type type, params Parameter[] parameters)
            where T : class
        {
            var lifetimeScope = LifetimeScope();
            if (lifetimeScope != null)
            {
                var instance = lifetimeScope.Resolve(type, parameters);

                return instance as T;
            }
            return null;
        }

        public static ILifetimeScope LifetimeScope()
        {
            return HttpContext.Current.Request.GetOwinContext().GetAutofacLifetimeScope();
        }
    }
}
