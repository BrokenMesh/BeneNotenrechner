namespace BeneNotenrechner.Backend
{

    public class Profile
    {
        public int id_profile;
        public int id_user;

        public List<SuperSubject> superSubjects;

        public Profile(int id_profile, int id_user)
        {
            this.id_profile = id_profile;
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
    }

    public class SuperSubject
    {
        public int id_supersubject;
        public int id_profile;
        public string name;
        public int semester;

        public List<Subject> subjects;

        public SuperSubject(int id_supersubject, int id_profile, string name, int semester) {
            this.id_supersubject = id_supersubject;
            this.id_profile = id_profile;
            this.name = name;
            this.semester = semester;
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
    }

    public class Subject
    {
        public int id_subject;
        public int id_supersubject;
        public string name;

        public List<Grade> grades;

        public Subject(int id_subject, int id_supersubject, string name)
        {
            this.id_subject = id_subject;
            this.id_supersubject = id_supersubject;
            this.name = name;
            grades = new List<Grade>();
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

        public void CreateGrade(string grade_Name, string grade_Grade, string grade_Date) {
            DBManager.instance.CreateGrade(this, float.Parse(grade_Grade), DateTime.Parse(grade_Date), grade_Name);
            grades = DBManager.instance.GetGradeAll(this);
        }
    }

    public class Grade
    {
        public int id_grade;
        public float grade;
        public DateTime date;
        public string name;
        public int id_subject;

        public Grade(int id_grade, float grade, DateTime date, string name, int id_subject) {
            this.id_grade = id_grade;
            this.grade = grade;
            this.date = date;
            this.name = name;
            this.id_subject = id_subject;
        }

        public void Delete() {
            DBManager.instance.DeleteGrade(this);
        }
    }

}
