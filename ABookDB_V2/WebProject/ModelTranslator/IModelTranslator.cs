using DBService.Repositories;
using WebProject.ViewModels.Book;
using WebProject.ViewModels.Review;
using WebProject.ViewModels.User;

namespace WebProject.ModelTranslator
{
    public interface IModelTranslatorBook
    {
        //Books
        Task<IndexVM> FillObjectAsync(IndexVM obj);
        Task<IndexVM> FillObjectAsync(IndexVM obj, string? searchString, List<string>? categories);
        Task<DetailVM> FillObjectAsync(DetailVM obj);
        Task<EditVM> FillObjectAsync(EditVM obj);
        Task<ViewModels.Book.CreateVM> FillObjectAsync(ViewModels.Book.CreateVM obj);
        Task<int> SaveObjectAsync(EditVM obj);
        Task<int> SaveObjectAsync(ViewModels.Book.CreateVM obj);
        Task<bool> SaveObjectAsync(ViewModels.Book.DeleteVM obj);
        Task<ViewModels.Book.DownloadFileVM> FillObjectAsync(int bookId, ViewModels.Book.DownloadFileVM obj);
    }
    public interface IModelTranslatorUser
    {
        //User
        Task<ProfileVM> FillObjectAsync(ProfileVM obj);
        Task<bool> SaveObjectAsync(ViewModels.User.ReadBookVM obj);
        Task<bool> SaveObjectAsync(ViewModels.User.ProfileVM obj);

    }
    public interface IModelTranslatorReview
    {
        //Review
        Task<ListVM> FillObjectAsync(ListVM obj);
        Task<int> SaveObjectAsync(ViewModels.Review.CreateVM obj);
    }
    public interface IModelTranslatorCategory 
    {
        //Category
        Task<ViewModels.Category.CreateVM> FillObjectAsync(ViewModels.Category.CreateVM obj);
        bool SaveObject(ViewModels.Category.CreateVM obj);
    }
}
