using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormPractice
{
    public class ScreenCopy
    {
        public static void AreaCapture(int width, int height, string outputFileName)
        {
            Size size = new Size(width, height);
            Bitmap bmp = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(bmp);

            gr.CopyFromScreen(0, 0, 0, 0, size);

            bmp.Save(outputFileName);
            MessageBox.Show("저장되었습니다", "캡처", MessageBoxButtons.OK);
            bmp.Dispose();
        }

        public static void FreeCapture(int x, int y, int width, int height, string outputFileName)
        {
            Size size = new Size(width, height);
            Bitmap bmp = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(bmp);

            gr.CopyFromScreen(x, y, 0, 0, size);

            bmp.Save(outputFileName);
            MessageBox.Show("저장되었습니다", "캡처", MessageBoxButtons.OK);
            bmp.Dispose();
        }

        public static void FullCapture(string outputFileName)
        {
            // 전체 화면 캡처
            Rectangle rect = Screen.PrimaryScreen.Bounds;

            int bitsPerPixel = Screen.PrimaryScreen.BitsPerPixel;
            PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
            if (bitsPerPixel <= 16)
            {
                pixelFormat = PixelFormat.Format16bppRgb565;
            }
            if (bitsPerPixel == 24)
            {
                pixelFormat = PixelFormat.Format24bppRgb;
            }

            Bitmap bmp = new Bitmap(rect.Width, rect.Height, pixelFormat);

            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
            }

            bmp.Save(outputFileName);
            MessageBox.Show("저장되었습니다", "캡처", MessageBoxButtons.OK);
            bmp.Dispose();
        }
    }
}
