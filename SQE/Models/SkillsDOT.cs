using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Models
{
    public class CreateSkillsDOT
    {
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Address name is too long")]
        public string SkilsName { get; set; }
        public bool? ActiveStatus { get; set; } = false;
        [Required]
        public string ApiUserId { get; set; }
    }
    public class SkillsDOT: CreateSkillsDOT
    {
        public int Id { get; set; }
        public UserDOT ApiUser { get; set; }
    }
}
