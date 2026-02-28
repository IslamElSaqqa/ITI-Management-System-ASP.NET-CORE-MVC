using ITIEntities;
using ITIEntities.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ITI_Management_System.Controllers
{
    public class DepartmentController : Controller
    {

        // Index should Talk to DB using IEntity Repo
        ITEntityRepo<Department> deptRepo = new DepartmentRepo();
        public IActionResult Index()
        {
            var model = deptRepo.GetAll();
            return View(model);
        }

        public IActionResult Details(int? id) {

            if (id == null) // No Id
                return BadRequest();
            
            var model = deptRepo.GetById(id.Value); // because id accepts null so we need to get the value from the id obj
            if (model == null)
                return NotFound();
            return View(model);
            // If we want to return JSON
            // return Json(model)

            // If we want to return String
            // return Content("Welcome to Details")
            // or new ContentResult({content = "Welcome to details"})

            // If we want to return Image or file
            // return File(path, extension, NameOfFileOnClient)

            //For Redirection
             //return Redirect() or RedirectPermenant
             //params = new {z =10, y = 10} => multiple params
             // return RedirectToAction(action, controller, params)


        }
        // Get or Any Method But not POST
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

    }
}
