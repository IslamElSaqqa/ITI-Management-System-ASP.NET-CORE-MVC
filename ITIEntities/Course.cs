using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIEntities
{
    public class Course
    {
        public int CrsId { get; set; }

        public string CrsName { get; set; }

        public int Duration { get; set; }

        public virtual List<Department> Departments { get; set; }

        public virtual List<StudentCourse> CourseStudents { get; set; }


    }
}
