using FinalProjectMVC.Services.Implementations;
using FinalProjectMVC.ViewModels.Admin.HomePreview;
using FinalProjectMVC.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetContactAsync();
            var contactVMs = contacts.Select(x => new ContactViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Subject = x.Subject,
                Message = x.Message,


            }).ToList();

            return View(contactVMs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            var contactVM = new ContactViewModel
            {
                Id = contact.Id,
                Name=contact.Name,
                Email=contact.Email,
                Subject=contact.Subject,
                Message=contact.Message,
            };

            return View(contactVM);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            var contactVM = new ContactViewModel
            {
                Id=contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Subject = contact.Subject,
                Message = contact.Message,
            };

            return View(contactVM);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return RedirectToAction(nameof(Index));
        }








    }
}
