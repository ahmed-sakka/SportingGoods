using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models.Repositories
{
    public interface IItemRepository
    {
        IList<Item> GetAll();
        Item GetById(int id);
        void Add(Item item);
        void Edit(Item item);
        void Delete(Item item);
    }
}
