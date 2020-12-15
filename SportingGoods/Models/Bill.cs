using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models
{
    public class Bill
    {
        public int ID { get; set; }
        public List<Item> Items{ get; set; }
        public Client Client { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
