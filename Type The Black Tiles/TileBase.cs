using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Type_The_Black_Tiles
{
    class TileBase
    {
        protected Bitmap Sprite;
        public Point Position;
        protected Size SpriteSize;

        public TileBase(Point Position, Size SpriteSize)
        {
            this.SpriteSize = SpriteSize;
            this.Position = Position;
            Sprite = new Bitmap(SpriteSize.Width, SpriteSize.Height);
        }

        public virtual void InitializeSprite(Color TileColor, Color TileTextColor, string TheText)
        {
             if (TheText == null && TheText == "")
             {
                 using (Graphics GraphicsController = Graphics.FromImage(Sprite))
                 {
                     using (SolidBrush ColorBrush = new SolidBrush(TileColor))
                     {
                         GraphicsController.FillRectangle(ColorBrush, 0, 0, Sprite.Width, Sprite.Height);
                     }
                     GraphicsController.DrawRectangle(new Pen(Color.LightGray, 1f), 0, 0, Sprite.Width, Sprite.Height);
                 }
             }
             else
             {
                 using (Graphics GraphicsController = Graphics.FromImage(Sprite))
                 {
                     using (SolidBrush ColorBrush = new SolidBrush(TileColor))
                     {
                         GraphicsController.FillRectangle(ColorBrush, 0, 0, Sprite.Width, Sprite.Height);
                         ColorBrush.Color = TileTextColor;
                         Font TextFont = new Font(new Font("Kohinoor Latin", 24), FontStyle.Bold);
                         SizeF TextSize = GraphicsController.MeasureString(TheText, TextFont);
                         GraphicsController.DrawString(TheText, TextFont, ColorBrush, new Point(((int)((SpriteSize.Width / 2) - (TextSize.Width / 2))), (int)((SpriteSize.Height / 2) - (TextSize.Height / 2))));
                     }
                     GraphicsController.DrawRectangle(new Pen(Color.LightGray, 1f), 0, 0, Sprite.Width, Sprite.Height);
                 }
             }
        }

        public void Draw(Graphics Handle)
        {
            Handle.DrawImage(Sprite, Position);
        }
    }
}
