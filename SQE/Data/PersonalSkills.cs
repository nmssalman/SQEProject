using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Data
{
    public class PersonalSkills
    {
        public int Id { get; set; }
        public bool ActiveStatus { get; set; }
        [ForeignKey(nameof(PersonalDetails))]
        public int PersonalDetailsId { get; set; }
        public PersonalDetails PersonalDetails { get; set; }
        [ForeignKey(nameof(Skils))]
        public int SkilsId { get; set; }
        public Skills Skils { get; set; }
        public IList<Skills> Skilss { get; set; }
    }
}
