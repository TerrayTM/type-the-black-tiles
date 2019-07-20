using System;
using System.Drawing;
using System.Windows.Forms;

namespace Type_The_Black_Tiles
{
    public partial class Main : Form
    {
        bool Initalized;
        int GameModeIndex;
        bool IsMenu;
        bool CanClick; 
        Bitmap BackBuffer;
        Menu GameMenu;
        ArcadeMode ArcadeGame;
        SpeedMode SpeedGame;
        ArithmeticMode ArithmeticGame;
        
        public Main()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            ClientSize = new Size(560, 729);
            BackBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            GameModeIndex = -1;
            Initalized = false;
            IsMenu = true;
            GameMenu = new Menu();
            CanClick = true;
            GameData.LoadData();
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics GraphicsControl = Graphics.FromImage(BackBuffer))
            {
                GraphicsControl.Clear(Color.Transparent);
                if (Initalized) 
                {
                    switch (GameModeIndex)
                    {
                        case 1:
                            ArcadeGame.Draw(GraphicsControl);
                            break;
                        case 2:
                            SpeedGame.Draw(GraphicsControl);
                            break;
                        case 3:
                            ArithmeticGame.Draw(GraphicsControl);
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                    }
                }
                if (IsMenu)
                {
                    GameMenu.Draw(GraphicsControl);
                }
                e.Graphics.DrawImage(BackBuffer, Point.Empty);
            }
        }

        private void Updater_Tick(object sender, EventArgs e)
        {
            if (IsMenu)
            {
                GameMenu.MouseMoveUpdate(PointToClient(Cursor.Position));
                GameMenu.Update(ref IsMenu, ref Initalized, ref CanClick);
            }
            else
            {
                switch (GameModeIndex)
                {
                    case 1:
                        ArcadeGame.Update();
                        break;
                    case 2:
                        SpeedGame.Update();
                        break;
                    case 3:
                        ArithmeticGame.Update();
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }
            }
            Invalidate();
        }

        private void Main_MouseLeave(object sender, EventArgs e)
        {
            if (IsMenu)
            {
                GameMenu.MouseLeft();
            }
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            if (CanClick)
            {
                switch (GameMenu.MouseClicked(e.Location))
                {
                    case 1:
                        GameModeIndex = 1;
                        ArcadeGame = new ArcadeMode();
                        Initalized = true;
                        CanClick = false;
                        break;
                    case 2:
                        GameModeIndex = 2;
                        SpeedGame = new SpeedMode();
                        Initalized = true;
                        CanClick = false;
                        break;
                    case 3:
                        GameModeIndex = 3;
                        ArithmeticGame = new ArithmeticMode();
                        Initalized = true;
                        CanClick = false;
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }
            }
            else if (!IsMenu)
            {
                switch (GameModeIndex)
                {
                    case 1:
                        if (ArcadeGame.Click(e.Location) == 1)
                        {
                            IsMenu = true;
                            GameMenu.MenuEnter();
                        }
                        break;
                    case 2:
                        if (SpeedGame.Click(e.Location) == 1)
                        {
                            IsMenu = true;
                            GameMenu.MenuEnter();
                        }
                        break;
                    case 3:
                        if (ArithmeticGame.Click(e.Location) == 1)
                        {
                            IsMenu = true;
                            GameMenu.MenuEnter();
                        }
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }
            }
        }

        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsMenu)
            {
                switch (GameModeIndex)
                {
                    case 1:
                        if (e.KeyChar == ArcadeGame.Current)
                        {
                            ArcadeGame.Refresh();
                        }
                        else
                        {
                            ArcadeGame.EndGame();
                        }
                        break;
                    case 2:
                        if (e.KeyChar == SpeedGame.Current)
                        {
                            SpeedGame.Refresh();
                        }
                        else
                        {
                            SpeedGame.EndGame(false);
                        }
                        break;
                    case 3:
                        if (e.KeyChar == ArithmeticGame.Current)
                        {
                            ArithmeticGame.Refresh();
                        }
                        else
                        {
                            ArithmeticGame.EndGame();
                        }
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameData.SaveData();
        }
    }
}
