namespace FinalProjectMVC.ViewModels.Admin.Slider
{
    public class SliderVM
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public bool IsMain { get; set; } = false;
    }
}
