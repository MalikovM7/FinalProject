namespace FinalProjectMVC.ViewModels.Admin.AboutUs
{
    public class AboutUsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public List<string> Points { get; set; }
        public List<string> ImageUrls { get; set; }


    }
}
