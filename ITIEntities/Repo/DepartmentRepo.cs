using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIEntities.Repo
{
    public class DepartmentRepo : ITEntityRepo<Department>
    {
        ITIContext context = new ITIContext();
        public void Add(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }

        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return context.Departments.Find(id);
        }

        public void Update(Department department)
        {
            context.Departments.Update(department);
            context.SaveChanges();
        }

        public void Delete(int id) 
        {
            context.Departments.Remove(GetById(id));
            context.SaveChanges();
        }
    }
}
