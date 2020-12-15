using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 8)]
        public string CIN { get; set; }
    }
}
