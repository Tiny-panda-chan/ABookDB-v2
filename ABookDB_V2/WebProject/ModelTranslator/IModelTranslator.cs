using DBService.Repositories;
using WebProject.ViewModels;

namespace WebProject.ModelTranslator
{
    public interface IModelTranslator
    {
        IndexVM FillObject(IndexVM obj);
        DetailVM FillObject(DetailVM obj);
    }
}
