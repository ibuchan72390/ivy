using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Captcha.Core.Models;
using Ivy.Utility.Core;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

/*
 * To be honest, most of this project is some crazy System.Drawing black magic to me.
 * Original source was pulled from: https://code.msdn.microsoft.com/How-to-make-and-use-d0d1752a
 * 
 * I've converted it to a service-oriented library that specifically ensures we reuse existing pieces of code
 * within the Ivy infrastructure.  This will ensure we don't have to worry about Random leaks and can integrate
 * it with tests and service oriented architecture.
 */

namespace Ivy.Captcha.Services
{
    public class CaptchaGenerationService : ICaptchaGenerationService
    {
        #region Variables & Constants

        private readonly ICaptchaCodeGenerator _captchaCodeGen;
        private readonly ICaptchaImageHelper _imageHelper;
        private readonly ICaptchaDrawingService _captchaDrawer;

        #endregion

        #region Constructor

        public CaptchaGenerationService(
            ICaptchaCodeGenerator captchaCodeGen,
            ICaptchaImageHelper imageHelper,
            ICaptchaDrawingService captchaDrawer)
        {
            _captchaCodeGen = captchaCodeGen;
            _imageHelper = imageHelper;
            _captchaDrawer = captchaDrawer;
        }

        #endregion

        #region Public Methods

        public CaptchaResult GenerateCaptchaImage(int captchaCharLength, int width, int height)
        {
            if (height <= 0 || width <= 0)
            {
                throw new Exception("Both height and width need to be greater than 0!");
            }

            using (Bitmap baseMap = new Bitmap(width, height))
            using (Graphics graph = Graphics.FromImage(baseMap))
            {
                graph.Clear(_imageHelper.GetRandomLightColor());

                var captchaCode = _captchaCodeGen.GenerateCaptchaCode(captchaCharLength);

                _captchaDrawer.DrawCaptchaCode(width, height, captchaCode, graph);

                _captchaDrawer.DrawDisorderLine(width, height, graph);

                _captchaDrawer.UnsafeAdjustRippleEffect(baseMap);

                MemoryStream ms = new MemoryStream();

                baseMap.Save(ms, ImageFormat.Png);

                return new CaptchaResult
                {
                    CaptchaCode = captchaCode,
                    CaptchaByteData = ms.ToArray()
                };
            }

            #endregion
        }
    }
}
