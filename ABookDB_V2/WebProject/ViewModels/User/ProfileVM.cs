namespace WebProject.ViewModels.User
{
    public class ProfileVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
        //for password edit
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public List<BookItem>? ReadBooks { get; set; }

        public struct BookItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int TotalPages { get; set; }
            public int ReadToPage { get; set; }
            public string ReadPercentage { get; set; }

        }

    }

    
}
