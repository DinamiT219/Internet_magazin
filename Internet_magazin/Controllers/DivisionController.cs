using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Internet_magazin.Models;
using Internet_magazin.Services;

namespace Internet_magazin.Controllers
{
    public class DivisionController : Controller
    {
        private readonly IDivisionService _divisionService;
        private readonly IAccountService _accountService;

        public DivisionController(IDivisionService divisionService, IAccountService accountService)
        {
            _divisionService = divisionService;
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View(_divisionService.Get());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Division division)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirstValue("Id"))))
            {
                division.Id=Guid.NewGuid();
                _divisionService.Create(division);
            }
            return Index();
        }
        public IActionResult Update(Guid id)
        {
            return View(_divisionService.Get(id));
        }
        [HttpPost]
        public IActionResult Update(Division division)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirstValue("Id"))))
            {
                _divisionService.Update(division);
            }
            return Index();
        }
        public IActionResult Delete(Guid id)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirstValue("Id"))))
            {
                _divisionService.Delete(id);
            }
            return Index();
        }
    }
}
