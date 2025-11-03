using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryModel?> GetByIdAsync(int id);
        Task<IEnumerable<CategoryModel>> GetAllAsync();
        void Add(CategoryModel model);
        void Edit(CategoryModel model);
        void Delete(CategoryModel model);
        Task<IEnumerable<BookModel>> GetAllBooksInCategory(CategoryModel model);
    }
}
