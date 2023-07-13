using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGames
{
    public static class PosHelper
    {

        public static NumberInfo getNumberInfromClick(Point clickPos, List<NumberInfo> NumberInfos, int fontsize)
        {

            foreach (var item in NumberInfos)
            {
                if (IsPosInBox(clickPos, item.BlockPoint, fontsize))
                {

                    return item;
                }
            }

            return null;

        }

         

        private static Point getRightBottomPos(Point leftTopPos, int fontsize)
        {
            int area = fontsize / 2;
            return new Point(leftTopPos.X + area, leftTopPos.Y + area);

        }

        private static bool IsPosInBox(Point pos, Point leftTopPos, int fontsize)
        {
            var rightBottom = getRightBottomPos(leftTopPos, fontsize);
            if (pos.X >= leftTopPos.X && pos.X <= rightBottom.X)
            {
                if (pos.Y >= leftTopPos.Y && pos.Y <= rightBottom.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
