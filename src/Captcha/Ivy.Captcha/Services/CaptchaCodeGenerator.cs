using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Utility.Core;
using System.Text;

namespace Ivy.Captcha.Services
{
    public class CaptchaCodeGenerator : ICaptchaCodeGenerator
    {
        #region Variables & Constants

        const string Letters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private readonly IRandomizationHelper _rand;

        #endregion

        #region Constructor

        public CaptchaCodeGenerator(
            IRandomizationHelper rand)
        {
            _rand = rand;
        }

        #endregion

        #region Public Methods

        public string GenerateCaptchaCode(int charLength)
        {
            int maxRand = Letters.Length - 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < charLength; i++)
            {
                int index = _rand.RandomInt(0, maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }

        #endregion
    }
}
