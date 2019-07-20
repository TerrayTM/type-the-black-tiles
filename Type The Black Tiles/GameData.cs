using System;
using System.IO;

namespace Type_The_Black_Tiles
{
    static class GameData
    {
        static string SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TTBT.Save");
        static int[] HighScores = new int[] { 0, 0, 0 };

        public enum GameMode
        {
            Arcade,
            Arithmetic,
            Speed
        };

        public static void LoadData()
        {
            if (!File.Exists(SavePath))
            {
                return;
            }
            try
            {
                string[] Data = AES.Decrypt(File.ReadAllBytes(SavePath)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (Data.Length != 3)
                {
                    return;
                }
                for (int Index = 0; Index < 3; ++Index)
                {
                    if (!int.TryParse(Data[Index], out int Current))
                    {
                        HighScores = new int[] { 0, 0, 0 };
                        return;
                    }
                    HighScores[Index] = Current;
                }
            }
            catch
            {
                return;
            }
        }

        public static void UpdateScore(GameMode Mode, int NewScore)
        {
            if (Mode == GameMode.Speed)
            {
                if (HighScores[(int)Mode] == 0)
                {
                    HighScores[(int)Mode] = NewScore;
                    return;
                }
                HighScores[(int)Mode] = Math.Min(HighScores[(int)Mode], NewScore);
            }
            else
            {
                HighScores[(int)Mode] = Math.Max(HighScores[(int)Mode], NewScore);
            }
        }

        public static int GetScore(GameMode Mode) 
        {
            return HighScores[(int)Mode];
        }

        public static void SaveData()
        {
            string Data = "";
            foreach (int Item in HighScores)
            {
                Data += Item + " ";
            }
            try
            {
                File.WriteAllBytes(SavePath, AES.Encrypt(Data));
            }
            catch
            {
                return;
            }
        }
    }
}
