using AutoMapper;
using DBService.Repositories;
using Models.Models;
using WebProject.Helpers;

namespace WebProject.ModelTranslator
{
    public class ModelTranslatorCategory(ABookDBContext _context, IStatusService _services, IHttpContextAccessor _httpContextAccesor) : ModelTranslatorParent(_httpContextAccesor, _context), IModelTranslatorCategory
    {
        //Category
        public async Task<ViewModels.Category.CreateVM> FillObjectAsync(ViewModels.Category.CreateVM obj)
        {
            obj.AllCategories = new((await CategoryRepository.GetAllAsync()).Select(c => c.Name).ToList());
            return obj;
        }


        public async Task<bool> SaveObjectAsync(ViewModels.Category.CreateVM obj)
        {
            CategoryModel cm = new CategoryModel() { Name = obj.Name };

            return CategoryRepository.Add(cm).GetValueOrDefault();
        }
    }
}
