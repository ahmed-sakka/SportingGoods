using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models
{
    public class Item
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        [Required]
        public double Price { get; set; }
        public string Brand { get; set; }
        public Category Category { get; set; }
    }
}
