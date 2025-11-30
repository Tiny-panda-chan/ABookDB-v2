using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebProject.ViewModels.Category
{
    public class CreateVM
    {
        public string Name { get; set; }

        public SelectList AllCategories { get; set; }
        public List<string>? SelectedCategories { get; set; }
    }
}
