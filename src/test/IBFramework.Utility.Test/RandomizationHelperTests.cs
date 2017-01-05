using IBFramework.Core.Utility;
using IBFramework.TestHelper;
using IBFramework.TestUtilities;
using System;
using Xunit;

namespace IBFramework.Utility.Test
{
    public class RandomizationHelperTests : TestBase
    {
        #region Variables & Constants

        private readonly IRandomizationHelper _sut;

        #endregion

        #region Constructor

        public RandomizationHelperTests()
        {
            _sut = TestServiceLocator.StaticContainer.Resolve<IRandomizationHelper>();
        }

        #endregion

        #region Tests

        [Fact]
        public void RandomString_Generates_Random_String_To_Length()
        {
            const int length = 20;

            var result = _sut.RandomString(length);

            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void RandomString_Passes_Random_Sample_Test()
        {
            TestRandomSample(
                () => _sut.RandomString(),
                (a, b) => a.Equals(b));
        }

        [Fact]
        public void RandomDouble_Passes_Random_Sample_Test()
        {
            TestRandomSample(
                () => _sut.RandomDouble(),
                (a, b) => a.Equals(b));
        }

        [Fact]
        public void RandomInt_Passes_Random_Sample_Test()
        {
            TestRandomSample(
                () => _sut.RandomInt(),
                (a, b) => a.Equals(b));
        }

        [Fact]
        public void RandomDecimal_Passes_Random_Sample_Test()
        {
            TestRandomSample(
                () => _sut.RandomDecimal(),
                (a, b) => a.Equals(b));
        }

        #endregion

        #region Helper Methods

        private void TestRandomSample<T>(Func<T> getFn, Func<T, T, bool> validationFn)
        {
            // I can believe that 1 random sampling comes up the same out of ... 10?
            // Any more than that and I call bullshit, your random function is fucked up

            int errorsHit = 0;
            int errorLimit = 1;
            int retryCount = 10;

            for (var i = 0; i < retryCount; i++)
            {
                var a = getFn();
                var b = getFn();

                if (validationFn(a, b))
                {
                    errorsHit++;
                }

                Assert.True(errorsHit <= errorLimit);
            }
        }

        #endregion
    }
}
