using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SQE.Models
{
    public class LoginDTO
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Your password is limited {2} to {1}", MinimumLength = 6)]
        public string Password { get; set; }
    }
    public class UserDOT : LoginDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public ICollection<string> Roles { get; set; }

    }
    public class ForgotPasswordDOT 
    {
        public string Email { get; set; }

    }
}
