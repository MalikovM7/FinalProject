namespace FinalProjectMVC.ViewModels.Admin.News
{
    public class NewsVM 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsSelected { get; set; }

        public string NewsCategory { get; set; }

        public string AuthorName { get; set; }

        public string NewsDate { get; set; }
    }
}
