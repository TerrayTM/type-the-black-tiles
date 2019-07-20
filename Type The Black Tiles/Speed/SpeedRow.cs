using System.Drawing;

namespace Type_The_Black_Tiles
{
    class SpeedRow
    {
        public int PositionY;
        public char BlackTileCharacter;
        ArcadeTile[] SpeedTiles;
        int Index;
        int MoveAmount;
        public bool Move;
        public bool Triggered;

        public SpeedRow(int PositionY, int CharacterIndex)
        {
            Move = false;
            MoveAmount = 0;
            this.PositionY = PositionY;
            Triggered = false;
            SpeedTiles = new ArcadeTile[ArcadeHelperVaribles.RowSize];
            Index = ArcadeHelperVaribles.PRNG.Next(0, ArcadeHelperVaribles.RowSize);
            for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
            {
                if (i == Index)
                {
                    BlackTileCharacter = SpeedHelperVaribles.Characters[CharacterIndex];
                    SpeedTiles[i] = new ArcadeTile(new Point(i * ArcadeHelperVaribles.TileSize.Width, PositionY), BlackTileCharacter.ToString());
                }
                else
                {
                    SpeedTiles[i] = new ArcadeTile(new Point(i * ArcadeHelperVaribles.TileSize.Width, PositionY), null);
                }
            }
        }

        public void Update()
        {
            if (Move && MoveAmount != 8)
            {
                for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
                {
                    SpeedTiles[i].Position.Y += 20;
                }
                PositionY += 20;
                ++MoveAmount;
            }
            else if (MoveAmount == 8)
            {
                Move = false;
                MoveAmount = 0;
            }
        }

        public void TriggerTyped()
        {
            SpeedTiles[Index].Triggered();
            Triggered = true;
        }

        public void Draw(Graphics Handle)
        {
            for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
            {
                SpeedTiles[i].Draw(Handle);
            }
        }
    }
}
