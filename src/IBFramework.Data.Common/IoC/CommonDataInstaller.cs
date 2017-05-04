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
            containerGenerator.RegisterSingleton<IDatabaseKeyManager, DatabaseKeyManager>();

            containerGenerator.RegisterTransient<ITranConn, TranConn>();
            containerGenerator.RegisterTransient<ITransactionHelper, TransactionHelper>();

            containerGenerator.RegisterTransient<ISqlPropertyGenerator, SqlPropertyGenerator>();

            containerGenerator.RegisterTransient(typeof(IBlobRepository<>), typeof(BlobRepository<>));
            containerGenerator.RegisterTransient(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            containerGenerator.RegisterTransient(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));
            containerGenerator.RegisterTransient(typeof(IEnumEntityRepository<,>), typeof(EnumEntityRepository<,>));
            //containerGenerator.RegisterTransient(typeof(IEnumEntityRepository<,,>), typeof(EnumEntityRepository<,,>));
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
