using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly AppDbContext context;
        public BillRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Bill bill)
        {
            context.Bills.Add(bill);
            context.SaveChanges();
        }

        public void Delete(Bill bill)
        {
            Bill b = context.Bills.Find(bill.ID);
            if (b != null)
            {
                context.Bills.Remove(b);
                context.SaveChanges();
            }
        }

        public void Edit(Bill bill)
        {
            Bill b = context.Bills.Find(bill.ID);
            if (b != null)
            {
                b.Client = bill.Client;
                b.ClientID = bill.ClientID;
                b.Date = bill.Date;
                b.Items = bill.Items;
                context.SaveChanges();
            }
        }

        public IList<Bill> GetAll()
        {
            return context.Bills.OrderBy(i => i.Date).ToList();
        }

        public Bill GetById(int id)
        {
            return context.Bills.Find(id);
        }
    }
}
