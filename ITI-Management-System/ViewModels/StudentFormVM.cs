using ITIEntities.Models;

public class StudentFormVM
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    
    public int DeptId { get; set; }
    public List<Department> AllDepartments { get; set; }

    // For courses selection
    public List<int> SelectedCourseIds { get; set; }
    public List<Course> AllCourses { get; set; }
}