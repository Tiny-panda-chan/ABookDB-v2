using AutoMapper;
using DBService.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using WebProject.ViewModels;

namespace WebProject.ModelTranslator
{
    public class ModelTranslator(ABookDBContext _context, IMapper _mapper) : IModelTranslator
    {
        private readonly ABookDBContext _context;
        private AuthorRepository AuthorRepository = new AuthorRepository(_context);
        private BookRepository BookRepository = new BookRepository(_context);
        private CategoryRepository CategoryRepository = new CategoryRepository(_context);
        private UserRepository UserRepository = new UserRepository(_context);



        public IndexVM FillObject(IndexVM obj)
        {
            obj.BookList = BookRepository.GetAllAsync().Result.Select(bl => new BookItem { Title = bl.Name, Description = bl.Description }).ToList();
            return obj;
        }

        public DetailVM FillObject(DetailVM obj)
        {
            BookModel bk = BookRepository.GetByIdAsync(obj._id).Result;
            obj.Name = bk.Name;
            obj.Categories = BookRepository.GetAllCategoriesAsync(bk).Result.ToList();
            var dvm = _mapper.Map<DetailVM>(bk);
            return obj;
        }

        /*public T FillObject<T>(T obj) where T : class
        {
            throw new NotImplementedException();
        }*/
    }
}
