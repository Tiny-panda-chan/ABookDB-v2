using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProject.Models;
using DBService;
using DBService.Repositories;
using WebProject.ModelTranslator;
using WebProject.ViewModels;
using AutoMapper;
using System.Threading.Tasks;
using WebProject.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;

namespace WebProject.Controllers
{
    //[ValidateAntiForgeryToken]
    public class BookController(IModelTranslatorBook _translator/*, IMapper _mapper*/) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexVM indexVM = await _translator.FillObjectAsync(new ViewModels.Book.IndexVM());
            //var blbalb = _translator.FillObject(new ViewModels.Book.DetailVM(4));

            //IndexVM ivm = _mapper.Map<IndexVM>(_translator.);
            return View(indexVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexVM? indexVM)
        {
            if (indexVM?.SearchString == null && indexVM?.SearchCategories == null)
            {
                return RedirectToAction(nameof(Index));
            } else
            {
                await _translator.FillObjectAsync(indexVM, indexVM.SearchString, indexVM.SearchCategories);
            }

                
            return View(indexVM);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null ? true : id == 0)
                throw new NotSupportedException();
            else
            {
                DetailVM detailVM = new DetailVM((int)id);
                return View(_translator.FillObjectAsync(detailVM).Result);
            }    
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                throw new NotSupportedException();

            EditVM editVM = await _translator.FillObjectAsync(new EditVM(id));
            return View(editVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditVM editVM, int? EditId)
        {
            if (EditId  == null)
                throw new NotSupportedException();
            editVM._id = EditId.Value;
            await _translator.SaveObjectAsync(editVM);
            return RedirectToAction(nameof(Detail), new { id = EditId });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await _translator.FillObjectAsync(new CreateVM()));
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM createVM)
        {
            var createdBookId = await _translator.SaveObjectAsync(createVM);
            return RedirectToAction(nameof(Detail), new { id = createdBookId });
        }


        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            var res = await _translator.SaveObjectAsync(new ViewModels.Book.DeleteVM() { Id = id.Value });
            if (!res)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Download(int bookId, string fileName)
        {
            DownloadFileVM dfvm = await _translator.FillObjectAsync(bookId, new DownloadFileVM() { FileName = fileName });
            return File(dfvm.Data, System.Net.Mime.MediaTypeNames.Application.Octet, dfvm.FileName);
        }
        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
