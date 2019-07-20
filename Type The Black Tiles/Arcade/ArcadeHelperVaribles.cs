using System;
using System.Drawing;

namespace Type_The_Black_Tiles
{
    static class ArcadeHelperVaribles
    {
        public static char[] Characters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        public static int RowSize = 4;
        public static Random PRNG = new Random();
        public static Size TileSize = new Size(140, 160);
        public static int RowSpeed = 3;
    }
}
