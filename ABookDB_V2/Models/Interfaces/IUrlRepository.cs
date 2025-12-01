using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IUrlRepository
    {
        Task<UrlModel?> GetByIdAsync(int id);
        Task<IEnumerable<UrlModel>> GetAllAsync();
        bool? Add(UrlModel model);
        void Edit(UrlModel model);
        void Delete(UrlModel model);
    }
}
