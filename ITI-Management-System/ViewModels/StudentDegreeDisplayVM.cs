namespace ITI_Management_System.ViewModels
{
    public class StudentDegreeDisplayVM
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public string StudentName { get; set; }
        public string CourseName { get; set; }

        public int? Degree { get; set; }
    }
}
