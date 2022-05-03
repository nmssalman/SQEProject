using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Models
{
    public class CreateEducationDTO
    {
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Major is too long")]
        public string Major { get; set; }
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Institute is too long")]
        public string Institute { get; set; }
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Description is too long")]
        public string Description { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public bool ActiveStatus { get; set; }
        [Required]
        public int PersonalDetailsId { get; set; }
    }
    public class EducationDTO : CreateEducationDTO
    {
        public int Id { get; set; }
        public PersonalDetailsDOT PersonalDetails { get; set; }
        public IList<PersonalDetailsDOT> PersonalDetailsList { get; set; }
    }
}
