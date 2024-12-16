using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IContactService 
    {
        void SaveContact(Contact contact);

        Task DeleteContactAsync(int id);

        Task<List<Contact>> GetContactAsync();

        Task<Contact> GetContactByIdAsync(int id);


    }
}
