using ITIEntities;
using ITIEntities.Repo;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Management_System.Controllers
{
    public class TestController : Controller
    {
        public string Display() {

            IStudentRepo s1 = new StudentRepo();
            s1.Add(new Student { Name = "Islam", Age = 30, Deptno = 100 });
            return "<h1>Hello, World!</h1>";
        }

        
    }
}
