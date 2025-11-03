using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProject.Models;
using DBService;
using DBService.Repositories;
using WebProject.ModelTranslator;
using WebProject.ViewModels;
using AutoMapper;

namespace WebProject.Controllers
{
    public class HomeController(IModelTranslator _translator/*, IMapper _mapper*/) : Controller
    {

        public IActionResult Index()
        {
            var bla = _translator.FillObject(new ViewModels.IndexVM());
            var blbalb = _translator.FillObject(new ViewModels.DetailVM(4));

            //IndexVM ivm = _mapper.Map<IndexVM>(_translator.);
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
