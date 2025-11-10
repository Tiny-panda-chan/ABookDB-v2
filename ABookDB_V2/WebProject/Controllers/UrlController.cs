using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class UrlController : Controller
    {
        public async Task<IActionResult> AddUrl([Bind("Urls")] List<string> urls)
        {
            urls.Add("");
            ViewModels.Book.CreateVM bookvm = new ViewModels.Book.CreateVM() { Urls = urls };
            return PartialView("~/Views/Url/CreatePV.cshtml", bookvm);

        }
        public async Task<IActionResult> RemoveUrl([Bind("Urls")] List<string> urls, int indexToRemove)
        {
            urls.RemoveAt(indexToRemove);
            ViewModels.Book.CreateVM bookvm = new ViewModels.Book.CreateVM() { Urls = urls };
            return PartialView("~/Views/Url/CreatePV.cshtml", bookvm);

        }
        [Authorize]
        public IActionResult ValidateUrl(string validateUrl)
        {
            if (validateUrl == null)
                return BadRequest();

            Uri uri = new Uri(validateUrl);
            if (WebScraper.WorkingHosts.hostnames.Any(h => h == uri.Host))
            {
                return Ok(uri.AbsoluteUri);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
