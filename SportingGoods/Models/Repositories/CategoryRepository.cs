using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void Delete(Category category)
        {
            Item c = context.Items.Find(category.ID);
            if (c != null)
            {
                context.Items.Remove(c);
                context.SaveChanges();
            }
        }

        public void Edit(Category category)
        {
            Category c = context.Categories.Find(category.ID);
            if (c != null)
            {
                c.Name = category.Name;
                c.Description = category.Description;
                context.SaveChanges();
            }
        }

        public IList<Category> GetAll()
        {
            return context.Categories.OrderBy(i => i.Name).ToList();
        }

        public Category GetById(int id)
        {
            return context.Categories.Find(id);
        }
    }
}
