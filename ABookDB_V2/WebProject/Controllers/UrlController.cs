using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class UrlController : Controller
    {
        public async Task<IActionResult> AddUrl([Bind("Urls")] List<string> urls)
        {
            //book.Book.Urls.Add(new BookUrl());
            urls.Add("");//new List<BookUrl>() { new BookUrl { UrlValue = "sdlfk" }, new BookUrl { UrlValue = "123457" } };//.Add(new BookUrl());
            ViewModels.Book.CreateVM bookvm = new ViewModels.Book.CreateVM() { Urls = urls };
            return PartialView("~/Views/PartialViews/UrlAddPV.cshtml", bookvm);

        }
        public async Task<IActionResult> RemoveUrl([Bind("Urls")] List<string> urls, int indexToRemove)
        {
            //book.Book.Urls.Add(new BookUrl());
            urls.RemoveAt(indexToRemove);//new List<BookUrl>() { new BookUrl { UrlValue = "sdlfk" }, new BookUrl { UrlValue = "123457" } };//.Add(new BookUrl());
            ViewModels.Book.CreateVM bookvm = new ViewModels.Book.CreateVM() { Urls = urls };
            return PartialView("~/Views/PartialViews/UrlAddPV.cshtml", bookvm);

        }
        [Authorize]
        public IActionResult ValidateUrl(string validateUrl)
        {
            Uri uri = new Uri(validateUrl);
            if (WebScraper.WorkingHosts.hostnames.Any(h => h == uri.AbsoluteUri))
            {
                return Ok(uri.AbsoluteUri);
            }
            else
            {
                return BadRequest();
            }

            //var absUrl = new BookUrl() { UrlValue = uri.AbsoluteUri };
            //if (Uri.IsWellFormedUriString(absUrl.UrlValue, UriKind.Absolute) ? await absUrl.IsUrlValid() && WebScraper.WorkingHosts.hostnames.Contains(uri.Host) : false)
            //{
            //    return Ok(absUrl.UrlValue);
            //}
            //else
            //{
            //    return NoContent();
            //}
        }
    }
}
