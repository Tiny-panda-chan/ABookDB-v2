using DBService.Repositories;
using WebProject.ViewModels.Book;

namespace WebProject.ModelTranslator
{
    public interface IModelTranslator
    {
        //Books
        Task<IndexVM> FillObjectAsync(IndexVM obj);
        Task<DetailVM> FillObjectAsync(DetailVM obj);
        Task<EditVM> FillObjectAsync(EditVM obj);
        Task<CreateVM> FillObjectAsync(CreateVM obj);
        Task<int> SaveObjectAsync(EditVM obj);
        Task<int> SaveObjectAsync(CreateVM obj);
    }
}
