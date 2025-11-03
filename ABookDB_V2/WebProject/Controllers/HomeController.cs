using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProject.Models;
using DBService;
using DBService.Repositories;

namespace WebProject.Controllers
{
    public class HomeController(ABookDBContext context, ILogger<HomeController> logger) : Controller
    {

        public IActionResult Index()
        {
            BookRepository br = new BookRepository(context);
            var bk = br.GetByIdAsync(1).Result;
            var books = br.GetAllAsync().Result;
            var cats = br.GetAllCategoriesAsync(books.FirstOrDefault()).Result;
            var categories = new CategoryRepository(context).GetAllAsync().Result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
