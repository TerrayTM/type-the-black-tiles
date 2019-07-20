using System.IO;
using System.Security.Cryptography;

namespace Type_The_Black_Tiles
{
    static class AES
    {
        private static readonly byte[] DefaultKey = new byte[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        private static readonly byte[] DefaultIV = new byte[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };

        public static byte[] Encrypt(string PlainText)
        {
            return Encrypt(PlainText, DefaultKey, DefaultIV);
        }

        public static byte[] Encrypt(string PlainText, byte[] Key, byte[] IV)
        {
            byte[] Encrypted;
            using (RijndaelManaged RijndaelControl = new RijndaelManaged())
            {
                RijndaelControl.Key = Key;
                RijndaelControl.IV = IV;
                ICryptoTransform TheEncryptor = RijndaelControl.CreateEncryptor(RijndaelControl.Key, RijndaelControl.IV);
                using (MemoryStream MemoryStreamEncrypt = new MemoryStream())
                {
                    using (CryptoStream CryptoStreamEncrypt = new CryptoStream(MemoryStreamEncrypt, TheEncryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter SreamWriterEncrypt = new StreamWriter(CryptoStreamEncrypt))
                        {
                            SreamWriterEncrypt.Write(PlainText);
                        }
                        Encrypted = MemoryStreamEncrypt.ToArray();
                    }
                }
            }
            return Encrypted;
        }

        public static string Decrypt(byte[] Cipher)
        {
            return Decrypt(Cipher, DefaultKey, DefaultIV);
        }

        public static string Decrypt(byte[] Cipher, byte[] Key, byte[] IV)
        {
            string PlainText = null;
            using (RijndaelManaged RijndaelControl = new RijndaelManaged())
            {
                RijndaelControl.Key = Key;
                RijndaelControl.IV = IV;
                ICryptoTransform TheDecryptor = RijndaelControl.CreateDecryptor(RijndaelControl.Key, RijndaelControl.IV);
                using (MemoryStream MemoryStreamDecrypt = new MemoryStream(Cipher))
                {
                    using (CryptoStream CryptoStreamDecrypt = new CryptoStream(MemoryStreamDecrypt, TheDecryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader StreamReaderDecrypt = new StreamReader(CryptoStreamDecrypt))
                        {
                            PlainText = StreamReaderDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return PlainText;
        }
    }
}
