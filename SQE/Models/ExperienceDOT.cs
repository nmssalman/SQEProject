using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Models
{
    public class CreateExperienceDOT
    {
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Company name is too long")]
        public string Company_Name { get; set; }
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Description name is too long")]
        public string Description { get; set; }
        [Required]
        public double? Years_of_Experience { get; set; }
        public bool ActiveStatus { get; set; }
        public int PersonalDetailsId { get; set; }

    }
    public class ExperienceDOT : CreateExperienceDOT
    {
        public int Id { get; set; }
        public PersonalDetailsDOT PersonalDetails { get; set; }
        public IList<PersonalDetailsDOT> PersonalDetailsList { get; set; }
    }
}
