using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using JqD.Common.WindsorInstaller;
using JqD.Data.WindsorInstaller;
using JqD.Logic.WindsorInstaller;
using JqD.Repository.WindsorInstaller;

namespace JqueryDapper.WindsorInstaller
{
    public class WindsorBootstrapper
    {
        public static IWindsorContainer Container { get; private set; }

        public static void Initialize(IWindsorContainer container)
        {
            Container = container;
            InitializeCommon(container);
        }

        public static void InitializeCommon(IWindsorContainer container)
        {
            container.Install(FromAssembly.This(),
                FromAssembly.Containing<DataInstaller>(),
                FromAssembly.Containing<RepositoryInstaller>(),
                FromAssembly.Containing<LogicInstaller>(),
                FromAssembly.Containing<CommonInstaller>()
            );
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestylePerWebRequest());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            container.Register(Component.For<IWindsorContainer>().Instance(container).LifestyleSingleton());
        }
    }
}