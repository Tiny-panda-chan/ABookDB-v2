using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.Interfaces
{
    public interface IBookRepository
    {
        Task<BookModel?> GetByIdAsync(int id);
        Task<IEnumerable<BookModel>> GetAllAsync();
        void Add(BookModel model);
        void Edit(BookModel model);
        void Delete(BookModel model);
        Task<IEnumerable<CategoryModel>?> GetAllCategoriesAsync(BookModel model);
        Task<IEnumerable<UrlModel>?> GetAllUrlsAsync(BookModel model);
        Task<IEnumerable<FileModel>?> GetAllFilesAsync(BookModel model);
        Task<IEnumerable<ReviewModel>?> GetAllReviewsAsync(BookModel model);
    }
}
