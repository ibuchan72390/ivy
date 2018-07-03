using Ivy.IoC;
using Ivy.Utility.Core;
using Ivy.Utility.IoC;

/*
 * When working in a Mocking context, 
 * this will allow you to access the IRandomizationHelper 
 * functionality without going through a modified container.
 * 
 * This ensures you will never pull a Mock<IRandomizationHelper> on accident
 */
namespace Ivy.TestUtilities.Utilities
{
    public static class StaticRandom
    {
        private static IRandomizationHelper _rand;

        public static IRandomizationHelper Rand
        {
            get {

                if (_rand == null)
                {
                    var containerGen = new ContainerGenerator();

                    containerGen.InstallIvyUtility();

                    _rand = containerGen.GenerateContainer()
                        .GetService<IRandomizationHelper>();
                }

                return _rand;
            }
        }
    }
}
