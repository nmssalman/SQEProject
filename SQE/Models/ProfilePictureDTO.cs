using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Models
{
    public class CreateProfilePictureDTO
    {
        public string Image_Base64_String { get; set; }
        public bool ActiveStatus { get; set; }
        public int PersonalDetailsId { get; set; }
        public PersonalDetailsDOT PersonalDetails { get; set; }
    }
    public class ProfilePictureDTO: CreateProfilePictureDTO
    {
        public int Id { get; set; }
        public IList<PersonalDetailsDOT> PersonalDetailsList { get; set; }
    }
}
