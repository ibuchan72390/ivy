using IBFramework.IoC;
using IBFramework.Core.IoC;

namespace IBFramework.TestUtilities
{
    public class TestServiceLocator : ServiceLocator
    {
        // May need to move this to the base
        // It's not a good pattern, but occasionally necessary
        private static IContainer _staticContainer;

        public static IContainer StaticContainer
        {
            get
            {
                if (_staticContainer == null)
                    throw new System.Exception("TestServiceLocator not Initialized!");

                return _staticContainer;
            }
        }

        public override void Init(IContainer container)
        {
            base.Init(container);
            _staticContainer = container;
        }

        public override void Reset()
        {
            base.Reset();
            _staticContainer = null;
        }
    }
}
