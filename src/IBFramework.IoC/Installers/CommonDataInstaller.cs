﻿using IBFramework.Core.Data;
using IBFramework.Core.Data.SQL;
using IBFramework.Core.Enum;
using IBFramework.Core.IoC;
using IBFramework.Data.Common;
using IBFramework.Data.Common.Sql;
using IBFramework.Data.Common.Transaction;

namespace IBFramework.IoC.Installers
{
    public class CommonDataInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.Register<DatabaseKeyManager>().As<IDatabaseKeyManager>().WithLifestyle(RegistrationLifestyleType.Singleton);

            containerGenerator.Register<TranConn>().As<ITranConn>().WithLifestyle(RegistrationLifestyleType.Transient);
            containerGenerator.Register<TransactionHelper>().As<ITransactionHelper>().WithLifestyle(RegistrationLifestyleType.Transient);

            containerGenerator.Register<SqlPropertyGenerator>().As<ISqlPropertyGenerator>().WithLifestyle(RegistrationLifestyleType.Transient);

            containerGenerator.Register(typeof(Repository<>)).As(typeof(IRepository<>)).WithLifestyle(RegistrationLifestyleType.Transient);
            containerGenerator.Register(typeof(Repository<,>)).As(typeof(IRepository<,>)).WithLifestyle(RegistrationLifestyleType.Transient);
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
