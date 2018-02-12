using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Utility.Core;
using System;
using System.Drawing;
using System.Drawing.Imaging;

/*
 * I have absolutely no idea how to test this class
 * It's largely just a bunch of System.Drawing black magic
 * 
 */

namespace Ivy.Captcha.Services
{
    public class CaptchaDrawingService : ICaptchaDrawingService
    {
        #region Variables & Constants

        private readonly ICaptchaImageHelper _imageHelper;

        private readonly IRandomizationHelper _rand;

        #endregion

        #region Constructor

        public CaptchaDrawingService(
            ICaptchaImageHelper imageHelper,
            IRandomizationHelper rand)
        {
            _imageHelper = imageHelper;
            _rand = rand;
        }

        #endregion

        #region Public Methods

        public void DrawCaptchaCode(int width, int height, string captchaCode, Graphics graph)
        {
            SolidBrush fontBrush = new SolidBrush(Color.Black);
            int fontSize = _imageHelper.GetFontSize(width, captchaCode.Length);
            Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            for (int i = 0; i < captchaCode.Length; i++)
            {
                fontBrush.Color = _imageHelper.GetRandomDeepColor();

                int shiftPx = fontSize / 6;

                float x = i * fontSize + _rand.RandomInt(-shiftPx, shiftPx) + _rand.RandomInt(-shiftPx, shiftPx);
                int maxY = height - fontSize;
                if (maxY < 0) maxY = 0;
                float y = _rand.RandomInt(0, maxY);

                graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
            }
        }

        public void DrawDisorderLine(int width, int height, Graphics graph)
        {
            Pen linePen = new Pen(new SolidBrush(Color.Black), 3);
            for (int i = 0; i < _rand.RandomInt(3, 5); i++)
            {
                linePen.Color = _imageHelper.GetRandomDeepColor();

                Point startPoint = new Point(_rand.RandomInt(0, width), _rand.RandomInt(0, height));
                Point endPoint = new Point(_rand.RandomInt(0, width), _rand.RandomInt(0, height));
                graph.DrawLine(linePen, startPoint, endPoint);

                Point bezierPoint1 = new Point(_rand.RandomInt(0, width), _rand.RandomInt(0, height));
                Point bezierPoint2 = new Point(_rand.RandomInt(0, width), _rand.RandomInt(0, height));

                graph.DrawBezier(linePen, startPoint, bezierPoint1, bezierPoint2, endPoint);
            }
        }

        public void UnsafeAdjustRippleEffect(Bitmap baseMap)
        {
            short nWave = 6;
            int nWidth = baseMap.Width;
            int nHeight = baseMap.Height;

            Point[,] pt = new Point[nWidth, nHeight];

            double newX, newY;
            double xo, yo;

            for (int x = 0; x < nWidth; ++x)
            {
                for (int y = 0; y < nHeight; ++y)
                {
                    xo = ((double)nWave * Math.Sin(2.0 * 3.1415 * (float)y / 128.0));
                    yo = ((double)nWave * Math.Cos(2.0 * 3.1415 * (float)x / 128.0));

                    newX = (x + xo);
                    newY = (y + yo);

                    if (newX > 0 && newX < nWidth)
                    {
                        pt[x, y].X = (int)newX;
                    }
                    else
                    {
                        pt[x, y].X = 0;
                    }


                    if (newY > 0 && newY < nHeight)
                    {
                        pt[x, y].Y = (int)newY;
                    }
                    else
                    {
                        pt[x, y].Y = 0;
                    }
                }
            }

            Bitmap bSrc = (Bitmap)baseMap.Clone();

            BitmapData bitmapData = baseMap.LockBits(new Rectangle(0, 0, baseMap.Width, baseMap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int scanline = bitmapData.Stride;

            IntPtr Scan0 = bitmapData.Scan0;
            IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = bitmapData.Stride - baseMap.Width * 3;

                int xOffset, yOffset;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        xOffset = pt[x, y].X;
                        yOffset = pt[x, y].Y;

                        if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                        {
                            p[0] = pSrc[(yOffset * scanline) + (xOffset * 3)];
                            p[1] = pSrc[(yOffset * scanline) + (xOffset * 3) + 1];
                            p[2] = pSrc[(yOffset * scanline) + (xOffset * 3) + 2];
                        }

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            baseMap.UnlockBits(bitmapData);
            bSrc.UnlockBits(bmSrc);
            bSrc.Dispose();
        }

        #endregion
    }
}
