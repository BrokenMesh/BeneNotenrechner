using System;
using System.IO;
using System.IO.Pipes;
using System.Security.Cryptography;
using System.Text;

namespace Aes_Example
{
    class AES_Encription_Demo
    {
        public static void Run() {
            string original = "Here is some data to encrypt!";

            string _globalKey = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4";

            string _pass1 = "7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31";
            string _pass2 = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4";

            string _salt1 = "7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31";
            string _salt2 = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4";

            byte[] _key1 = ConvertToKey(_pass1, _salt1, _globalKey);
            byte[] _key2 = ConvertToKey(_pass2, _salt2, _globalKey);
            byte[] _iv1 = Encoding.ASCII.GetBytes(_pass1, 0, 16);
            byte[] _iv2 = Encoding.ASCII.GetBytes(_pass1, 0, 16);

            byte[] encrypted1 = EncryptStringToBytes_Aes(original, _key1, _iv1);
            Console.WriteLine("Original key1:   {0}", original);

            byte[] encrypted2 = EncryptStringToBytes_Aes(original, _key2, _iv2);
            Console.WriteLine("Original key2:   {0}", original);

            try {
                string roundtrip1 = DecryptStringFromBytes_Aes(encrypted1, _key1, _iv1);
                Console.WriteLine("Round Trip pass1 key1: {0}", roundtrip1);
            }
            catch (Exception _e) {
                Console.WriteLine("Round Trip pass1 key2: Failed");
            }

            try {
                string roundtrip2 = DecryptStringFromBytes_Aes(encrypted1, _key2, _iv2);
                Console.WriteLine("Round Trip pass1 key1: {0}", roundtrip2);
            }
            catch (Exception _e) {
                Console.WriteLine("Round Trip pass1 key1: Failed");
            }


            try {
                string roundtrip1 = DecryptStringFromBytes_Aes(encrypted2, _key1, _iv1);
                Console.WriteLine("Round Trip pass2 key1: {0}", roundtrip1);
            }
            catch (Exception _e) {
                Console.WriteLine("Round Trip pass2 key2: Failed");
            }

            try {
                string roundtrip2 = DecryptStringFromBytes_Aes(encrypted2, _key2, _iv2);
                Console.WriteLine("Round Trip pass2 key2: {0}", roundtrip2);
            }
            catch (Exception _e) {
                Console.WriteLine("Round Trip pass2 key2: Failed");
            }

        }

        public static byte[] ConvertToKey(string _password, string _salt, string _globalKey) {
            byte[] _key;

            using (SHA256 _sha = SHA256.Create()) {
                byte[] _rawKey = Encoding.ASCII.GetBytes(_password + _globalKey + _salt);
                _key = _sha.ComputeHash(_rawKey);
            }

            return _key;
        }

        public static string ByteArrToStr(byte[] _in) {
            string _output = "";

            for (int i = 0; i < _in.Length; i++) {
                _output += _in[i] + ", ";
            }

            return _output;
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV) {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream()) {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV) {
            string plaintext = null;

            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText)) {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}