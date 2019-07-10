using System;
using System.ComponentModel.DataAnnotations;

namespace LiftTracker2.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 8 characters long")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime Created { get; set; }
         [Required]
        public DateTime LastActive { get; set; }
        

        public string Gender { get; set; }
         [Required]
        public DateTime DateOfBirth { get; set; }
    }
}