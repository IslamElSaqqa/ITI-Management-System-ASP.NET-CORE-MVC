using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITI_Management_System.ViewModels
{
    public class UpdateDegreeVM
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int? Degree { get; set; }

        public List<SelectListItem> Students { get; set; }
        public List<SelectListItem> Courses { get; set; }
    }
}
