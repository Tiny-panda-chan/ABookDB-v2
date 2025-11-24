using AutoMapper;
using DBService.Repositories;
using Models.Interfaces;
using Models.Models;
using WebProject.Helpers;

namespace WebProject.ModelTranslator
{
    public class ModelTranslatorCategory(ABookDBContext _context,
        IHttpContextAccessor _httpContextAccesor,
        IUserRepository _userRepository,
        ICategoryRepository _categoryRepository) : ModelTranslatorParent(_httpContextAccesor, _context, _userRepository), IModelTranslatorCategory
    {
        //Category
        public async Task<ViewModels.Category.CreateVM> FillObjectAsync(ViewModels.Category.CreateVM obj)
        {
            obj.AllCategories = new((await _categoryRepository.GetAllAsync()).Select(c => c.Name).ToList());
            return obj;
        }


        public async Task<bool> SaveObjectAsync(ViewModels.Category.CreateVM obj)
        {
            CategoryModel cm = new CategoryModel() { Name = obj.Name };

            return _categoryRepository.Add(cm).GetValueOrDefault();
        }
    }
}
