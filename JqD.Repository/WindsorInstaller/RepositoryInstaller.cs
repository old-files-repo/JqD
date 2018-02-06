using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace JqD.Repository.WindsorInstaller
{
    public class RepositoryInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<RepositoryInstaller>()
                .Where(x => x.Name.EndsWith("Repository"))
                .WithService.DefaultInterfaces()
                .LifestylePerWebRequest());
        }
    }
}