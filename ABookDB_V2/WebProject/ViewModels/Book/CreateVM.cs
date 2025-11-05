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
        public List<string>? Urls { get; set; }
        public List<string>? SelectedCategories { get; set; }
        public IFormFileCollection? UploadedFiles { get; set; }


        public List<string>? AllCategories { get; set; }
        public List<string>? AllAuthors { get; set; }
    }
}
