namespace BeneNotenrechner.Backend
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public DateTime LastAuthentication { get; private set; }

        private Profile? profile;

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

        public Profile? GetProfile() {
            if (profile == null) {
                profile = DBManager.instance.GetProfile(Id, true);
            }

            return profile;
        }
    }
}
