using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Internet_magazin.Enums;
using Internet_magazin.Services;
using Internet_magazin.Models;
using Internet_magazin.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Internet_magazin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICartService _cartService;
        public AccountController(IAccountService accountService, ICartService cartService)
        {
            _accountService = accountService;
            _cartService = cartService;
        }
        public IActionResult List()
        {
            return View(_accountService.GetList());
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM lvm)
        {
            Account acc = _accountService.LogIn(lvm.Login, lvm.Password);
            if(acc!=null)
            {
                Authenticate(acc);
            }
            return RedirectToAction("Index","Home");
            
        }
        public IActionResult logOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        private void Authenticate(Account acc)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim("CartId", acc.CartId.ToString()),
                new Claim("Id", acc.Id.ToString()),
                new Claim("Role", acc.Role.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", "Id", "Role");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM rvm)
        {
            _accountService.CheckForLoginAndEmail(rvm.Login, rvm.Email);
            Account acc = new Account
            {
                Name = rvm.Name,
                Role = Role.User,
                Id = Guid.NewGuid(),
                Login = rvm.Login,
                Address = rvm.Address,
                Email = rvm.Email,
                LastName = rvm.LastName,
                PhoneNumber = rvm.PhoneNumber,
                DateOfRegistration=DateTime.Now,
                Password = rvm.Password
            };
            if (rvm.AttachCart)
            {
                if(User.FindFirst("CartId")!=null)
                    acc.CartId= Guid.Parse(User.FindFirst("CartId").Value);
            }
            else
            {
                acc.CartId=_cartService.Create();
            }
            _accountService.Create(acc);
            Authenticate(acc);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Account account)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirst("Id").Value)))
            {
                account.Id=Guid.NewGuid();
                account.DateOfRegistration=DateTime.Now;
                account.CartId = _cartService.Create();
                _accountService.Create(account);
            }
            return RedirectToAction("List");
        }
        public IActionResult Details(Guid id)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirst("Id").Value)))
            {
                return View(_accountService.Get(id));
            }
            return RedirectToAction("List");
        }
        public IActionResult Update(Guid id)
        {
            return View(_accountService.Get(id));
        }
        [HttpPost]
        public IActionResult Update(Account account)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirst("Id").Value)))
            {
                _accountService.Update(account);
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(Guid id)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirst("Id").Value)))
            {
                _accountService.Delete(id);
            }
            return RedirectToAction("List");
        }
    }
}
