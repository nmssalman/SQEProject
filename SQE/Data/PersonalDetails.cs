using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Data
{
    public class PersonalDetails
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }
        public string Occupation { get; set; }
        public string Linkedin { get; set; }
        public string Stackoverflow { get; set; }
        public bool ActiveStatus { get; set; } 
        [ForeignKey(nameof(ApiUser))]
        public string ApiUserId { get; set; }
        public ApiUser ApiUser { get; set; } 
    }
}
