using DBService.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.ModelTranslator;

namespace WebProject.Controllers
{
    [Authorize]
    public class CategoryController(IModelTranslator _translator, ABookDBContext _dbContext) : Controller
    {
        public async Task<IActionResult> AddCategory(string slist, string catName)
        {
            var res = await _translator.SaveObjectAsync(new ViewModels.Category.CreateVM() { Name = catName });
            if (!res)
                return BadRequest();

            ViewModels.Book.CreateVM returnViewModel = new ViewModels.Book.CreateVM() {
                SelectedCategories = slist.Split(',').ToList(), 
                CategoryCreateVM = new() { AllCategories = new SelectList((new CategoryRepository(_dbContext).GetAllAsync().Result).Select(c => c.Name).ToList()) }
            };

            return PartialView("~/Views/Category/ListPV.cshtml", returnViewModel);

        }
        public async Task<IActionResult> RemoveCategory(string catName)
        {
            //book.Book.Urls.Add(new BookUrl());
            //urls.RemoveAt(indexToRemove);//new List<BookUrl>() { new BookUrl { UrlValue = "sdlfk" }, new BookUrl { UrlValue = "123457" } };//.Add(new BookUrl());
            //BookCreateVM bookvm = new BookCreateVM() { Urls = urls };
            return Ok();

        }
    }
}
