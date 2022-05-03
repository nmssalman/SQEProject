using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Data
{
    public class Experience
    {
        public int Id { get; set; }
        public string Company_Name { get; set; }
        public string Description { get; set; }
        public double? Years_of_Experience { get; set; }
        public bool ActiveStatus { get; set; }
        [ForeignKey(nameof(PersonalDetails))]
        public int PersonalDetailsId { get; set; }
        public PersonalDetails PersonalDetails { get; set; }
        public IList<PersonalDetails> PersonalDetailsList { get; set; }
    }
}
