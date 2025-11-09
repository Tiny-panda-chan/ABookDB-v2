using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class UrlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
