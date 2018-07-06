using Ivy.IoC.Core;
using Ivy.Migration.IoC;
using Ivy.TestUtilities.Base;

namespace Ivy.Migration.Test.Base
{
    public class MigrationTestBase<T> : 
        GenericTestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyMigration();
        }
    }
}
