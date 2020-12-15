using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext context;

        public ItemRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
        }

        public void Delete(Item item)
        {
            Item item1 = context.Items.Find(item.ID);
            if (item1 != null)
            {
                context.Items.Remove(item1);
                context.SaveChanges();
            }
        }

        public void Edit(Item item)
        {
            Item item1 = context.Items.Find(item.ID);
            if (item1 != null)
            {
                item1.Name = item.Name;
                item1.Picture = item.Picture;
                item1.Price = item.Price;
                item1.Description = item.Description;
                item1.Brand = item.Brand;
                context.SaveChanges();
            }
        }

        public IList<Item> GetAll()
        {
            return context.Items.OrderBy(i => i.Name).ToList();
        }

        public Item GetById(int id)
        {
            return context.Items.Find(id);
        }
    }
}
