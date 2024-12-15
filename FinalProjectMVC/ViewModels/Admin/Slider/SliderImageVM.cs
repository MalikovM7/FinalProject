

using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Admin.Slider
{
    public class SliderImageVM
    {
        [Required]
        public string Image { get; set; }
    }
}
