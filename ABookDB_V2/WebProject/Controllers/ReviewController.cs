using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProject.ModelTranslator;
using WebProject.ViewModels.Review;

namespace WebProject.Controllers
{
    public class ReviewController(IModelTranslatorReview _translator) : Controller
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(int bookId, string content)
        {

            await _translator.SaveObjectAsync(new CreateVM() { Text = content, BookId = bookId});
            return View("~/Views/Review/ReviewPV.cshtml", await _translator.FillObjectAsync(new ListVM(bookId)));
        }

        
    }
    public class CommentViewComponent(IModelTranslatorReview _translator) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int bookId)
        {
            return View("~/Views/Review/ReviewPV.cshtml", await _translator.FillObjectAsync(new ListVM(bookId)));
        }


    }
}
