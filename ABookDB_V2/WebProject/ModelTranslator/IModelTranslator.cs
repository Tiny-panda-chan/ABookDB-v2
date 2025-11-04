using DBService.Repositories;
using WebProject.ViewModels.Book;

namespace WebProject.ModelTranslator
{
    public interface IModelTranslator
    {
        Task<IndexVM> FillObjectAsync(IndexVM obj);
        Task<DetailVM> FillObjectAsync(DetailVM obj);
        Task<EditVM> FillObjectAsync(EditVM obj);
    }
}
