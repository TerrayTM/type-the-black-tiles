using System.Collections.Generic;
using System.Drawing;

namespace Type_The_Black_Tiles
{
    class Menu
    {
        List<ButtonTile[]> TileList;
        Size TheButtonSize;
        bool Leave;
        bool Enter;
        int Frames;

        public Menu()
        {
            Frames = 0;
            Enter = false;
            TileList = new List<ButtonTile[]>();
            Leave = false;
            Point TileOnePosition = new Point(0, 0);
            Point TileTwoPosition = new Point(280, 0);
            TheButtonSize = new Size(280, 243);
            for (int i = 0; i < 3; ++i)
            {
                switch (i)
                {
                    case 0:
                        TileList.Add(new ButtonTile[] { new ButtonTile(TileOnePosition, "Arcade", Color.White, Color.Black, TheButtonSize), new ButtonTile(TileTwoPosition, "Speed", Color.Black, Color.White, TheButtonSize) });
                        break;
                    case 1:
                        TileList.Add(new ButtonTile[] { new ButtonTile(TileOnePosition, "Arithmetic", Color.Black, Color.White, TheButtonSize), new ButtonTile(TileTwoPosition, "Donate", Color.White, Color.Black, TheButtonSize) });
                        break;
                    case 2:
                        TileList.Add(new ButtonTile[] { new ButtonTile(TileOnePosition, "By Terry Zheng", Color.White, Color.Black, TheButtonSize), new ButtonTile(TileTwoPosition, "Version 1.0", Color.Black, Color.White, TheButtonSize) });
                        break;
                }
                TileOnePosition.Y += 243;
                TileTwoPosition.Y += 243;
            }
        }

        public void MouseLeft()
        {
            for (int Index = 0; Index < TileList.Count; ++Index)
            {
                TileList[Index][0].MouseLeft();
                TileList[Index][1].MouseLeft();
            }
        }

        public int MouseClicked(Point Location)
        {
            for (int i = 0; i < TileList.Count; ++i)
            {
                if (TileList[i][0].SpriteRegion.Contains(Location))
                {
                    switch (i)
                    {
                        case 0:
                            Leave = true;
                            return 1;
                        case 1:
                            Leave = true;
                            return 3;
                        case 2:
                            return 5;
                    }
                }
                if (TileList[i][1].SpriteRegion.Contains(Location))
                {
                    switch (i)
                    {
                        case 0:
                            Leave = true;
                            return 2;
                        case 1:
                            System.Diagnostics.Process.Start("https://terrytm.com");
                            System.Windows.Forms.MessageBox.Show("Thank you for your support.", "Attention");
                            return 4;
                        case 2:
                            return 6;
                    }
                }
            }
            return -1;
        }

        public void MenuEnter()
        {
            Enter = true;
        }

        public void MouseMoveUpdate(Point Location)
        {
            for (int i = 0; i < TileList.Count; ++i)
            {
                if (TileList[i][0].SpriteRegion.Contains(Location))
                {
                    if (TileList[i][0].Selected != true)
                    {
                        for (int Index = 0; Index < TileList.Count; ++Index)
                        {
                            TileList[Index][0].MouseLeft();
                            TileList[Index][1].MouseLeft();
                        }
                        TileList[i][0].MouseEntered();
                        return;
                    }
                    return;
                }
                if (TileList[i][1].SpriteRegion.Contains(Location))
                {
                    if (TileList[i][1].Selected != true)
                    {
                        for (int Index = 0; Index < TileList.Count; ++Index)
                        {
                            TileList[Index][0].MouseLeft();
                            TileList[Index][1].MouseLeft();
                        }
                        TileList[i][1].MouseEntered();
                        return;
                    }
                    return;
                }
            }
        }

        public void Update(ref bool IsMenu, ref bool Initalized, ref bool CanClick)
        {
            if (Leave)
            {
                ++Frames;
                TileList[0][0].Position.X -= 12;
                TileList[0][1].Position.X += 12;
                if (Frames >= 20)
                {
                    TileList[1][0].Position.X -= 12;
                    TileList[1][1].Position.X += 12;
                }
                if (Frames >= 40)
                {
                    TileList[2][0].Position.X -= 12;
                    TileList[2][1].Position.X += 12;
                }
                if (Frames >= 80)
                {
                    TileList[0][0].Position.X = -612;
                    TileList[0][1].Position.X = 892;
                    TileList[1][0].Position.X = -612;
                    TileList[1][1].Position.X = 892;
                    TileList[2][0].Position.X = -612;
                    TileList[2][1].Position.X = 892;
                    Leave = false;
                    IsMenu = false;
                    Frames = 0;
                }
            }
            if (Enter)
            {
                ++Frames;
                if (TileList[0][0].Position.X != 0)
                {
                    TileList[0][0].Position.X += 12;
                    TileList[0][1].Position.X -= 12;
                }
                if (TileList[1][0].Position.X != 0)
                {
                    if (Frames >= 20)
                    {
                        TileList[1][0].Position.X += 12;
                        TileList[1][1].Position.X -= 12;
                    }
                }
                if (TileList[2][0].Position.X != 0)
                {
                    if (Frames >= 40)
                    {
                        TileList[2][0].Position.X += 12;
                        TileList[2][1].Position.X -= 12;
                    }
                }
                else
                {
                    Enter = false;
                    Initalized = false;
                    CanClick = true;
                    Frames = 0;
                }
            }
        }

        public void Draw(Graphics Handle)
        {
            for (int i = 0; i < TileList.Count; ++i)
            {
                TileList[i][0].Draw(Handle);
                TileList[i][1].Draw(Handle);
            }
        }
    }
}
