using System.ComponentModel.DataAnnotations;

namespace ITI_Management_System.ViewModels
{
    public class RegisterVM
    {
        // User Info
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        // Student Info
        [Required]
        public string Name { get; set; }

        [Range(22, 30, ErrorMessage = "Age must be between 22 and 30")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int DeptId { get; set; }
    }
}