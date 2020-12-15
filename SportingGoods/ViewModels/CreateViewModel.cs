using Microsoft.AspNetCore.Http;
using SportingGoods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.ViewModels
{
    public class CreateViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public Category Category { get; set; }
    }
}
