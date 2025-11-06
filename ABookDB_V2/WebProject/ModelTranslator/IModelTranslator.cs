using DBService.Repositories;
using WebProject.ViewModels.Book;
using WebProject.ViewModels.Review;
using WebProject.ViewModels.User;

namespace WebProject.ModelTranslator
{
    public interface IModelTranslator
    {
        //Books
        Task<IndexVM> FillObjectAsync(IndexVM obj);
        Task<DetailVM> FillObjectAsync(DetailVM obj);
        Task<EditVM> FillObjectAsync(EditVM obj);
        Task<ViewModels.Book.CreateVM> FillObjectAsync(ViewModels.Book.CreateVM obj);
        Task<int> SaveObjectAsync(EditVM obj);
        Task<int> SaveObjectAsync(ViewModels.Book.CreateVM obj);

        //User
        Task<ProfileVM> FillObjectAsync(ProfileVM obj);

        //Review
        Task<ListVM> FillObjectAsync(ListVM obj);
        Task<int> SaveObjectAsync(ViewModels.Review.CreateVM obj);
    }
}
