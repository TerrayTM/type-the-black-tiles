using System.Drawing;

namespace Type_The_Black_Tiles
{
    class GameOverScreen
    {
        protected Bitmap Sprite;

        public GameOverScreen(string ModeTitle, string Score)
        {
            Sprite = new Bitmap(560, 729);
            using (Graphics GraphicsController = Graphics.FromImage(Sprite))
            {
                using (SolidBrush ColorBrush = new SolidBrush(Color.Black))
                {
                    GraphicsController.FillRectangle(ColorBrush, 0, 0, Sprite.Width, Sprite.Height);
                    ColorBrush.Color = Color.White;
                    Font TextFont = new Font(new Font("Kohinoor Latin", 24), FontStyle.Bold);
                    SizeF TextSize = GraphicsController.MeasureString(Score, TextFont);
                    GraphicsController.DrawString(ModeTitle, TextFont, ColorBrush, new Point(15, 15));
                    GraphicsController.DrawString(Score, TextFont, ColorBrush, new Point(((int)((Sprite.Width / 2) - (TextSize.Width / 2))), (int)((Sprite.Height / 2) - (TextSize.Height / 2))));
                }
                GraphicsController.DrawRectangle(Pens.LightGray, 0, 0, Sprite.Width, Sprite.Height);
            }
        }

        public void Draw(Graphics Handle)
        {
            Handle.DrawImage(Sprite, Point.Empty);
        }
    }
}
