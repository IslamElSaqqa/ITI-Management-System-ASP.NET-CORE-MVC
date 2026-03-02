namespace ITI_Management_System.ViewModels
{
    public class CourseDetailsVM
    {
        public int CrsId { get; set; }

        public string CrsName { get; set; }

        public int Duration { get; set; }

        public List<string> Departments { get; set; }

        public List<string> Students { get; set; }
    }
}
