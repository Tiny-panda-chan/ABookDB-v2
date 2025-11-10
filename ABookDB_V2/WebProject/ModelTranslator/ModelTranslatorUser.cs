using AutoMapper;
using DBService.Repositories;
using Models.Models;
using WebProject.Helpers;
using WebProject.ViewModels.User;

namespace WebProject.ModelTranslator
{
    public class ModelTranslatorUser(ABookDBContext _context, IStatusService _services, IHttpContextAccessor _httpContextAccesor) : ModelTranslatorParent(_httpContextAccesor, _context), IModelTranslatorUser
    {
        //User
        public Task<ProfileVM> FillObjectAsync(ProfileVM obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveObjectAsync(ViewModels.User.ReadBookVM obj)
        {
            UserModel user = await GetUser();
            BookModel book = await BookRepository.GetByIdAsync(obj.BookID);
            if (book == null || user == null)
                return false;
            ReadBooksModel rb = new ReadBooksModel();
            rb.Book = book;
            rb.User = user;
            rb.Page = obj.ReadToPage;
            rb.ReadStage = (obj.ReadToPage < book.TotalPages) ? ReadStage.InProgress : ReadStage.Finished;
            UserRepository.AddOrUpdateReadBook(rb);
            return true;
        }
    }
}
