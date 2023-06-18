namespace BeneNotenrechner.Backend
{
    public class EncriptionManager
    {
        private static Random random = new Random();

        private const string randomStringChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GenRandomString(int _length) {
            string _output = "";
            
            for (int i = 0; i < _length; i++) {
                _output += randomStringChars[random.Next(randomStringChars.Length)];
            }

            return _output;
        }



    }
}
