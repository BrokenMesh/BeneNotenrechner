namespace BeneNotenrechner.Backend
{
    public class User
    {
        public int Id { get; private set; } 
        public string Username { get; private set; }
        public uint Token { get; private set; }

        public User(int id, string username, uint token) {
            Id = id;
            Username = username;
            Token = token;
        }
    }
}
