using IBFramework.Core.Data;
using IBFramework.Core.Data.SQL;
using IBFramework.Core.IoC;
using IBFramework.Data.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBFramework.IoC.Installers
{
    public class MsSqlInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.Register<MsSqlTransactionHelper>().As<ITransactionHelper>();
            containerGenerator.Register<MsSqlTranConnGenerator>().As<ITranConnGenerator>();
            containerGenerator.Register<SqlPropertyGenerator>().As<ISqlPropertyGenerator>();
            containerGenerator.Register(typeof(MsSqlGenerator<>)).As(typeof(ISqlGenerator<>));
            containerGenerator.Register(typeof(Repository<>)).As(typeof(IRepository<>));
        }
    }

    public static class MsSqlInstallerExtension
    {
        public static IContainerGenerator InstallMsSql(this IContainerGenerator container)
        {
            new MsSqlInstaller().Install(container);
            return container;
        }
    }
}
