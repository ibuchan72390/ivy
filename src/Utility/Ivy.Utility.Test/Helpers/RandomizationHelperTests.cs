using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.Utility.Core;
using System;
using System.Linq;
using Xunit;

namespace Ivy.Utility.Test.Helpers
{
    public class RandomizationHelperTests : TestBase<IRandomizationHelper>
    {
        #region Tests

        [Fact]
        public void RandomString_Generates_Random_String_To_Length()
        {
            const int length = 20;

            var result = Sut.RandomString(length);

            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void RandomString_Passes_Random_Sample_Test()
        {
            TestRandomSample(
                () => Sut.RandomString());
        }

        [Fact]
        public void RandomString_Doesnt_Create_Duplicates()
        {
            var origItems = Enumerable.Range(0, 10).Select(x => Sut.RandomString());
            var distinctItems = origItems.Distinct();

            Assert.Equal(origItems.Count(), distinctItems.Count());
        }

        [Fact]
        public void RandomDouble_Passes_Random_Sample_Test()
        {
            TestRandomSample(
                () => Sut.RandomDouble());
        }

        [Fact]
        public void RandomInt_Passes_Random_Sample_Test()
        {
            TestRandomSample(
                () => Sut.RandomInt());
        }

        [Fact]
        public void RandomInt_Can_Include_Max_Value()
        {
            const int min = 0;
            const int max = 1;

            // We should be able to get both in 50 Randoms
            for (var i = 0; i < 50; i++)
            {
                var result = Sut.RandomInt(min, max);

                if (result == max)
                    return;
            }

            throw new Exception($"Unable to get max value of RandomInt with 2 values. Min: {min} / Max: {max}");
        }

        [Fact]
        public void RandomDecimal_Passes_Random_Sample_Test()
        {
            TestRandomSample(
                () => Sut.RandomDecimal());
        }

        [Fact]
        public void RandomBoolean_Passes_Random_Sample_Test()
        {
            bool init = Sut.RandomBool();

            // The chances of 50 in a row are absurd
            for (var i = 0; i < 50; i++)
            {
                if (Sut.RandomBool() != init)
                    return;
            }

            throw new Exception($"Random boolean returned {init} 50 times in a row");
        }

        #endregion

        #region Helper Methods

        private void TestRandomSample<T>(Func<T> getFn)
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

                if (a.Equals(b))
                {
                    errorsHit++;
                }

                Assert.True(errorsHit <= errorLimit);
            }
        }

        #endregion
    }
}
