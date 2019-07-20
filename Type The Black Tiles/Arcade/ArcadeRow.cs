using System.Drawing;

namespace Type_The_Black_Tiles
{
    class ArcadeRow
    {
        public int PositionY;
        public char BlackTileCharacter;
        ArcadeTile[] ArcadeTiles;
        int Index;
        public bool Triggered;

        public ArcadeRow(int PositionY)
        {
            this.PositionY = PositionY;
            Triggered = false;
            ArcadeTiles = new ArcadeTile[ArcadeHelperVaribles.RowSize];
            Index = ArcadeHelperVaribles.PRNG.Next(0, ArcadeHelperVaribles.RowSize);
            for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
            {
                if (i == Index)
                {
                    BlackTileCharacter = ArcadeHelperVaribles.Characters[ArcadeHelperVaribles.PRNG.Next(0, ArcadeHelperVaribles.Characters.Length)];
                    ArcadeTiles[i] = new ArcadeTile(new Point(i * ArcadeHelperVaribles.TileSize.Width, PositionY), BlackTileCharacter.ToString());
                }
                else
                {
                    ArcadeTiles[i] = new ArcadeTile(new Point(i * ArcadeHelperVaribles.TileSize.Width, PositionY), null);
                }
            }
        }

        public void Update()
        {
            PositionY += ArcadeHelperVaribles.RowSpeed;
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
