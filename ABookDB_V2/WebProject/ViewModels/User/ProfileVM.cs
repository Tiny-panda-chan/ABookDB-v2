namespace WebProject.ViewModels.User
{
    public class ProfileVM
    {
        public int Id { get; set; }
        public ProfileVM(int id) 
        {
            Id = id;
        }
        public string Username { get; set; }
        public string Email { get; set; }
        //for password edit
        public string Password { get; set; }

        public List<BookItem>? ReadBooks { get; set; }

        public struct BookItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int TotalPages { get; set; }
            public int ReadPercentage
            {
                get { return ReadPercentage; }
                set { ReadPercentage = (value % TotalPages) * 100; }
            }

        }

    }

    
}
