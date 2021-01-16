using Microsoft.AspNetCore.Http;
using SportingGoods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.ViewModels
{
    public class CreateItemViewModel
    {
        private readonly AppDbContext context;
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public Category Category { get; set; }
        public int CategoryID { get; set; }


        public IList<Category> GetAllCategories()
        {

            return context.Categories.OrderBy(i => i.Name).ToList();
        }
    }
}
