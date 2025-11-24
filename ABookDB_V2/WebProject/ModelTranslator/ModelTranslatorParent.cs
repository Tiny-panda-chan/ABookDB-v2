using DBService.Repositories;
using Models.Interfaces;
using Models.Models;
using System.Security.Claims;

namespace WebProject.ModelTranslator
{
    public abstract class ModelTranslatorParent(IHttpContextAccessor _httpContextAccesor, ABookDBContext _context, IUserRepository _userRepository)
    {
        /*protected AuthorRepository AuthorRepository = new AuthorRepository(_context);
        protected BookRepository BookRepository = new BookRepository(_context);
        protected CategoryRepository CategoryRepository = new CategoryRepository(_context);
        protected UserRepository UserRepository = new UserRepository(_context);
        protected ReviewRepository ReviewRepository = new ReviewRepository(_context);*/
        protected async Task<UserModel> GetUser()
        {
            UserModel? user;
            if (!_httpContextAccesor?.HttpContext?.User.Claims.Any(c => c.Type == ClaimTypes.NameIdentifier) ?? true)
            {
                return new UserModel();
            }else
            {
                string? userid = _httpContextAccesor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userid == null)
                    return new UserModel();
                user = await _userRepository.GetByIdAsync(Int32.Parse(userid));
            }
            if (user == null)
                return new UserModel();
            return user;
        }
    }
}
