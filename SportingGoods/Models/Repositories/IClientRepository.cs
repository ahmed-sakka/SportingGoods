using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportingGoods.Models.Repositories
{
    public interface IClientRepository
    {
        IList<Client> GetAll();
        Client GetById(int id);
        void Add(Client client);
        void Edit(Client client);
        void Delete(Client client);
    }
}
