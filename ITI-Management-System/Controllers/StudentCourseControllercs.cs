using ITI_Management_System.ViewModels;
using ITIEntities;
using ITIEntities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize (Roles = "Admin")]
public class StudentCourseController : Controller
{
    private readonly ITIContext _context;

    public StudentCourseController(ITIContext context)
    {
        _context = context;
    }


    
    public IActionResult Index()
    {
        var data = _context.Set<StudentCourse>()
            .Select(sc => new StudentDegreeDisplayVM
            {
                StudentId = sc.StdId,
                CourseId = sc.CrsId,
                StudentName = sc.Student.Name,
                CourseName = sc.Course.CrsName,
                Degree = sc.Degree
            })
            .ToList();

        return View(data);
    }


    // GET: Show Form
    public IActionResult UpdateDegree(int studentId, int courseId)
    {
        var sc = _context.Set<StudentCourse>()
            .FirstOrDefault(x => x.StdId == studentId && x.CrsId == courseId);

        if (sc == null)
            return NotFound();

        var vm = new UpdateDegreeVM
        {
            StudentId = sc.StdId,
            CourseId = sc.CrsId,
            Degree = sc.Degree,

            Students = _context.Students
                .Select(s => new SelectListItem
                {
                    Value = s.ID.ToString(),
                    Text = s.Name
                }).ToList(),

            Courses = _context.Courses
                .Select(c => new SelectListItem
                {
                    Value = c.CrsId.ToString(),
                    Text = c.CrsName
                }).ToList()
        };

        return View(vm);
    }

    // POST: Save Degree
    [HttpPost]
    public IActionResult UpdateDegree(UpdateDegreeVM model)
    {
        var sc = _context.Set<StudentCourse>()
            .FirstOrDefault(x => x.StdId == model.StudentId
                              && x.CrsId == model.CourseId);

        if (sc == null)
        {
            sc = new StudentCourse
            {
                StdId = model.StudentId,
                CrsId = model.CourseId,
                Degree = model.Degree
            };

            _context.Add(sc);
        }
        else
        {
            sc.Degree = model.Degree;
        }

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}