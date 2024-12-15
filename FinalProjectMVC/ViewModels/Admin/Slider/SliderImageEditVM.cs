
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Admin.Slider
{
    public class SliderImageEditVM
    {
        public int Id { get; set; }
        [Required]

        public IFormFile Photo { get; set; }
        [Required]

        public string Image { get; set; }
    }
}
