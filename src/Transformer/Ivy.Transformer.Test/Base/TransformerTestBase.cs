using Ivy.TestHelper;
using Ivy.Transformer.IoC;

namespace Ivy.Transformer.Test.Base
{
    public class TransformerTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => containerGen.InstallIvyTransformer());
        }
    }
}
