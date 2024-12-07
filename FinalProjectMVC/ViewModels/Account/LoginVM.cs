using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Account
{
    public class LoginVM
    {

        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
