using IBFramework.Core.Data;
using IBFramework.Core.Data.SQL;
using IBFramework.Core.IoC;
using IBFramework.Data.Common.Sql;
using IBFramework.Data.Common.Transaction;

namespace IBFramework.Data.Common.IoC
{
    public class CommonDataInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            //containerGenerator.Register<DatabaseKeyManager>().As<IDatabaseKeyManager>().WithLifestyle(RegistrationLifestyleType.Singleton);

            //containerGenerator.Register<TranConn>().As<ITranConn>().WithLifestyle(RegistrationLifestyleType.Transient);
            //containerGenerator.Register<TransactionHelper>().As<ITransactionHelper>().WithLifestyle(RegistrationLifestyleType.Transient);

            //containerGenerator.Register<SqlPropertyGenerator>().As<ISqlPropertyGenerator>().WithLifestyle(RegistrationLifestyleType.Transient);

            //containerGenerator.Register(typeof(BlobRepository<>)).As(typeof(IBlobRepository<>)).WithLifestyle(RegistrationLifestyleType.Transient);
            //containerGenerator.Register(typeof(EntityRepository<,>)).As(typeof(IEntityRepository<,>)).WithLifestyle(RegistrationLifestyleType.Transient);

            //containerGenerator.Register(typeof(EntityRepository<>)).As(typeof(IEntityRepository<>)).WithLifestyle(RegistrationLifestyleType.Transient);



            containerGenerator.RegisterSingleton<IDatabaseKeyManager, DatabaseKeyManager>();

            containerGenerator.RegisterTransient<ITranConn, TranConn>();
            containerGenerator.RegisterTransient<ITransactionHelper, TransactionHelper>();

            containerGenerator.RegisterTransient<ISqlPropertyGenerator, SqlPropertyGenerator>();

            containerGenerator.RegisterTransient(typeof(IBlobRepository<>), typeof(BlobRepository<>));
            containerGenerator.RegisterTransient(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            containerGenerator.RegisterTransient(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));

        }
    }

    public static class CommonDataInstallerExtension
    {
        public static IContainerGenerator InstallCommonData(this IContainerGenerator containerGenerator)
        {
            new CommonDataInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
