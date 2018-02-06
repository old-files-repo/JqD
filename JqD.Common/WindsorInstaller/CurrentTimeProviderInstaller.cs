using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace JqD.Common.WindsorInstaller
{
    public class CurrentTimeProviderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICurrentTimeProvider>().ImplementedBy<CurrentTimeProvider>().LifestyleSingleton()
            );
        }
    }
}