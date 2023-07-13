using designMode.observer;
using NumberGames;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace designMode
{
    public class DrawHelper
    {
        private int FontSize;
        public List<NumberInfo> NumberInfos;

        public DrawHelper(int fontsize)
        {
            FontSize = fontsize;
            NumberInfos = new List<NumberInfo>();
        }

        public Image DrawReady(int imgWeight, int imgheight,int level, int ms, PictureBox pic)
        {
            Image img = new Bitmap(imgWeight, imgheight);
            Font font = new Font("Arial", FontSize / 2, GraphicsUnit.Pixel);
             
            pic.Image = null;
            pic.Refresh();
            using (Graphics graphics = Graphics.FromImage(img))
            {
                int charLength = ms.ToString().Length;
                int wordspix = (charLength + 10) * (FontSize / 2)/2;
                int wordLength= imgWeight - wordspix;
                int offset = wordLength/2;

                int offset2 = (imgWeight - (FontSize / 4)) / 2;

                for (int i = 3; i > 0; i--)
                {
                    graphics.FillRectangle(Brushes.Black, 0, 0, imgWeight, imgheight);
                    graphics.DrawString($"第{level+1}关{ms}ms", font, Brushes.White, offset, imgheight / 4);
                    graphics.DrawString(i.ToString(), font, Brushes.White, offset2, imgheight / 2);
                    pic.Image = img;
                    pic.Refresh();

                    Thread.Sleep(1000);
                }


            }
            return img;
        }

        public Image DrawImg(Image img)
        {

            Font font = new Font("Arial", FontSize / 2, GraphicsUnit.Pixel);
            using (Graphics graphics = Graphics.FromImage(img))
            {
                graphics.FillRectangle(Brushes.Black, 0, 0, img.Width, img.Height);
                foreach (var item in NumberInfos)
                {
                    graphics.DrawString(item.Word, font, Brushes.White, item.BlockPoint);
                }

            }
            return img;
        }

        public Image DrawBlock(Image img)
        {
            using (Graphics graphics = Graphics.FromImage(img))
            {
                foreach (var item in NumberInfos)
                {
                    graphics.FillRectangle(Brushes.White, new Rectangle(item.BlockPoint, new Size(FontSize / 2, FontSize / 2)));

                }
            }
            return img;

        }



        public Image DrawFail(Image img, Point clickBoxPos)
        {

            Font font = new Font("Arial", FontSize / 2, GraphicsUnit.Pixel);
            using (Graphics graphics = Graphics.FromImage(img))
            {
                var nunmberinfo = PosHelper.getNumberInfromClick(clickBoxPos, NumberInfos, FontSize);
                graphics.FillRectangle(Brushes.Black, new Rectangle(nunmberinfo.BlockPoint, new Size(FontSize / 2, FontSize / 2)));
                graphics.DrawString(nunmberinfo.Word, font, Brushes.Red, clickBoxPos);
            }
            return img;
        }

        public Image DrawSuccess(Image img, Point RealPos)
        {
            using (Graphics graphics = Graphics.FromImage(img))
            {
                graphics.FillRectangle(Brushes.Black, new Rectangle(RealPos, new Size(FontSize / 2, FontSize / 2)));

            }
            return img;
        }






    }
}
