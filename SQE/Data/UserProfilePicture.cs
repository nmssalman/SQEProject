using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Data
{
    public class UserProfilePicture
    {
        public int Id { get; set; }
        //[Column(TypeName = "ntext")]
        public string Image_Base64_String { get; set; }
        public bool ActiveStatus { get; set; }
        [ForeignKey(nameof(PersonalDetails))]
        public int PersonalDetailsId { get; set; }
        public PersonalDetails PersonalDetails { get; set; }
        public IList<PersonalDetails> PersonalDetailsList { get; set; }
    }
}
