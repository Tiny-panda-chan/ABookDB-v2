using Models.Models;

namespace WebProject.ViewModels.Book
{
    public class DetailVM
    {
        public readonly int _id;
        public DetailVM(int Id)
        {
            _id = Id;
        }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int TotalPages { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> BookCategories { get; set; }
        public List<string> BookFiles { get; set; }
        public List<string> BookUrls { get; set; }
        public List<ReviewItem> BookReviews { get; set; }
    }

    public class ReviewItem
    {
        public string Author { get; set; }
        public string Text { get; set; }
    }
}
