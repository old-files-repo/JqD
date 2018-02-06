using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace JqD.Logic.WindsorInstaller
{
    public class LogicInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<LogicInstaller>()
                .Where(x => x.Name.EndsWith("Logic"))
                .WithService.DefaultInterfaces()
                .LifestylePerWebRequest());
        }
    }
}