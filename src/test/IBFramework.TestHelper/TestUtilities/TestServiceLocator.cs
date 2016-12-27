using IBFramework.IoC;
using IBFramework.Core.IoC;

namespace IBFramework.TestHelper
{
    public class TestServiceLocator : ServiceLocator
    {
        public static IContainer StaticContainer;

        public override void Init(IContainer container)
        {
            base.Init(container);
            StaticContainer = container;
        }

        public override void Reset()
        {
            base.Reset();
            StaticContainer = null;
        }
    }
}
