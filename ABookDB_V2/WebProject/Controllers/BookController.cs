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

namespace WebProject.Controllers
{
    public class BookController(IModelTranslator _translator/*, IMapper _mapper*/) : Controller
    {

        public async Task<IActionResult> Index()
        {
            IndexVM indexVM = await _translator.FillObjectAsync(new ViewModels.Book.IndexVM());
            //var blbalb = _translator.FillObject(new ViewModels.Book.DetailVM(4));

            //IndexVM ivm = _mapper.Map<IndexVM>(_translator.);
            return View(indexVM);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
                throw new NotSupportedException();
            DetailVM detailVM = new DetailVM(id);
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                throw new NotSupportedException();
            return View(await _translator.FillObjectAsync(new EditVM(id)));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await _translator.FillObjectAsync(new CreateVM()));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVM createVM)
        {
            await _translator.SaveObjectAsync(createVM);
            return RedirectToAction();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
