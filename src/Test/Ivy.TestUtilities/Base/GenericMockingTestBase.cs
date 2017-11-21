using Ivy.IoC.Core;
using Moq;
using System;

namespace Ivy.TestUtilities.Base
{
    public abstract class GenericMockingTestBase<TTestEntity> : GenericTestBase<TTestEntity>, IDisposable
        where TTestEntity : class
    {
        #region Helper Methods

        protected Mock<T> InitializeMoq<T>(IContainerGenerator containerGen)
            where T : class
        {
            var mock = new Mock<T>();
            containerGen.RegisterInstance<T>(mock);
            return mock;
        }

        #endregion
    }
}
