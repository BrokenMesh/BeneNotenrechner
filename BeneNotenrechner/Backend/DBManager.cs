using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

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

        #region USER

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

        #endregion USER

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
            CloseStream();

            Profile _profile = new Profile(_id_profile, _id_user);

            if (_resolveChildren) {
                _profile.superSubjects = GetSuperSubjectAll(_profile, true);
            }

            return _profile;
        }

        public List<SuperSubject> GetSuperSubjectAll(Profile _profile, bool _resolveChildren) {
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
            CloseStream();

            if (_resolveChildren) {
                foreach (SuperSubject _superSubject in _superSubjects) {
                    _superSubject.subjects = GetSubjectAll(_superSubject, true);
                }
            }

            return _superSubjects;
        }

        public List<Subject> GetSubjectAll(SuperSubject _superSubject, bool _resolveChildren) {
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
            CloseStream();

            if (_resolveChildren) {
                foreach (Subject _subject in _subjects) {
                    _subject.grades = GetGradeAll(_subject);
                }
            }

            return _subjects;
        }

        public List<Grade> GetGradeAll(Subject _subject) {
            OpenStream();

            List<Grade> _grades = new List<Grade>();

            int _id_subject = _subject.id_subject;

            string _sql = "SELECT * FROM tbl_grade WHERE id_subject = @id_subject";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@id_subject", _id_subject);

                using (MySqlDataReader _reader = _command.ExecuteReader()) {
                    while (_reader.Read()) {
                        _grades.Add(new Grade(
                            _reader.GetInt32("grade_id"),
                            _reader.GetFloat("grade"),
                            _reader.GetDateTime("date"),
                            _reader.GetString("name"),
                            _reader.GetInt32("id_subject")));
                    }
                }
            }
            CloseStream();

            return _grades;
        }



        #endregion PROFILE_GETTERS

        #region PROFILE_CREATION
        public Profile? CreateProfile(int _user_id) {
            OpenStream();

            string _sql = "INSERT INTO `benenotenrechner_db`.`tbl_profile` (`id_users`) VALUES (@id_users);";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@id_users", _user_id);
                _command.ExecuteNonQuery();
            }

            CloseStream();
            return GetProfile(_user_id, false);
        }

        public void CreateSuperSubject(Profile _profile, string _name, int _semester) {
            OpenStream();

            string _sql = "INSERT INTO `benenotenrechner_db`.`tbl_supersubject` (`name`, `semester`, `id_profile`) VALUES (@name, @semester, @id_profile); ";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@name", _name);
                _command.Parameters.AddWithValue("@semester", _semester);
                _command.Parameters.AddWithValue("@id_profile", _profile.id_profile);
                _command.ExecuteNonQuery();
            }

            CloseStream();
        }

        public void CreateSubject(SuperSubject _superSubject, string _name) {
            OpenStream();

            string _sql = "INSERT INTO `benenotenrechner_db`.`tbl_subject` (`name`, `id_supersubject`) VALUES (@name, @id_supersubject); ";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@name", _name);
                _command.Parameters.AddWithValue("@id_supersubject", _superSubject.id_supersubject);
                _command.ExecuteNonQuery();
            }

            CloseStream();
        }

        public void CreateGrade(Subject _subject, float _grade, DateTime _date, string _name) {
            OpenStream();

            string _sql = "INSERT INTO `benenotenrechner_db`.`tbl_grade` (`grade`, `date`, `name`, `id_subject`) VALUES ( @grade, @date, @name, @id_subject); ";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@grade", _grade);
                _command.Parameters.AddWithValue("@date", _date);
                _command.Parameters.AddWithValue("@name", _name);
                _command.Parameters.AddWithValue("@id_subject", _subject.id_subject);
                _command.ExecuteNonQuery();
            }

            CloseStream();
        }

        #endregion PROFILE_CREATION

        #region PROFILE_UPDATE

        public void UpdateSubject(Subject _newSubject) {
            OpenStream();

            string _sql = "UPDATE `benenotenrechner_db`.`tbl_subject` SET `name` = @name WHERE `subject_id` = @subject_id;";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@name", _newSubject.name);
                _command.Parameters.AddWithValue("@subject_id", _newSubject.id_subject);
                _command.ExecuteNonQuery();
            }

            CloseStream();
        }


        #endregion PROFILE_UPDATE

        #region PROFILE_DELETE

        public void DeleteSubject(Subject _subject) {
            OpenStream();

            string _sql = "DELETE FROM `benenotenrechner_db`.`tbl_subject` WHERE `subject_id` = @subject_id;";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@subject_id", _subject.id_subject);
                _command.ExecuteNonQuery();
            }

            CloseStream();
        }

        internal void DeleteGrade(Grade _grade) {
            OpenStream();

            string _sql = "DELETE FROM `benenotenrechner_db`.`tbl_grade` WHERE `grade_id` = @grade_id;";
            using (MySqlCommand _command = new MySqlCommand(_sql, db)) {
                _command.Parameters.AddWithValue("@grade_id", _grade.id_grade);
                _command.ExecuteNonQuery();
            }

            CloseStream();
        }

        #endregion PROFILE_DELETE

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
