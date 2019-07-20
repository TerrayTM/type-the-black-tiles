using System;
using System.Drawing;

namespace Type_The_Black_Tiles
{
    class ArithmeticRow
    {
        public int PositionY;
        public char BlackTileCharacter;
        ArcadeTile[] ArcadeTiles;
        int Index;
        public bool Triggered;

        public ArithmeticRow(int PositionY)
        {
            this.PositionY = PositionY;
            Triggered = false;
            ArcadeTiles = new ArcadeTile[ArcadeHelperVaribles.RowSize];
            Index = ArcadeHelperVaribles.PRNG.Next(0, ArcadeHelperVaribles.RowSize);
            for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
            {
                if (i == Index)
                {
                    string StringText = null;
                    int NumberTwo = 0;
                    int NumberOne = 0;
                    NumberOne = ArcadeHelperVaribles.PRNG.Next(-4, 4);
                    NumberTwo = ArcadeHelperVaribles.PRNG.Next(-4, 4);
                    BlackTileCharacter = char.Parse(Math.Abs(NumberOne + NumberTwo).ToString());
                    StringText = NumberOne.ToString() + " + " + NumberTwo.ToString();
                    ArcadeTiles[i] = new ArcadeTile(new Point(i * ArcadeHelperVaribles.TileSize.Width, PositionY),  "|" + StringText + "|");
                }
                else
                {
                    ArcadeTiles[i] = new ArcadeTile(new Point(i * ArcadeHelperVaribles.TileSize.Width, PositionY), null);
                }
            }
        }

        public void Update()
        {
            PositionY += ArithmeticHelperVaribles.RowSpeed;
            for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
            {
                ArcadeTiles[i].Position.Y = PositionY;
            }
        }

        public void TriggerTyped()
        {
            ArcadeTiles[Index].Triggered();
            Triggered = true;
        }

        public void Draw(Graphics Handle)
        {
            for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
            {
                ArcadeTiles[i].Draw(Handle);
            }
        }
    }
}
