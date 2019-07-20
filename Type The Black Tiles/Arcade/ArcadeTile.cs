using System.Drawing;

namespace Type_The_Black_Tiles
{
    class ArcadeTile : TileBase
    {
        public ArcadeTile(Point Position, string TheText) : base(Position, ArcadeHelperVaribles.TileSize)
        {
            if (!string.IsNullOrEmpty(TheText))
            {
                InitializeSprite(Color.Black, Color.White, TheText);
            }
            else
            {
                InitializeSprite(Color.White, Color.White, null);
            }
        }

        public void Triggered()
        {
            InitializeSprite(Color.Gray, Color.Gray, null);
        }
    }
}
