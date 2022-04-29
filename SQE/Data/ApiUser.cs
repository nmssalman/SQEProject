using Microsoft.AspNetCore.Identity;

namespace SQE.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       // //[DataType(DataType.PhoneNumber)]
       // public string PhoneNumber { get; set; }
       // //[DataType(DataType.EmailAddress)]
       //// [Required]
       // public string Email { get; set; }
       // //[Required]
       // //[StringLength(15, ErrorMessage = "Your password is limited {2} to {1}", MinimumLength = 6)]
       // public string Password { get; set; }
    }
}
