using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string? Email { get; set; }
        public string? Course { get; set; }
    }
}
