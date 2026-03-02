using ITIEntities;
using ITIEntities.Models;
using ITIEntities.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ITI_Management_System.Controllers
{
    [Authorize (Roles = "Admin")]
    public class DepartmentController : Controller
    {

        // Index should Talk to DB using IEntity Repo
        ITEntityRepo<Department> deptRepo;
        ITIContext context;

        public DepartmentController(ITEntityRepo<Department> _deptRepo, ITIContext _context)
        {
            deptRepo = _deptRepo;
            context = _context;
        }
        public IActionResult Index()
        {
            var model = deptRepo.GetAll();
            return View(model);
        }

        public IActionResult ShowCourses(int id)
        {
            var model = context.Departments.Include(d => d.Courses).FirstOrDefault(d => d.DeptId == id);
            return View(model);
        }
        public IActionResult ManageDeptCourse(int id)
        {
            var model = context.Departments.Include(d => d.Courses).FirstOrDefault(d => d.DeptId == id);
            var allcourses = context.Courses.ToList();
            var coursesNotInDept = allcourses.Except(model.Courses).ToList();
            ViewBag.coursesNotInDept = coursesNotInDept;
            return View(model);
        }
        [HttpPost]
        public IActionResult ManageDeptCourse(int id, int[] coursestoremove, int[] coursestoadd)
        {
            var dept = context.Departments.Include(d => d.Courses).FirstOrDefault(d => d.DeptId == id);
            foreach (var courseId in coursestoremove)
            {
                Course c = dept.Courses.FirstOrDefault(c => c.CrsId == courseId);
                dept.Courses.Remove(c);
            }
            foreach (var item in coursestoadd)
            {
                Course c = context.Courses.FirstOrDefault(c => c.CrsId == item);
                dept.Courses.Add(c);
            }

            context.SaveChanges();
            return RedirectToAction(nameof(ManageDeptCourse), new { id = id });
        }

        public IActionResult Details(int? id) {

            if (id == null) 
                return BadRequest();
            
            var model = deptRepo.GetById(id.Value); 
            if (model == null)
                return NotFound();
            return View(model);
   
        }
      
        public IActionResult Create() {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dept) 
        {
            deptRepo.Add(dept);
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Edit(int? id) {
            if (id == null)
                return BadRequest();
            var model = deptRepo.GetById(id.Value);

            if (model == null)
                return NotFound();

            return View(model);

          
        }
        [HttpPost]
        // Either we pass id as a parameter and fill it with the dept then Remove the hidden input from the Form
        // or Keep the input as hidden but only pass the dept object to Edit Action
        public IActionResult Edit(Department dept, int id) {
            dept.DeptId = id;
            deptRepo.Update(dept);
            return RedirectToAction(nameof(Index));
        
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = deptRepo.GetById(id.Value);

            if (model == null)
                return NotFound();

            return View(model);


        }
        [HttpPost]
        public IActionResult Delete(Department dept, int id)
        {
            dept.DeptId = id;
            deptRepo.Delete(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
