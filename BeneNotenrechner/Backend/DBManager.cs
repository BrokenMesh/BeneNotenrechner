using MySql.Data.MySqlClient;

namespace BeneNotenrechner.Backend
{
    public class DBManager 
    {
        private MySqlConnection db;
        private bool isStreamOpen = true;

        public static DBManager instance;
        public DBManager(string _server, string _userid, string _password, string _database) {
            instance = this;

            string _constr = $"server={_server};userid={_userid};password={_password};database={_database};";
            db = new MySqlConnection(_constr);
            db.Open();

            Console.WriteLine($"MySql version: {db.ServerVersion}");
        }

        public int CreateUser(string _username, string _password) {
            OpenStream();

            string _sql = "INSERT INTO tbl_users(username, tbl_users.password) VALUES (@username,  @password)";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@username", _username);
                _command.Parameters.AddWithValue("@password", _password);

                _command.ExecuteNonQuery();
            }

            CloseStream();
            return GetUser(_username);
        }

        public bool CheckPassword(int _userId, string _submitePassword) {
            OpenStream();

            string _sql = "SELECT tbl_users.password FROM tbl_users WHERE user_id = @userid";
            
            string? _userPassword = null;

            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@userid", _userId);

                using (MySqlDataReader _reader = _command.ExecuteReader()) {
                    while (_reader.Read()) {
                        _userPassword = _reader.GetString("password");
                    }
                }
            }

            CloseStream();
            return _userPassword == _submitePassword;
        }

        public int GetUser(string _username) {
            OpenStream();

            string _sql = "SELECT user_id FROM tbl_users WHERE username = @username";

            int _user_id = -1;

            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@username", _username);

                using (MySqlDataReader _reader = _command.ExecuteReader()) {
                    while (_reader.Read()) {
                        _user_id = _reader.GetInt32("user_id");
                    }
                }
            }

            CloseStream();
            return _user_id;
        }

        #region PROFILE_GETTERS
        public Profile? GetProfile(int _id_user, bool _resolveChildren) {
            OpenStream();

            int _id_profile = -1;

            string _sql = "SELECT profile_id FROM tbl_profile WHERE id_users = @id_user";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@id_user", _id_user);

                using (MySqlDataReader _reader = _command.ExecuteReader()) {
                    while (_reader.Read()) {
                        _id_profile = _reader.GetInt32("profile_id");
                    }
                }
            }

            Profile _profile = new Profile(_id_profile, _id_user);

            if (_resolveChildren) {
                _profile.superSubjects = GetSuperSubjects(_profile, true);
            }

            CloseStream();
            return _profile;
        }

        public List<SuperSubject> GetSuperSubjects(Profile _profile, bool _resolveChildren) {
            OpenStream();

            List<SuperSubject> _superSubjects = new List<SuperSubject>();

            int _id_profile = _profile.id_profile;

            string _sql = "SELECT * FROM tbl_supersubject WHERE id_profile = @id_profile";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@id_profile", _id_profile);

                using (MySqlDataReader _reader = _command.ExecuteReader()) {
                    while (_reader.Read()) {
                        _superSubjects.Add(new SuperSubject(
                            _reader.GetInt32("supersubject_id"),
                            _reader.GetInt32("id_profile"),
                            _reader.GetString("name"),
                            _reader.GetInt32("semester")));
                    }
                }
            }

            if (_resolveChildren) {
                foreach (SuperSubject _superSubject in _superSubjects) {
                    _superSubject.subjects = GetSubjects(_superSubject, true);
                }
            }

            CloseStream();
            return _superSubjects;
        }

        public List<Subject> GetSubjects(SuperSubject _superSubject, bool _resolveChildren) {
            OpenStream();

            List<Subject> _subjects = new List<Subject>();

            int _id_superSubject = _superSubject.id_supersubject;

            string _sql = "SELECT * FROM tbl_subject WHERE id_supersubject = @id_supersubject";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@id_supersubject", _id_superSubject);

                using (MySqlDataReader _reader = _command.ExecuteReader()) {
                    while (_reader.Read()) {
                        _subjects.Add(new Subject(
                            _reader.GetInt32("subject_id"),
                            _reader.GetInt32("id_supersubject"),
                            _reader.GetString("name")));
                    }
                }
            }

            if (_resolveChildren) {
                foreach (Subject _subject in _subjects) {
                    _subject.grades = GetGrades(_subject);
                }
            }

            CloseStream();
            return _subjects;
        }

        public List<Grade> GetGrades(Subject _subject) {
            OpenStream();

            List<Grade> _grades = new List<Grade>();

            int _id_subject = _subject.id_subject;

            string _sql = "SELECT * FROM tbl_subject WHERE id_subject = @id_subject";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@id_subject", _id_subject);

                using (MySqlDataReader _reader = _command.ExecuteReader()) {
                    while (_reader.Read()) {
                        _grades.Add(new Grade(
                            _reader.GetInt32("grade_id"),
                            _reader.GetFloat("grade"),
                            _reader.GetDateTime("date"),
                            _reader.GetInt32("id_subject")));
                    }
                }
            }

            CloseStream();
            return _grades;
        }

        #endregion PROFILE_GETTERS

        #region PROFILE_CREATION
        #endregion PROFILE_CREATION

        #region PROFILE_UPDATE
        #endregion PROFILE_UPDATE

        private void CloseStream() {
            isStreamOpen = true;
        }

        private void OpenStream() {
            while (!isStreamOpen) {
                Thread.Sleep(10);
            }
            isStreamOpen = false;
        }

    }
}
