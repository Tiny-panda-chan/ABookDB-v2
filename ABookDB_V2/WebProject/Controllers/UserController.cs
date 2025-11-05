using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Helpers;
using WebProject.ModelTranslator;
using WebProject.ViewModels.User;

namespace WebProject.Controllers
{
    public class UserController(IModelTranslator _translator/*, IMapper _mapper*/) : Controller
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

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            AuthHelper auth = new AuthHelper();
            return View();
        }

        

        
    }
}
