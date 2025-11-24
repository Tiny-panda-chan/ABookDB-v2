using DBService.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Interfaces;
using Models.Models;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using WebProject.Helpers.Interfaces;
using WebProject.ViewModels.User;

namespace WebProject.Helpers
{
    public class AuthHelper(IUserRepository _userRepository) : IAuthHelper
    {
        public async Task<bool?> LoginUser(HttpContext _httpContext, LoginVM user)
        {
            UserModel? dbUser = await _userRepository.GetByEmail(user.Email);
            if (dbUser == null)
            {
                return false;
            }
            if (new PasswordHasher<UserModel>().VerifyHashedPassword(dbUser, dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                return false;
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Name, dbUser.Username)
            };

            var identity = new ClaimsIdentity(claims, "authCookie");
            var principal = new ClaimsPrincipal(identity);
            var options = new AuthenticationProperties
            {
                IsPersistent = user.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };
            await _httpContext.SignInAsync("authCookie", principal, options);
            return true;
        }

        public async Task<UserModel> RegisterUserAsync(HttpContext _httpContext, RegisterVM user)
        {
            if (await _userRepository.GetByEmail(user.Email) != null)
            {
                return null;
            }
            var dbUser = new UserModel();
            var hashPassword = new PasswordHasher<UserModel>().HashPassword(dbUser, user.Password);
            dbUser.Password = hashPassword;
            dbUser.Username = user.Username;
            dbUser.Email = user.Email;
            dbUser.RegistrationDate = DateTime.Now;
            _userRepository.Add(dbUser);

            await LoginUser(_httpContext, new LoginVM() { Email = user.Email, Password = user.Password });
            return dbUser;
        }
    }
}
