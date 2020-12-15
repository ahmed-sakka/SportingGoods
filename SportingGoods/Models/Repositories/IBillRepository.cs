using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models.Repositories
{
    public interface IBillRepository
    {
        IList<Bill> GetAll();
        Bill GetById(int id);
        void Add(Bill bill);
        void Edit(Bill bill);
        void Delete(Bill bill);
    }
}
