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
using WebProject.Consts;
using WebProject.Helpers;
using WebProject.Helpers.Interfaces;
using WebProject.ModelTranslator;
using WebProject.ViewModels.User;
using static WebScraper.ScrapedFileModel;

namespace WebProject.Controllers
{
    public class UserController(IModelTranslatorUser _translator, IAuthHelper _auth) : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            ProfileVM pvm = await _translator.FillObjectAsync(new ProfileVM());
            return View(pvm);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ProfileVM profileVM)
        {
            var res = await _translator.SaveObjectAsync(profileVM);
            return View("Profile");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ProfileVM profileVM)
        {
            var res = await _translator.SaveObjectAsync(profileVM);
            return View("Profile");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            await _auth.RegisterUserAsync(HttpContext, registerVM);
            return RedirectToAction("Index", "Book");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var sucLogin = await _auth.LoginUser(HttpContext, loginVM);
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
            await HttpContext.SignOutAsync(ConstanceHelper.AuthCookie);
            return RedirectToAction("Index", "Book");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrUpdateReadBook(int? bookId, int? readToPage)
        {
            if (bookId == null || readToPage == null)
            {
                return RedirectToAction(nameof(BookController.Index), nameof(BookController));
            }
            else
            {
                var res = await _translator.SaveObjectAsync(new ReadBookVM() { BookID = (int)bookId, ReadToPage = (int)readToPage });
                if (!res)
                    return BadRequest();
            }
            return RedirectToAction(nameof(BookController.Detail), nameof(BookController), new { id = bookId });
        }



    }
}
