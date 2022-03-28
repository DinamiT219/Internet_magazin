using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Internet_magazin.Models;
using Internet_magazin.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Internet_magazin.Controllers
{
    public class SubDivisionController : Controller
    {
        private readonly ISubDivisionService _subDivisionService;
        private readonly IAccountService _accountService;
        private readonly IDivisionService _divisionService;

        public SubDivisionController(ISubDivisionService subDivisionService, IAccountService accountService, IDivisionService divisionService)
        {
            _subDivisionService = subDivisionService;
            _accountService = accountService;
            _divisionService = divisionService;
        }

        public IActionResult Index(Guid dId)
        {
            return View(_subDivisionService.Get(dId));
        }
        public IActionResult Create()
        {
            ViewBag.Divisions = new SelectList(_divisionService.Get(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(SubDivision subDivision)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirstValue("Id"))))
            {
                subDivision.Id = Guid.NewGuid();
                _subDivisionService.Create(subDivision);
            }
            return Index(subDivision.DivisionId);
        }
        public IActionResult Update(Guid id)
        {
            ViewBag.Divisions = new SelectList(_divisionService.Get(), "Id", "Name");
            return View(_subDivisionService.GetOne(id));
        }
        [HttpPost]
        public IActionResult Update(SubDivision subDivision)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirstValue("Id"))))
            {
                _subDivisionService.Update(subDivision);
            }
            return Index(subDivision.DivisionId);
        }
        public IActionResult Delete(Guid id)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirstValue("Id"))))
            {
                _subDivisionService.Delete(id);
            }
            return Index(_subDivisionService.GetOne(id).DivisionId);
        }
    }
}
