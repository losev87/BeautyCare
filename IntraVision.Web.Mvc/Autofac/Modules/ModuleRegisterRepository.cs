using System;
using Autofac;
using IntraVision.Repository;

namespace IntraVision.Web.Mvc.Autofac.Modules
{
    public class ModuleRegisterRepository : Module
    {
        private Type _repositoryType;

        public ModuleRegisterRepository(Type repositoryType)
        {
            _repositoryType = repositoryType;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(_repositoryType).As(typeof(IRepository<>)).InstancePerRequest();
        }
    }
}
