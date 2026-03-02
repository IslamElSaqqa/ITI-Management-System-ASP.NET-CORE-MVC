using ITIEntities;
using ITIEntities.Repo;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Management_System.Controllers
{
    public class StudentController: Controller
    {

        ITEntityRepo<Student> studentRepo;
        ITEntityRepo<Department> departmentRepo;
        ITEntityRepo<Course> courseRepo;
        ITIContext context;

        public StudentController(ITEntityRepo<Student> _studentrepo, ITEntityRepo<Department> _deptrepo, ITEntityRepo<Course> _courseRepo, ITIContext _context)
        {
            studentRepo = _studentrepo;
            departmentRepo = _deptrepo;
            courseRepo = _courseRepo;
            context = _context;
        }
        public IActionResult Index()
        {
            var model = studentRepo.GetAll();
            return View(model);
        }

        public IActionResult Details(int? id)
        {

            if (id == null) 
                return BadRequest();

            var model = studentRepo.GetById(id.Value); 
            if (model == null)
                return NotFound();
            return View(model);
        }

        // Get or Any Method But not POST
        public IActionResult Create()
        {
            var vm = new StudentFormVM
            {
                AllDepartments = departmentRepo.GetAll(),
                AllCourses = courseRepo.GetAll()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentFormVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AllDepartments = departmentRepo.GetAll();
                vm.AllCourses = courseRepo.GetAll();
                return View(vm);
            }

            // Create student
            var student = new Student
            {
                Name = vm.Name,
                Age = vm.Age,
                Deptno = vm.DeptId
            };

            context.Students.Add(student);
            context.SaveChanges();

            // Assign courses
            if (vm.SelectedCourseIds != null)
            {
                foreach (var crsId in vm.SelectedCourseIds)
                {
                    context.StudentCourses.Add(new StudentCourse
                    {
                        StdId = student.ID,
                        CrsId = crsId,
                        Degree = 0 
                    });
                }
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = studentRepo.GetById(id.Value);

            if (model == null)
                return NotFound();

            return View(model);


        }
      
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            

            var dbStudent = context.Students.Find(student.ID);
            if (dbStudent == null) return NotFound();

            
            dbStudent.Name = student.Name;
            dbStudent.Age = student.Age;
          
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var model = studentRepo.GetById(id.Value);

            if (model == null)
                return NotFound();

            return View(model);


        }
        [HttpPost]
        public IActionResult Delete(Student student, int id)
        {
          
            studentRepo.Delete(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
