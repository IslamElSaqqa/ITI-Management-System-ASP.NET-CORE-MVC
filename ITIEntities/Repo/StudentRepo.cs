using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIEntities.Repo
{

    public interface IStudentRepo {
        public List<Student> GetAll();
        public Student GetById(int id);
        public void Add(Student student);
        public void Update(Student student);

        public void Delete(int id);
        }
        public class StudentRepo : IStudentRepo
    {
        ITIContext context = new ITIContext();

        public List<Student> GetAll() {

            return context.Students.Include(s => s.Department).ToList();
        }

        public Student GetById(int id) { 
            return context.Students.Find(id);
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
