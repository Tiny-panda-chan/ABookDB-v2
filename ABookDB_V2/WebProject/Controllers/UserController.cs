using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Helpers;
using WebProject.ModelTranslator;
using WebProject.ViewModels.User;
using static WebScraper.ScrapedFileModel;

namespace WebProject.Controllers
{
    public class UserController(IModelTranslatorUser _translator, IStatusService _statusService) : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileVM profileVM)
        {

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            AuthHelper auth = new AuthHelper(_statusService);
            await auth.RegisterUserAsync(HttpContext, registerVM);
            return RedirectToAction("Index", "Book");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            AuthHelper auth = new AuthHelper(_statusService);
            var sucLogin = await auth.LoginUser(HttpContext, loginVM);
            if (sucLogin.Value)
            {
                return RedirectToAction("Index", "Book");
            }
            else
            {
                return View(loginVM);
            }
            
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("authCookie");
            return RedirectToAction("Index", "Book");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateReadBook(int bookId, int readToPage)
        {
            var res = await _translator.SaveObjectAsync(new ReadBookVM() { BookID = bookId, ReadToPage = readToPage});
            if (!res)
                return BadRequest();
            return RedirectToAction("Detail", "Book", new { id = bookId });
        }



    }
}
