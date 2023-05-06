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

        public SuperSubject? GetSuperSubject(int _id) { 
            foreach (SuperSubject _superSubject in superSubjects) {
                if(_superSubject.id_supersubject == _id) return _superSubject;
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
    }

    public class Grade
    {
        public int id_grade;
        public float grade;
        public DateTime date;
        public int id_subject;

        public Grade(int id_grade, float grade, DateTime date, int id_subject) {
            this.id_grade = id_grade;
            this.grade = grade;
            this.date = date;
            this.id_subject = id_subject;
        }
    }

}
