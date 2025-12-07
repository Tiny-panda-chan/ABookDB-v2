using AutoMapper;
using DBService.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Interfaces;
using Models.Models;
using Newtonsoft.Json.Linq;
using WebProject.Helpers;
using WebProject.ViewModels.User;

namespace WebProject.ModelTranslator
{
    public class ModelTranslatorUser(ABookDBContext _context,
        IHttpContextAccessor _httpContextAccesor,
        IUserRepository _userRepository,
        IBookRepository _bookRepository) : ModelTranslatorParent(_httpContextAccesor, _userRepository), IModelTranslatorUser
    {
        //User
        public async Task<ProfileVM> FillObjectAsync(ProfileVM obj)
        {
            UserModel user = await GetUser();
            if (user != null)
            {
                obj.Email = user.Email;
                obj.Username = user.Username;
                obj.ReadBooks = (await _userRepository.GetAllReadBooksAsync(user))?.Select(u => new ViewModels.User.ProfileVM.BookItem()
                {
                    Id = u.Id,
                    Name = u.Book.Name,
                    TotalPages = u.Book.TotalPages,
                    ReadToPage = u.Page,
                    ReadPercentage = string.Format("{0:P2}", ((decimal)u.Page / (decimal)u.Book.TotalPages))
                }).ToList();
            }

            return obj;
        }

        public async Task<bool> SaveObjectAsync(ViewModels.User.ReadBookVM obj)
        {
            UserModel user = await GetUser();
            BookModel? book = await _bookRepository.GetByIdAsync(obj.BookID);
            if (book == null || user == null)
                return false;
            ReadBooksModel rb = new ReadBooksModel();
            rb.Book = book;
            rb.User = user;
            rb.Page = obj.ReadToPage;
            rb.ReadStage = (obj.ReadToPage < book.TotalPages) ? ReadStage.InProgress : ReadStage.Finished;
            _userRepository.AddOrUpdateReadBook(rb);
            return true;
        }

        public async Task<bool> SaveObjectAsync(ViewModels.User.ProfileVM obj)
        {
            UserModel user = await GetUser();

            if (user == null) return false;
            if (obj.NewPassword != null && obj.OldPassword != null)
            {
                if (new PasswordHasher<UserModel>().VerifyHashedPassword(user, user.Password, obj.OldPassword) == PasswordVerificationResult.Failed)
                    return false;

                user.Password = new PasswordHasher<UserModel>().HashPassword(user, obj.NewPassword);
            }
            else if (obj.Email != null)
            {
                user.Email = obj.Email;
            }
            _userRepository.Edit(user);
            return true;

        }


    }
}
