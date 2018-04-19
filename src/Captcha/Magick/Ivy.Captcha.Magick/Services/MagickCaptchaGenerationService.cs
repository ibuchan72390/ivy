using ImageMagick;
using Ivy.Captcha.Core.Interfaces.Models;
using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Captcha.Core.Models;
using Ivy.Utility.Core;
using System.Drawing;

namespace Ivy.Captcha.Magick.Services
{
    public class MagickCaptchaGenerationService : ICaptchaGenerationService
    {
        #region Variables & Constants

        private readonly IRandomizationHelper _rand;
        private readonly ICaptchaImageHelper _imgHelper;

        #endregion

        #region Constructor

        public MagickCaptchaGenerationService(
            IRandomizationHelper randomHelper,
            ICaptchaImageHelper imageHelper)
        {
            _rand = randomHelper;
            _imgHelper = imageHelper;
        }

        #endregion

        #region Public Methods

        public ICaptchaResult GenerateCaptchaImage(int captchaCharLength, int width, int height)
        {
            var code = _rand.RandomString(captchaCharLength);
            var fontSize = _imgHelper.GetFontSize(width, captchaCharLength);
            var bgColor = _imgHelper.GetRandomLightColor();

            // I think this thing is fucked up
            var magickBgColor = GenerateMagickColor(bgColor);

            using (MagickImage image = new MagickImage(magickBgColor, width, height))
            {

                // Setup the image for drawing
                var drawable = new Drawables()
                  .FontPointSize(fontSize)
                  .Font("Comic Sans");



                // Create background filled bezier curves
                for (int i = 0; i < _rand.RandomInt(5, 7); i++)
                {
                    var bezierColor = GenerateMagickColor(_imgHelper.GetRandomDeepColor());

                    PointD startPoint = new PointD(_rand.RandomInt(0, width), _rand.RandomInt(0, height));
                    PointD endPoint = new PointD(_rand.RandomInt(0, width), _rand.RandomInt(0, height));

                    PointD bezierPoint1 = new PointD(_rand.RandomInt(0, width), _rand.RandomInt(0, height));
                    PointD bezierPoint2 = new PointD(_rand.RandomInt(0, width), _rand.RandomInt(0, height));

                    var fillColor = GenerateMagickColor(_imgHelper.GetRandomDeepColor());
                    var strokeColor = GenerateMagickColor(_imgHelper.GetRandomDeepColor());

                    drawable.StrokeColor(strokeColor)
                            .FillColor(fillColor)
                            .Bezier(startPoint, bezierPoint1, bezierPoint2, endPoint);
                }


                // Draw each of the cahracters out leveraging some x / y positioning from our online code sample
                for (int i = 0; i < code.Length; i++)
                {
                    // Increase this value to decrease how far the letters sway from center
                    const int ShiftDivisor = 6;

                    int shiftPx = fontSize / ShiftDivisor;

                    float x = i * fontSize + (fontSize / 2) + _rand.RandomInt(-shiftPx, shiftPx) + _rand.RandomInt(-shiftPx, shiftPx);

                    int maxY = height;
                    int minY = fontSize;

                    if (maxY < minY) maxY = minY;

                    float y = _rand.RandomInt(minY, maxY);


                    var fillColor = GenerateMagickColor(_imgHelper.GetRandomDeepColor());
                    var strokeColor = GenerateMagickColor(_imgHelper.GetRandomDeepColor());

                    // Is there some way we can progressively move each text over???
                    // That way we can draw character by character or not?
                    drawable.StrokeColor(strokeColor)
                            .FillColor(fillColor)
                            .TextAlignment(TextAlignment.Center)
                            .Text(x, y, code[i].ToString());
                }


                // Create foreground bezier lines
                for (int i = 0; i < _rand.RandomInt(5, 7); i++)
                {
                    var bezierColor = GenerateMagickColor(_imgHelper.GetRandomDeepColor());

                    PointD startPoint = new PointD(_rand.RandomInt(0, width), _rand.RandomInt(0, height));
                    PointD endPoint = new PointD(_rand.RandomInt(0, width), _rand.RandomInt(0, height));

                    PointD bezierPoint1 = new PointD(_rand.RandomInt(0, width), _rand.RandomInt(0, height));
                    PointD bezierPoint2 = new PointD(_rand.RandomInt(0, width), _rand.RandomInt(0, height));

                    var fillColor = GenerateMagickColor(_imgHelper.GetRandomDeepColor());
                    var strokeColor = GenerateMagickColor(_imgHelper.GetRandomDeepColor());

                    drawable.FillOpacity(new Percentage(0))
                            .FillColor(strokeColor)
                            .StrokeWidth(3)
                            .StrokeColor(strokeColor)
                            .Bezier(startPoint, bezierPoint1, bezierPoint2, endPoint);
                }


                image.Draw(drawable);

                // ToByteArray will fail without 
                image.Format = MagickFormat.Png24;

                var b64str = image.ToBase64();

                return new CaptchaResult
                {
                    CaptchaCode = code,
                    CaptchaByteData = image.ToByteArray()
                };
            }

        }

        #endregion

        #region Helper Methods

        private MagickColor GenerateMagickColor(Color color)
        {
            var colorHex = ColorTranslator.ToHtml(color);

            return new MagickColor(colorHex);
        }

        #endregion
    }
}
