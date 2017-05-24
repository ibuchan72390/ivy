﻿using Ivy.Data.Common.Sql;
using Ivy.Data.Common.Transaction;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.IoC.Core;

namespace Ivy.Data.Common.IoC
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
        }
    }

    public static class CommonDataInstallerExtension
    {
        public static IContainerGenerator InstallIvyCommonData(this IContainerGenerator containerGenerator)
        {
            new CommonDataInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}