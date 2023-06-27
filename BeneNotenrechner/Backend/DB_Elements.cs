
namespace BeneNotenrechner.Backend
{

    public class Profile
    {
        public int id_profile;
        public int index;
        public int id_user;

        public List<SuperSubject> superSubjects;

        public Profile(int id_profile, int id_user)
        {
            this.id_profile = id_profile;
            this.id_user = id_user;
            superSubjects = new List<SuperSubject>();
        }

        public Profile(int id_profile, int index, int id_user) {
            this.id_profile = id_profile;
            this.index = index;
            this.id_user = id_user;
            superSubjects = new List<SuperSubject>();
        }

        public SuperSubject? GetSuperSubject(string _strID) {
            if (int.TryParse(_strID, out int _id)) {
                foreach (SuperSubject _superSubject in superSubjects) {
                    if (_superSubject.id_supersubject == _id) return _superSubject;
                }
            }
            return null;
        }

        public void CreateSuperSubject(string _name, User _user) {
            DBManager.instance.CreateSuperSubject(this, _name);
            superSubjects = DBManager.instance.GetSuperSubjectAll(this, _user, true);
        }
    }

    public class SuperSubject
    {
        public int id_supersubject;
        public int id_profile;
        public string name;
        public float subjectAverage;

        public List<Subject> subjects;

        public SuperSubject(int id_supersubject, int id_profile, string name) {
            this.id_supersubject = id_supersubject;
            this.id_profile = id_profile;
            this.name = name;
            subjects = new List<Subject>();
        }

        public Subject? GetSubject(string _strID) {
            if (int.TryParse(_strID, out int _id)) {
                foreach (Subject _subject in subjects) {
                    if (_subject.id_subject == _id) return _subject;
                }
            }
            return null;
        }

        public void CreateSubject(string _name, User _user) {
            DBManager.instance.CreateSubject(this, _name);
            subjects = DBManager.instance.GetSubjectAll(this, _user, true);
        }

        public void Update(string _name) {
            name = _name;
            DBManager.instance.UpdateSuperSubject(this);
        }

        public void Delete() {
            foreach (Subject _subject in subjects) {
                _subject.Delete();
            }
            DBManager.instance.DeleteSuperSubject(this);
        }

        public void EvaluateAverage() {
            float _total = 0;
            float _count = 0;

            foreach (Subject _s in subjects) {
                if (_s.gradeAverage == 0f) continue;
                _total += _s.gradeAverage;
                _count++;
            }

            subjectAverage = _total / _count;
        }
    }

    public class Subject
    {
        public int id_subject;
        public int id_supersubject;
        public string name;
        public float gradeAverage;

        public List<Grade> grades;

        public Subject(int id_subject, int id_supersubject, string name)
        {
            this.id_subject = id_subject;
            this.id_supersubject = id_supersubject;
            this.name = name;
            grades = new List<Grade>();
        }

        public Grade? GetGrade(string _strID) {
            if (int.TryParse(_strID, out int _id)) {
                foreach (Grade _grade in grades) {
                    if (_grade.id_grade == _id)
                        return _grade;
                }
            }
            return null;
        }

        public void Update(string _name) { 
            name = _name;
            DBManager.instance.UpdateSubject(this);
        }

        public void Delete() {
            foreach (Grade _grade in grades) {
                _grade.Delete();
            }
            DBManager.instance.DeleteSubject(this);
        }
        
        public void CreateGrade(string _gradeName, string _strGrade, string _strEvaluation, string _strDate, User _user) {
            float _grade;
            float _evaluation;
            DateTime _date;

            if (!float.TryParse(_strGrade, out _grade)) return;
            if (!float.TryParse(_strEvaluation, out _evaluation)) return;
            if (!DateTime.TryParse(_strDate, out _date)) return;

            _grade = Math.Clamp(_grade, 1, 6);

            DBManager.instance.CreateGrade(this, _grade, _evaluation, _date, _gradeName, _user);
            grades = DBManager.instance.GetGradeAll(this, _user);
        }

        public void EvaluateAverage() {
            float _totalEval = 0;
            float _total = 0;

            for (int i = 0; i < grades.Count; i++) {
                _totalEval += grades[i].evaluation;
            }

            for (int i = 0; i < grades.Count; i++) {
                float _weight = grades[i].evaluation / _totalEval;
                _total += grades[i].grade * _weight;
            }

            gradeAverage = _total;
        }

    }

    public class Grade
    {
        public int id_grade;
        public float grade;
        public float evaluation;
        public DateTime date;
        public string name;
        public int id_subject;

        public Grade(int id_grade, float grade, float evaluation, DateTime date, string name, int id_subject) {
            this.id_grade = id_grade;
            this.grade = grade;
            this.date = date;
            this.evaluation = evaluation;
            this.name = name;
            this.id_subject = id_subject;
        }

        public void Update(string _name, string _strGrade, string _strEvaluation, string _strDate, User _user) {
            float _grade;
            float _evaluation;
            DateTime _date;

            if (!float.TryParse(_strGrade, out _grade)) return;
            if (!float.TryParse(_strEvaluation, out _evaluation)) return;
            if (!DateTime.TryParse(_strDate, out _date)) return;

            name = _name;
            grade = _grade;
            evaluation = _evaluation;
            date = _date;

            DBManager.instance.UpdateGrade(this, _user);
        }

        public void Delete() {
            DBManager.instance.DeleteGrade(this);
        }
    }

}
