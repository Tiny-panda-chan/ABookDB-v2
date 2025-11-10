using DBService.Repositories;
using Models.Models;
using System.Security.Claims;

namespace WebProject.ModelTranslator
{
    public abstract class ModelTranslatorParent(IHttpContextAccessor _httpContextAccesor, ABookDBContext _context)
    {
        protected AuthorRepository AuthorRepository = new AuthorRepository(_context);
        protected BookRepository BookRepository = new BookRepository(_context);
        protected CategoryRepository CategoryRepository = new CategoryRepository(_context);
        protected UserRepository UserRepository = new UserRepository(_context);
        protected ReviewRepository ReviewRepository = new ReviewRepository(_context);
        protected async Task<UserModel?> GetUser()
        {
            if (!_httpContextAccesor.HttpContext.User.Claims.Any(c=> c.Type == ClaimTypes.NameIdentifier))
                return new UserModel();
            UserModel user = await UserRepository.GetByIdAsync(Int32.Parse(_httpContextAccesor.HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value));
            if (user == null)
                return new UserModel();
            return user;
        }
    }
}
