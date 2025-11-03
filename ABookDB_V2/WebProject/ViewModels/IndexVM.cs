namespace WebProject.ViewModels
{
    public class IndexVM
    {
        public List<BookItem> BookList { get; set; }


       
    }
    public class BookItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

}
