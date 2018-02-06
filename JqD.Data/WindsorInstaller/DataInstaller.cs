using System.Data;
using System.Data.SqlClient;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JqD.Data.Logic;
using JqD.Data.Repository;

namespace JqD.Data.WindsorInstaller
{
    public class DataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IConnectionStringManager>().Instance(new ConnectionStringManager(DataConfig.GetConnectionString())),
                Component.For<IDbConnectionFactory>().ImplementedBy<SqlConnectionFactory>(),
                //SqlConnection，OracleConnection，OleDbConnection等类都继承自DbConnection类,
                //DbConnection类实现了IDbConnection这个接口
                Component.For<IDbConnection>().ImplementedBy<SqlConnection>().LifestyleTransient(),
                Component.For<ISqlDatabaseProxy>().ImplementedBy<SqlDatabaseProxy>(),
                Component.For(typeof(IRepositoryBase<>)).ImplementedBy(typeof(RepositoryBase<>)),
                Component.For(typeof(ILogicBase<>)).ImplementedBy(typeof(LogicBase<>))
            );
        }
    }
}