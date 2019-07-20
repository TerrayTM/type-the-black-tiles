using System.Drawing;

namespace Type_The_Black_Tiles
{
    class ButtonTile : TileBase
    {
        public Rectangle SpriteRegion;
        string TheText;
        Color TileColor;
        Color TileTextColor;
        public bool Selected;

        public ButtonTile(Point Position, string TheText, Color TileColor, Color TileTextColor, Size TheSize) : base(Position, TheSize)
        {
            InitializeSprite(TileColor, TileTextColor, TheText);
            SpriteRegion = new Rectangle(Position.X, Position.Y, 280, 243);
            this.TileTextColor = TileTextColor;
            this.TileColor = TileColor;
            this.TheText = TheText;
            Selected = false;
        }

        public void MouseEntered()
        {
            InitializeSprite(Color.Gray, TileTextColor, TheText);
            Selected = true;
        }

        public void MouseLeft()
        {
            InitializeSprite(TileColor, TileTextColor, TheText);
            Selected = false;
        }
    }
}
