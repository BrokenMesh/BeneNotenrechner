namespace BeneNotenrechner.Backend
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public DateTime LastAuthentication { get; private set; }
        public Profile? profile { get; private set; }

        public User(int id, string username)
        {
            Id = id;
            Username = username;

            SetAutenticationToNow();
        }

        public void SetAutenticationToNow()
        {
            LastAuthentication = DateTime.Now;
        }

        public void SetProfile(Profile _profile) {
            profile = _profile;
        }
    }
}
