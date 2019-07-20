using System.Collections.Generic;
using System.Drawing;

namespace Type_The_Black_Tiles
{
    class ArithmeticMode
    {
        List<ArithmeticRow> ArithmeticRowList;
        StartArcadeRow StartingRow;
        GameOverScreen ArithmeticModeEndScreen;
        ButtonTile MenuButton;
        ButtonTile RestartButton;
        public char Current;
        bool GameStart;
        bool GameOver;
        int Score;
        int Frames;

        public ArithmeticMode()
        {
            Frames = 0;
            Score = 0;
            ArithmeticRowList = new List<ArithmeticRow>();
            StartingRow = new StartArcadeRow(729 - ArcadeHelperVaribles.TileSize.Height);
            for (int i = 0; i < 10; ++i)
            {
                ArithmeticRowList.Add(new ArithmeticRow(729 - (ArcadeHelperVaribles.TileSize.Height * (i + 2))));
            }
            ArithmeticRowList.Reverse();
            Current = ArithmeticRowList[9].BlackTileCharacter;
            GameStart = false;
            GameOver = false;
        }

        private int GetIndex()
        {
            for (int i = ArithmeticRowList.Count - 1; i > 0; --i)
            {
                if (ArithmeticRowList[i].Triggered == false)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Refresh()
        {
            ++Score;
            if (!GameStart)
            {
                GameStart = true;
            }
            ArithmeticRowList[GetIndex()].TriggerTyped();
            Current = ArithmeticRowList[GetIndex()].BlackTileCharacter;
        }

        public void EndGame()
        {
            if (GameOver)
            {
                return;
            }
            GameData.UpdateScore(GameData.GameMode.Arithmetic, Score);
            ArithmeticModeEndScreen = new GameOverScreen("Arithmetic Mode [High Score: " + GameData.GetScore(GameData.GameMode.Arithmetic) + "]", Score.ToString());
            RestartButton = new ButtonTile(new Point(0, 486), "Restart", Color.Black, Color.White, new Size(280, 243));
            MenuButton = new ButtonTile(new Point(280, 486), "Menu", Color.Black, Color.White, new Size(280, 243));
            GameOver = true;
        }

        public int Click(Point Location)
        {
            if (GameOver)
            {
                if (RestartButton.SpriteRegion.Contains(Location))
                {
                    ArithmeticHelperVaribles.RowSpeed = 3;
                    Restart();
                    return 0;
                }
                if (MenuButton.SpriteRegion.Contains(Location))
                {
                    ArithmeticHelperVaribles.RowSpeed = 3;
                    return 1;
                }
            }
            return 0;
        }

        public void Restart()
        {
            Frames = 0;
            GameOver = false;
            Score = 0;
            ArithmeticRowList.Clear();
            StartingRow = new StartArcadeRow(729 - ArcadeHelperVaribles.TileSize.Height);
            for (int i = 0; i < 10; ++i)
            {
                ArithmeticRowList.Add(new ArithmeticRow(729 - (ArcadeHelperVaribles.TileSize.Height * (i + 2))));
            }
            ArithmeticRowList.Reverse();
            Current = ArithmeticRowList[9].BlackTileCharacter;
            GameStart = false;
        }

        public void Update()
        {
            if (!GameOver)
            {
                ++Frames;
                for (int i = 0; i < ArithmeticRowList.Count; ++i)
                {
                    if (ArithmeticRowList[i].PositionY > 750)
                    {
                        if (ArithmeticRowList[i].Triggered)
                        {
                            ArithmeticRowList.RemoveAt(i);
                            ArithmeticRowList.Insert(0, new ArithmeticRow(ArithmeticRowList[0].PositionY - ArcadeHelperVaribles.TileSize.Height));
                        }
                        else
                        {
                            EndGame();
                        }
                    }
                    else
                    {
                        if (GameStart)
                        {
                            ArithmeticRowList[i].Update();
                        }
                    }
                }
                if (GameStart)
                {
                    StartingRow.Update();
                }
            }
            if (Frames >= 1000)
            {
                if (ArithmeticHelperVaribles.RowSpeed != 10)
                {
                    ++ArithmeticHelperVaribles.RowSpeed;
                }
                Frames = 0;
            }
        }

        public void Draw(Graphics Handle)
        {
            if (!GameOver)
            {
                StartingRow.Draw(Handle);
                for (int i = 0; i < ArithmeticRowList.Count; ++i)
                {
                    if (ArithmeticRowList[i].PositionY >= -150)
                    {
                        ArithmeticRowList[i].Draw(Handle);
                    }
                }
            }
            else
            {
                ArithmeticModeEndScreen.Draw(Handle);
                RestartButton.Draw(Handle);
                MenuButton.Draw(Handle);
            }
        }
    }
}
