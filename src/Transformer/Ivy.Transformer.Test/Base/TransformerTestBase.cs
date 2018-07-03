using Ivy.IoC.Core;
using Ivy.TestHelper;
using Ivy.Transformer.IoC;

namespace Ivy.Transformer.Test.Base
{
    public class TransformerTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyTransformer();
        }
    }
}
