namespace Student.Web.Api.Models
{
    public class Pupil
    {
        public Pupil(string studentId)
        {
            StudentId = studentId;
            Grades = new List<Grade>(); // Initializing Grades
        }

        public Pupil()
        {
            StudentId = string.Empty; // Initializing StudentId, or consider making it nullable
            Grades = new List<Grade>(); // Initializing Grades
        }

        public string StudentId { get; private set; }
        public string LastName { get; set; } = string.Empty;
        public string FirsName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string MiddleInitial 
        { 
            get 
            {
                return !string.IsNullOrEmpty(MiddleName) ? MiddleName.Substring(0,1) + "." : "";
            } 
        }

        public List<Grade> Grades { get; set; }
    }
}
