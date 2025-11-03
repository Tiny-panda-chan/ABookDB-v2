using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IReviewRepository
    {
        Task<ReviewModel?> GetByIdAsync(int id);
        Task<IEnumerable<ReviewModel>> GetAllAsync();
        void Add(ReviewModel model);
        void Edit(ReviewModel model);
        void Delete(ReviewModel model);
    }
}
