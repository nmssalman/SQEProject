using SQE.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Models
{
    public class CreatePersonalSkilsDOT
    {
        public int PersonalDetailsId { get; set; }
        [Required]
        public int SkilsId { get; set; }
        public bool? ActiveStatus { get; set; } = false;
        [Required]
        public string ApiUserId { get; set; }
    }
    public class PersonalSkilsDOT : CreatePersonalSkilsDOT
    {
        public int Id { get; set; }
        public Skils Skils { get; set; }
        public PersonalDetails PersonalDetails { get; set; }
        public IList<Skils> Skilss { get; set; }
    }
}
