using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIEntities.Repo
{
    
    public class CourseRepo : ITEntityRepo<Course>
    {
        ITIContext context;

        public CourseRepo(ITIContext iticontext)
        {
            context = iticontext;
        }
        public void Add(Course entity)
        {
            context.Courses.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Courses.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Course> GetAll()
        {
            return context.Courses.ToList();
        }

        public Course GetById(int id)
        {
           return context.Courses
                .Include(c => c.Departments)
                .Include(c => c.CourseStudents)
                    .ThenInclude(cs => cs.Student)
                .FirstOrDefault(c => c.CrsId == id);
        }

        public void Update(Course entity)
        {
            context.Courses.Update(entity);
            context.SaveChanges();
        }
    }
}
