using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel?> GetByIdAsync(int id);
        Task<IEnumerable<UserModel>> GetAllAsync();
        void Add(UserModel model);
        void Edit(UserModel model);
        void Delete(UserModel model);
        Task<IEnumerable<ReadBooksModel>> GetAllReadBooksAsync(UserModel model);
        Task<IEnumerable<BookModel>> GetAllBookCreatedByAsync(UserModel model);
    }
}
