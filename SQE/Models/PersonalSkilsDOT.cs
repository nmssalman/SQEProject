using SQE.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Models
{
    public class CreatePersonalSkillsDOT
    {
        public int PersonalDetailsId { get; set; }
        [Required]
        public int SkilsId { get; set; }
        public bool? ActiveStatus { get; set; } = false;
        [Required]
        public string ApiUserId { get; set; }
    }
    public class PersonalSkillsDOT : CreatePersonalSkillsDOT
    {
        public int Id { get; set; }
        public Skills Skills { get; set; }
        public PersonalDetails PersonalDetails { get; set; }
        public IList<Skills> Skillss { get; set; }
    }
}
