using System.Security.Cryptography;

namespace SephirothTools
{
    public static class SephirothPlayerPrefsExtension
    {
        private static readonly System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        private static readonly System.IO.MemoryStream ms = new System.IO.MemoryStream();

        public static void Save<T>(string key, T value)
        {
            ms.Position = 0;
            ms.SetLength(0);
            formatter.Serialize(ms, value);
            UnityEngine.PlayerPrefs.SetString(key, System.Convert.ToBase64String(Encrypt(ms.ToArray())));
        }

        public static T Load<T>(string key, T defaultValue = default)
        {
            string loadValue = UnityEngine.PlayerPrefs.GetString(key, "");
            if (string.IsNullOrEmpty(loadValue))
            {
                return defaultValue;
            }

            byte[] bytes = Decrypt(System.Convert.FromBase64String(loadValue));
            ms.Position = 0;
            ms.Write(bytes, 0, bytes.Length);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }

        private const string AesIV = @"kgificpplgodckdi"; // Declare a random string of 16 single-byte characters.
        private const string AesKey = @"lfsaokfdsjhahfppjdfjshfsdgssdfas"; // Declare a random string of 32 single-byte characters.
        private const int KeySize = 256;
        private const int BlockSize = 128;

        private static byte[] Encrypt(byte[] byteValue)
        {
            return GetAes().CreateEncryptor().TransformFinalBlock(byteValue, 0, byteValue.Length);
        }

        private static byte[] Decrypt(byte[] byteValue)
        {
            return GetAes().CreateDecryptor().TransformFinalBlock(byteValue, 0, byteValue.Length);
        }

        private static AesManaged GetAes()
        {
            var aes = new AesManaged();
            aes.KeySize = KeySize;
            aes.BlockSize = BlockSize;
            aes.Mode = CipherMode.CBC;
            aes.IV = System.Text.Encoding.UTF8.GetBytes(AesIV);
            aes.Key = System.Text.Encoding.UTF8.GetBytes(AesKey);
            aes.Padding = PaddingMode.PKCS7;
            return aes;
        }
    }
}
