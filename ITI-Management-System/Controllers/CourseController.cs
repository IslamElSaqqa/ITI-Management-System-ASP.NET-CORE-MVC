using ITI_Management_System.ViewModels;
using ITIEntities;
using ITIEntities.Repo;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Management_System.Controllers
{
    public class CourseController : Controller
    {
        ITEntityRepo <Course> courseRepo;

        public CourseController(ITEntityRepo<Course> _courseRepo)
        {
            courseRepo = _courseRepo;
        }
        public IActionResult Index() {

            var model = courseRepo.GetAll();

            return View(model);
        }


        public IActionResult CourseDetails(int id)
        {
            var course = courseRepo.GetById(id);

            if (course == null)
                return NotFound();

            CourseDetailsVM vm = new CourseDetailsVM
            {
                CrsId = course.CrsId,
                CrsName = course.CrsName,
                Duration = course.Duration,
                Departments = course.Departments
                                    .Select(d => d.DeptName)
                                    .ToList(),

                Students = course.CourseStudents
                                 .Select(cs => cs.Student.Name)
                                 .ToList()
            };

            return View(vm);
        }

        // On Any Method not Post
        // Get or Any Method But not POST
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Course course)
        {
            courseRepo.Add(course);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = courseRepo.GetById(id.Value);

            if (model == null)
                return NotFound();

            return View(model);


        }
        [HttpPost]
        public IActionResult Edit(Course course, int id)
        {
            course.CrsId = id;
            courseRepo.Update(course);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = courseRepo.GetById(id.Value);

            if (model == null)
                return NotFound();

            return View(model);


        }
        [HttpPost]
        public IActionResult Delete(Course course, int id)
        {
            course.CrsId = id;
            courseRepo.Delete(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
