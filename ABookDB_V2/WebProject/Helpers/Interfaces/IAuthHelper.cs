using Models.Models;
using WebProject.ViewModels.User;

namespace WebProject.Helpers.Interfaces
{
    public interface IAuthHelper
    {
        Task<bool?> LoginUser(HttpContext _httpContext, LoginVM user);
        Task<UserModel> RegisterUserAsync(HttpContext _httpContext, RegisterVM user);
    }
}
