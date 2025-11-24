using AutoMapper;
using DBService.Repositories;
using Models.Interfaces;
using Models.Models;
using WebProject.Helpers;
using WebProject.ViewModels.Review;

namespace WebProject.ModelTranslator
{
    public class ModelTranslatorReview(ABookDBContext _context,
        IHttpContextAccessor _httpContextAccesor,
        IBookRepository _bookRepository,
        IUserRepository _userRepository,
        IReviewRepository _reviewRepository) : ModelTranslatorParent(_httpContextAccesor, _context, _userRepository), IModelTranslatorReview
    {
        //Review
        public async Task<ListVM> FillObjectAsync(ListVM obj)
        {
            BookModel bk = await _bookRepository.GetByIdAsync(obj._id);
            if (bk == null)
                return null;
            await _bookRepository.GetAllReviewsAsync(bk);
            if (bk.Reviews == null)
                return null;
            obj.ReviewItems = bk.Reviews.Select(c => new ListVM.ReviewItem() { TextContent = c.Text, CreatedBy = c.createdBy.Username, CreatedOn = c.createdOn }).ToList();
            return obj;
        }

        public async Task<int> SaveObjectAsync(ViewModels.Review.CreateVM obj)
        {
            BookModel bk = await _bookRepository.GetByIdAsync(obj.BookId);
            if (bk == null)
                return 0;
            UserModel user = await GetUser();
            ReviewModel rm = new ReviewModel() { createdOn = DateTime.Now, Text = obj.Text, book = bk, createdBy = user };

            _reviewRepository.Add(rm);
            return rm.Id;
        }
    }
}
