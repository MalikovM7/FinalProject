using FinalProjectMVC.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Domain.Models;
using Services.Interfaces;

namespace FinalProjectMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactViewModel viewModel)
        {
            
                var contact = new Contact
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Subject = viewModel.Subject,
                    Message = viewModel.Message
                };

                _service.SaveContact(contact);

                
                viewModel.SuccessMessage = "Your message has been sent successfully!";
                ModelState.Clear(); 
                return View(new ContactViewModel { SuccessMessage = viewModel.SuccessMessage });
            

            //return View(viewModel);
        }
    }
}
