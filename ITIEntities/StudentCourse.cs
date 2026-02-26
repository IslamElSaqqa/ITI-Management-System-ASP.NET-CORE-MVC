using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIEntities
{
    public class StudentCourse
    {
        [ForeignKey(nameof(Course))]
        public int CrsId { get; set; }
        [ForeignKey(nameof(Student))]
        public int StdId { get; set; }

        public int? Degree { get; set; }
        
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }



    }
}
