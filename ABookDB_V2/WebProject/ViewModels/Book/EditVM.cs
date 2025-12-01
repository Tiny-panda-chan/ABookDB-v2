using Models.Models;
using System.ComponentModel.DataAnnotations;

namespace WebProject.ViewModels.Book
{
    public class EditVM
    {
        public readonly int _id;
        public EditVM(int Id)
        {
            _id = Id;
        }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int TotalPages { get; set; }
        public List<string>? Categories { get; set; }
        public List<string>? Urls { get; set; }
        public ICollection<FileModel>? BookFiles { get; set; }
        public IFormFileCollection? UploadedFiles { get; set; }
        public ViewModels.Category.CreateVM CategoryCreateVM { get; set; } = new();
    }
}
