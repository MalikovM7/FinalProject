using Domain.Models;
using Repositories.Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactrepository;

        public ContactService(IContactRepository repository)
        {
            _contactrepository = repository;
        }

        public async Task DeleteContactAsync(int id)
        {
            await _contactrepository.DeleteAsync(id);
        }

        public async Task<List<Contact>> GetContactAsync()
        {
           return (await _contactrepository.GetAllAsync()).ToList();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return (await _contactrepository.GetByIdAsync(id));
        }

        public void SaveContact(Contact contact)
        {
            _contactrepository.AddContact(contact);
        }
    }
}
