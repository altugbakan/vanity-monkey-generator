using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Svg;

namespace VanityMonKeyGenerator
{
    public static class Drawing
    {
        public static void DrawMonKey(List<string> accessoryList, PictureBox pictureBox)
        {
            Image canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics graphics = Graphics.FromImage(canvas);

            foreach (string accessory in accessoryList)
            {
                var svg = Accessories.GetAccessorySvg(accessory);
                graphics.DrawImage(svg.Draw(pictureBox.Width, pictureBox.Height), 0, 0);
            }

            pictureBox.Image = canvas;
            canvas.Dispose();
        }
    }
}
