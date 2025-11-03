using Models.Models;

namespace WebProject.ViewModels
{
    public class DetailVM
    {
        public readonly int _id;
        public DetailVM(int Id)
        {
            _id = Id;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalPages { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
