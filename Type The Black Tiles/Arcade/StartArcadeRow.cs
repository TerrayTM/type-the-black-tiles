using System.Drawing;

namespace Type_The_Black_Tiles
{
    class StartArcadeRow
    {
        TileBase[] Tiles;
        int PositionY;
        bool Ended;

        public StartArcadeRow(int PositionY)
        {
            Ended = false;
            this.PositionY = PositionY; 
            Tiles = new TileBase[ArcadeHelperVaribles.RowSize];
            for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
            {
                Tiles[i] = new TileBase(new Point(i * ArcadeHelperVaribles.TileSize.Width, PositionY), ArcadeHelperVaribles.TileSize);
                Tiles[i].InitializeSprite(Color.Yellow, Color.White, null);
            }
        }

        public void Update()
        {
            if (!Ended)
            {
                if (PositionY >= 750)
                {
                    Ended = true;
                }
                else
                {
                    PositionY += ArcadeHelperVaribles.RowSpeed;
                    for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
                    {
                        Tiles[i].Position.Y = PositionY;
                    }
                }
            }
        }

        public void Draw(Graphics Handle)
        {
            if (!Ended)
            {
                for (int i = 0; i < ArcadeHelperVaribles.RowSize; ++i)
                {
                    Tiles[i].Draw(Handle);
                }
            }
        }
    }
}
