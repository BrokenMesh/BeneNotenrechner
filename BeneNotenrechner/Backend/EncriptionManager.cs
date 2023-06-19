using System.Security.Cryptography;

namespace BeneNotenrechner.Backend
{
    public class EncriptionManager
    {
        private static Random random = new Random();

        private const string randomStringChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        // TODO: Read from secure place
        private static string globalKey = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4";

        public static string GenRandomString(int _length) {
            string _output = "";
            
            for (int i = 0; i < _length; i++) {
                _output += randomStringChars[random.Next(randomStringChars.Length)];
            }

            return _output;
        }

        public static bool EncripFloat(float _value, User _user, out string _result) {
            return EncripString(_value.ToString(), _user, out _result);
        }

        public static bool DecripFloat(string _value, User _user, out float _result) {
            if (DecripString(_value, _user, out string _strResult)) {
                if (float.TryParse(_strResult, out _result)) {
                    return true;
                }
            }

            _result = 0;
            return false;
        }

        public static bool EncripString(string _value, User _user, out string _result) {
            byte[] _key = ConvertToKey(_user.Password, _user.Salt, globalKey);
            byte[] _iv = ByteArrayFromString(_user.Salt.Substring(0, 16));

            try {
                byte[] _encResult = EncryptStringToBytes_Aes(_value, _key, _iv);
                _result = StringFromByteArray(_encResult);
                return true;
            }
            catch (Exception) {
                _result = "";
                return false;
            }
        }

        public static bool DecripString(string _value, User _user, out string _result) {
            byte[] _key = ConvertToKey(_user.Password, _user.Salt, globalKey);
            byte[] _iv = ByteArrayFromString(_user.Salt.Substring(0, 16));

            try {
                byte[] _encValue = ByteArrayFromString(_value);
               _result = DecryptStringFromBytes_Aes(_encValue, _key, _iv);
                return true;
            }
            catch (Exception) {
                _result = "";
                return false;
            }
        }

        private static byte[] ConvertToKey(string _password, string _salt, string _globalKey) {
            byte[] _key;

            using (SHA256 _sha = SHA256.Create()) {
                byte[] _rawKey = ByteArrayFromString(_password + _globalKey + _salt);
                _key = _sha.ComputeHash(_rawKey);
            }

            return _key;
        }

        private static byte[] ByteArrayFromString(string _str) {
            byte[] _output = new byte[_str.Length];

            for (int i = 0; i < _str.Length; i++) {
                _output[i] = (byte)_str[i];
            }

            return _output;
        }

        private static string StringFromByteArray(byte[] _bytes) {
            string _output = "";

            for (int i = 0; i < _bytes.Length; i++) {
                _output += (char)_bytes[i];
            }

            return _output;
        }

        private static byte[] EncryptStringToBytes_Aes(string _value, byte[] _key, byte[] _iv) {
            byte[] encrypted;

            using (Aes _aesAlg = Aes.Create()) {
                _aesAlg.Key = _key;
                _aesAlg.IV = _iv;

                ICryptoTransform _encryptor = _aesAlg.CreateEncryptor(_aesAlg.Key, _aesAlg.IV);

                using (MemoryStream _msEncrypt = new MemoryStream()) {
                    using (CryptoStream _csEncrypt = new CryptoStream(_msEncrypt, _encryptor, CryptoStreamMode.Write)) {
                        using (StreamWriter _swEncrypt = new StreamWriter(_csEncrypt)) {
                            _swEncrypt.Write(_value);
                        }
                        encrypted = _msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        private static string DecryptStringFromBytes_Aes(byte[] _value, byte[] _key, byte[] _iv) {
            string _result = "";

            using (Aes _aesAlg = Aes.Create()) {
                _aesAlg.Key = _key;
                _aesAlg.IV = _iv;

                ICryptoTransform _decryptor = _aesAlg.CreateDecryptor(_aesAlg.Key, _aesAlg.IV);

                using (MemoryStream _msDecrypt = new MemoryStream(_value)) {
                    using (CryptoStream _csDecrypt = new CryptoStream(_msDecrypt, _decryptor, CryptoStreamMode.Read)) {
                        using (StreamReader _srDecrypt = new StreamReader(_csDecrypt)) {
                            _result = _srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return _result;
        }

    }
}
