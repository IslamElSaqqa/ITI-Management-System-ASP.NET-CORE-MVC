using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIEntities
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeptId { get; set; }

        [Required, MaxLength(20)]
        public string DeptName { get; set; }
        public int Capacity { get; set; }

        // Navigation property to Students
        // We used HashSet to avoid duplicates and to provide better performance for lookups
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        public virtual List<Course> Courses { get; set; }
    }
}
