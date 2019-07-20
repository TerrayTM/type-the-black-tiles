using System.Collections.Generic;
using System.Drawing;

namespace Type_The_Black_Tiles
{
    class ArcadeMode
    {
        List<ArcadeRow> ArcadeRowList;
        StartArcadeRow StartingRow;
        GameOverScreen ArcadeModeEndScreen;
        ButtonTile MenuButton;
        ButtonTile RestartButton;
        public char Current;
        bool GameStart;
        bool GameOver;
        int Score;
        int Frames;

        public ArcadeMode()
        {
            Frames = 0;
            Score = 0;
            ArcadeRowList = new List<ArcadeRow>();
            StartingRow = new StartArcadeRow(729 - ArcadeHelperVaribles.TileSize.Height);
            for (int i = 0; i < 10; ++i)
            {
                ArcadeRowList.Add(new ArcadeRow(729 - (ArcadeHelperVaribles.TileSize.Height * (i + 2))));
            }
            ArcadeRowList.Reverse();
            Current = ArcadeRowList[9].BlackTileCharacter;
            GameStart = false;
            GameOver = false;
        }

        private int GetIndex()
        {
            for (int i = ArcadeRowList.Count - 1; i > 0; --i)
            {
                if (ArcadeRowList[i].Triggered == false)
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
            ArcadeRowList[GetIndex()].TriggerTyped();
            Current = ArcadeRowList[GetIndex()].BlackTileCharacter;
        }

        public void EndGame()
        {
            if (GameOver)
            {
                return;
            }
            GameData.UpdateScore(GameData.GameMode.Arcade, Score);
            ArcadeModeEndScreen = new GameOverScreen("Arcade Mode [High Score: " + GameData.GetScore(GameData.GameMode.Arcade) + "]", Score.ToString());
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
                    ArcadeHelperVaribles.RowSpeed = 3;
                    Restart();
                    return 0;
                }
                if (MenuButton.SpriteRegion.Contains(Location))
                {
                    ArcadeHelperVaribles.RowSpeed = 3;
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
            ArcadeRowList.Clear();
            StartingRow = new StartArcadeRow(729 - ArcadeHelperVaribles.TileSize.Height);
            for (int i = 0; i < 10; ++i)
            {
                ArcadeRowList.Add(new ArcadeRow(729 - (ArcadeHelperVaribles.TileSize.Height * (i + 2))));
            }
            ArcadeRowList.Reverse();
            Current = ArcadeRowList[9].BlackTileCharacter;
            GameStart = false;
        }

        public void Update()
        {
            if (!GameOver)
            {
                ++Frames;
                for (int i = 0; i < ArcadeRowList.Count; ++i)
                {
                    if (ArcadeRowList[i].PositionY > 750)
                    {
                        if (ArcadeRowList[i].Triggered)
                        {
                            ArcadeRowList.RemoveAt(i);
                            ArcadeRowList.Insert(0, new ArcadeRow(ArcadeRowList[0].PositionY - ArcadeHelperVaribles.TileSize.Height));
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
                            ArcadeRowList[i].Update();
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
                if (ArcadeHelperVaribles.RowSpeed < 10)
                {
                    ++ArcadeHelperVaribles.RowSpeed;
                }
                Frames = 0;
            }
        }

        public void Draw(Graphics Handle)
        {
            if (!GameOver)
            {
                StartingRow.Draw(Handle);
                for (int i = 0; i < ArcadeRowList.Count; ++i)
                {
                    if (ArcadeRowList[i].PositionY >= -150)
                    {
                        ArcadeRowList[i].Draw(Handle);
                    }
                }
            }
            else
            {
                ArcadeModeEndScreen.Draw(Handle);
                RestartButton.Draw(Handle);
                MenuButton.Draw(Handle);
            }
        }
    }
}
