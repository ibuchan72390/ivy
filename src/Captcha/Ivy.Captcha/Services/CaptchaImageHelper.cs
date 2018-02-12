using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Utility.Core;
using System;
using System.Drawing;

namespace Ivy.Captcha.Services
{
    public class CaptchaImageHelper : ICaptchaImageHelper
    {
        #region Variables & Constants

        private readonly IRandomizationHelper _rand;

        #endregion

        #region Constructor

        public CaptchaImageHelper(
            IRandomizationHelper rand)
        {
            _rand = rand;
        }

        #endregion

        #region Public Methods

        public int GetFontSize(int imageWidth, int captchCodeCount)
        {
            var averageSize = imageWidth / captchCodeCount;

            return Convert.ToInt32(averageSize);
        }

        public Color GetRandomDeepColor()
        {
            return GetRandomColor(true);
        }

        public Color GetRandomLightColor()
        {
            return GetRandomColor(false);
        }

        #endregion

        #region Helper Methods

        private Color GetRandomColor(bool dark)
        {
            int low, high;

            if (dark) {
                low = 0;
                high = 160;
            } else {
                low = 180;
                high = 255;
            }

            return Color.FromArgb(_rand.RandomInt(low, high),
                                  _rand.RandomInt(low, high),
                                  _rand.RandomInt(low, high));
        }

        #endregion
    }
}
