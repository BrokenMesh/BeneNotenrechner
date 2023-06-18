﻿namespace BeneNotenrechner.Backend
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public DateTime LastAuthentication { get; private set; }

        private Profile? profile;

        public User(int id, string username, string password, string salt) {
            Id = id;
            Username = username;
            Password = password;
            Salt = salt;

            SetAutenticationToNow();
        }
        
        public void SetAutenticationToNow()
        {
            LastAuthentication = DateTime.Now;
        }

        public Profile? GetProfile() {
            if (profile == null) {
                profile = DBManager.instance.GetProfile(this, true);
            }

            return profile;
        }
    }
}
