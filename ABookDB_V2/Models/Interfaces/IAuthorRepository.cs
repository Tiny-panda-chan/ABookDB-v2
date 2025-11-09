using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IAuthorRepository
    {
        Task<AuthorModel?> GetByIdAsync(int id);
        Task<IEnumerable<AuthorModel>> GetAllAsync();
        bool? Add(AuthorModel model);
        void Edit(AuthorModel model);
        void Delete(AuthorModel model);
        Task<IEnumerable<BookModel>> GetAllBooksByAuthorAsync(AuthorModel model);
    }
}
