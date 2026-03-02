using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIEntities.Repo
{
        public class StudentRepo : ITEntityRepo<Student>
        {
        ITIContext context;

        public StudentRepo(ITIContext iticontext)
        {
            context = iticontext;
        }
        public List<Student> GetAll() {

                return context.Students.Include(s => s.Department).ToList();
            }

            public Student GetById(int id) { 
                return context.Students
                    .Include(s => s.StudentCourses)  
                        .ThenInclude(sc => sc.Course) 
                    .FirstOrDefault(s => s.ID == id);
        }

            public void Add(Student student)
            {
                context.Students.Add(student);
                context.SaveChanges();
            }

            public void Update(Student student)
            {
                context.Students.Update(student);
                context.SaveChanges();
            }

            public void Delete(int id)
            {
                context.Students.Remove(GetById(id));
                context.SaveChanges();
            }


    }
}
