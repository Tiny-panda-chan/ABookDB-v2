using Models.Models;
using System.ComponentModel.DataAnnotations;

namespace WebProject.ViewModels.Book
{
    public class EditVM
    {
        public int _id = 0;
        public EditVM(int? Id)
        {
            if (Id != null)
                _id = Id.Value;
        }
        public EditVM()
        { }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int TotalPages { get; set; }
        public List<string>? Categories { get; set; }
        public List<string>? Urls { get; set; }
        public string BookFile { get; set; }
        public IFormFileCollection? UploadedFiles { get; set; }
        public ViewModels.Category.CreateVM CategoryCreateVM { get; set; } = new();
    }
}
