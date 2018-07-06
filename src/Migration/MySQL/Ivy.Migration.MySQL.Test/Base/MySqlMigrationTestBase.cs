using Ivy.IoC.Core;
using Ivy.Migration.MySQL.IoC;
using Ivy.TestUtilities.Base;

namespace Ivy.Migration.MySQL.Test.Base
{
    public class MySqlMigrationTestBase<T> : 
        GenericTestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyMySqlMigration();
        }
    }
}
