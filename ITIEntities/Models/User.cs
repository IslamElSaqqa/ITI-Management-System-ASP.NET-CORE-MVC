using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIEntities.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } // "Admin" or "Student"

        [ForeignKey(nameof(Student))]
        public int? StudentId { get; set; }
        public Student Student { get; set; }
    }
}
