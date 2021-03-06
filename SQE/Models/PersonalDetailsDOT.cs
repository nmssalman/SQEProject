using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Models
{
    public class CreatePersonalDetailsDOT
    {
        [Required]
        [StringLength(maximumLength:500, ErrorMessage = "Address name is too long")]
        public string Address { get; set; }
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Country name is too long")]
        public string Country { get; set; }
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "City name is too long")]
        public string City { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [StringLength(maximumLength: 1000, ErrorMessage = "Description is too long")]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Occupation is too long")]
        public string Occupation { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Linkedin is too long")]
        public string Linkedin { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Linkedin is too long")]
        public string Stackoverflow { get; set; }
        public bool? ActiveStatus { get; set; } = false;
        [Required]
        public string ApiUserId { get; set; }
    }
    public class PersonalDetailsDOT : CreatePersonalDetailsDOT
    {
        public int Id { get; set; }
        public UserDOT ApiUser { get; set; }
        public IList<UserDOT> ApiUsers { get; set; }
    }
    public class UpdatePersonalDetailsDOT : CreatePersonalDetailsDOT
    {

    }
}
