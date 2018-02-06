using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JqD.Common.ILogic;
using JqD.Common.IRepository;
using JqD.Common.Logic;
using JqD.Common.Repository;

namespace JqD.Common.WindsorInstaller
{
    public class CommonInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISystemUserLogic>().ImplementedBy<SystemUserLogic>().LifestylePerWebRequest(),
                Component.For<IOperationLogsRepository>().ImplementedBy<OperationLogsRepository>().LifestylePerWebRequest(),
                Component.For<ISystemUserRepository>().ImplementedBy<SystemUserRepository>().LifestylePerWebRequest()
                );
        }
    }
}