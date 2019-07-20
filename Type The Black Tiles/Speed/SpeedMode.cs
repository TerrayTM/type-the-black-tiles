using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Type_The_Black_Tiles
{
    class SpeedMode
    {
        List<SpeedRow> SpeedRowList;
        StartArcadeRow StartingRow;
        GameOverScreen SpeedModeEndScreen;
        ButtonTile MenuButton;
        ButtonTile RestartButton;
        Stopwatch TheStopWatch;
        public char Current;
        bool GameStart;
        bool GameOver;

        public SpeedMode()
        {
            SpeedRowList = new List<SpeedRow>();
            StartingRow = new StartArcadeRow(729 - ArcadeHelperVaribles.TileSize.Height);
            for (int i = 0; i < 26; ++i)
            {
                SpeedRowList.Add(new SpeedRow(729 - (ArcadeHelperVaribles.TileSize.Height * (i + 2)), i));
            }
            SpeedRowList.Reverse();
            Current = SpeedRowList[25].BlackTileCharacter;
            TheStopWatch = new Stopwatch();
            GameStart = false;
            GameOver = false;
        }

        private int GetIndex()
        {
            for (int i = SpeedRowList.Count - 1; i > 0; --i)
            {
                if (SpeedRowList[i].Triggered == false)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Refresh()
        {
            if (!GameStart)
            {
                GameStart = true;
                TheStopWatch.Start();
            }
            int Value = GetIndex();
            if (Value == 1)
            {
                SpeedRowList[1].TriggerTyped();
                Current = SpeedRowList[0].BlackTileCharacter;
            }
            else if (Value == -1)
            {
                EndGame(true);
            }
            else
            {
                SpeedRowList[Value].TriggerTyped();
                Current = SpeedRowList[GetIndex()].BlackTileCharacter;
            }
            for (int i = 0; i < SpeedRowList.Count; ++i)
            {
                SpeedRowList[i].Move = true;
            }
        }

        public void EndGame(bool Win)
        {
            if (GameOver)
            {
                return;
            }
            int Score = (int)TheStopWatch.ElapsedMilliseconds;
            if (Win)
            {
                GameData.UpdateScore(GameData.GameMode.Speed, Score);
            }
            SpeedModeEndScreen = new GameOverScreen("Speed Mode [High Score: " + (GameData.GetScore(GameData.GameMode.Speed) / 1000.0d).ToString() + "s]", (Score / 1000.0d).ToString() + " Seconds" + (Win ? " [WIN]" : " [FAIL]"));
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
                    SpeedHelperVaribles.RowSpeed = 3;
                    Restart();
                    return 0;
                }
                if (MenuButton.SpriteRegion.Contains(Location))
                {
                    SpeedHelperVaribles.RowSpeed = 3;
                    return 1;
                }
            }
            return 0;
        }

        public void Restart()
        {
            TheStopWatch.Reset();
            GameOver = false;
            SpeedRowList.Clear();
            StartingRow = new StartArcadeRow(729 - ArcadeHelperVaribles.TileSize.Height);
            for (int i = 0; i < 26; ++i)
            {
                SpeedRowList.Add(new SpeedRow(729 - (ArcadeHelperVaribles.TileSize.Height * (i + 2)), i));
            }
            SpeedRowList.Reverse();
            Current = SpeedRowList[25].BlackTileCharacter;
            GameStart = false;
        }

        public void Update()
        {
            if (!GameOver)
            {
                for (int i = 0; i < SpeedRowList.Count; ++i)
                {
                    if (SpeedRowList[i].PositionY > 750)
                    {
                        if (SpeedRowList[i].Triggered)
                        {
                            SpeedRowList.RemoveAt(i);
                        }
                        else
                        {
                            EndGame(false);
                        }
                    }
                    else
                    {
                        if (GameStart)
                        {
                            SpeedRowList[i].Update();
                        }
                    }
                }
                if (GameStart)
                {
                    StartingRow.Update();
                }
            }
        }

        public void Draw(Graphics Handle)
        {
            if (!GameOver)
            {
                StartingRow.Draw(Handle);
                for (int i = 0; i < SpeedRowList.Count; ++i)
                {
                    if (SpeedRowList[i].PositionY >= -150)
                    {
                        SpeedRowList[i].Draw(Handle);
                    }
                }
            }
            else
            {
                SpeedModeEndScreen.Draw(Handle);
                RestartButton.Draw(Handle);
                MenuButton.Draw(Handle);
            }
        }
    }
}
