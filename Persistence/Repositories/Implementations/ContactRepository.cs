using Domain.Models;
using Persistence.Data;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementations
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {

        public ContactRepository(AppDbContext context) : base(context)
        {

        }
        public void AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }
    }
}
