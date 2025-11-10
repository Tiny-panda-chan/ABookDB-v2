using AutoMapper;
using DBService.Repositories;
using Models.Models;
using WebProject.Helpers;
using WebProject.ViewModels.Review;

namespace WebProject.ModelTranslator
{
    public class ModelTranslatorReview(ABookDBContext _context, IStatusService _services, IHttpContextAccessor _httpContextAccesor) : ModelTranslatorParent(_httpContextAccesor, _context), IModelTranslatorReview
    {
        //Review
        public async Task<ListVM> FillObjectAsync(ListVM obj)
        {
            BookModel bk = await BookRepository.GetByIdAsync(obj._id);
            if (bk == null)
                return null;
            await BookRepository.GetAllReviewsAsync(bk);
            if (bk.Reviews == null)
                return null;
            obj.ReviewItems = bk.Reviews.Select(c => new ListVM.ReviewItem() { TextContent = c.Text, CreatedBy = c.createdBy.Username, CreatedOn = c.createdOn }).ToList();
            return obj;
        }

        public async Task<int> SaveObjectAsync(ViewModels.Review.CreateVM obj)
        {
            BookModel bk = await BookRepository.GetByIdAsync(obj.BookId);
            if (bk == null)
                return 0;
            UserModel user = await GetUser();
            ReviewModel rm = new ReviewModel() { createdOn = DateTime.Now, Text = obj.Text, book = bk, createdBy = user };

            ReviewRepository.Add(rm);
            return rm.Id;
        }
    }
}
