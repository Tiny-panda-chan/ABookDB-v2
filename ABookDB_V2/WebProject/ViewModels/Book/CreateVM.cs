using Models.Models;
using System.ComponentModel.DataAnnotations;

namespace WebProject.ViewModels.Book
{
    public class CreateVM
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public AuthorModel? Author { get; set; }
        public int TotalPages { get; set; }
        public List<string>? Urls { get; set; } = new List<string>() { "" };
        public IFormFileCollection? UploadedFiles { get; set; }


        //public List<string>? AllCategories { get; set; }
        public ViewModels.Category.CreateVM CategoryCreateVM { get; set; }
        public List<string>? AllAuthors { get; set; }
    }
}
