using DBService.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Security.Claims;
using WebProject.ViewModels.User;

namespace WebProject.Helpers
{
    public class AuthHelper
    {
        [Inject]
        private ABookDBContext blabal
        {
            get { return blabal; }
            set {
                blabal = value;
                repo = new(value);
            }
        }
        private UserRepository repo { get; set; }
        public async Task<bool?> LoginUser(HttpContext _httpContext, LoginVM user)
        {
            UserModel? dbUser = await repo.GetByEmail(user.Email);
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
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString())
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
    }
}
