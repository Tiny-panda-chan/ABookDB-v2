namespace WebProject.ViewModels.Book
{
    public class IndexVM
    {
        public List<BookItem>? BookList { get; set; }
        public List<string> Categories { get; set; }

        public struct BookItem
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public List<string> BookCategories { get; set; }
        }
    }
    

}
