using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Contact
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public string Email { get; set; }


        public string Subject { get; set; }

        public string Message { get; set; }

        public string SuccessMessage { get; set; }

    }
}
