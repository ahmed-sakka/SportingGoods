using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext context;

        public ClientRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Client client)
        {
            context.Clients.Add(client);
            context.SaveChanges();
        }

        public void Delete(Client client)
        {
            Client c = context.Clients.Find(client.ClientID);
            if (c != null)
            {
                context.Clients.Remove(c);
                context.SaveChanges();
            }
        }

        public void Edit(Client client)
        {
            Client c = context.Clients.Find(client.ClientID);
            if (c != null)
            {
                c.FirstName = client.FirstName;
                c.LastName = client.LastName;
                c.CIN = client.CIN;
                c.Gender = client.Gender;
                context.SaveChanges();
            }
        }

        public IList<Client> GetAll()
        {
            return context.Clients.OrderBy(i => i.FirstName).ToList();
        }

        public Client GetById(int id)
        {
            return context.Clients.Find(id);
        }
    }
}
