using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SQE.Data
{
    public class Skills
    {
        public int Id { get; set; }
        public string SkilsName { get; set; }
        public bool ActiveStatus { get; set; }
        [ForeignKey(nameof(ApiUser))]
        public string ApiUserId { get; set; }
        public ApiUser ApiUser { get; set; }
    }
}
