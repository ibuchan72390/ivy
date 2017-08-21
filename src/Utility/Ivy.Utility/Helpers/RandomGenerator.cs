using System;
using Ivy.Utility.Core.Helpers;
using Ivy.Utility.Core;

namespace Ivy.Utility.Helpers
{
    public class RandomGenerator : IRandomGenerator
    {
        #region Variables & Constants

        private readonly Random _rand;

        #endregion

        #region Constructor

        public RandomGenerator(
            IClock clock)
        {
            // We're going to use UtcNow here in order to guarantee that we are time zone irrelevant
            // Now could feasibly be duplicated if the app was instantiated in multiple time zones
            _rand = new Random((int)clock.UtcNow.Ticks);
        }

        #endregion

        #region Public Methods

        public Random GetRandom()
        {
            return _rand;
        }

        #endregion
    }
}
