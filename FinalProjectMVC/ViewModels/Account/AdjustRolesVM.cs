using FinalProjectMVC.Identity;
using Microsoft.AspNetCore.Identity;


namespace FinalProjectMVC.ViewModels.Account
{
    public class AdjustRolesVM
    {
        public string UserId { get; set; }

        public List<AppUser> Users { get; set; } = new List<AppUser>();

        public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();

        public string SelectedRole { get; set; }
    }
}
