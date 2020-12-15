using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models.Repositories
{
    public interface ICategoryRepository
    {
        IList<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        void Edit(Category category);
        void Delete(Category category);
    }
}
