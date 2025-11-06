namespace WebProject.ViewModels.Review
{
    public class ListVM
    {
        public readonly int _id;
        public ListVM(int Id)
        {
            _id = Id;
        }
        public List<ReviewItem> ReviewItems { get; set; }

        public struct ReviewItem
        {
            public string TextContent { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
        }
    }
}
