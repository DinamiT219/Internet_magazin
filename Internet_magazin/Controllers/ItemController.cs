using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Internet_magazin.Enums;
using Internet_magazin.Models;
using Internet_magazin.Services;
using Internet_magazin.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Internet_magazin.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ISubDivisionService _subDivisionService;
        private readonly IAccountService _accountService;

        public ItemController(IItemService itemService, ISubDivisionService subDivisionService, IAccountService accountService)
        {
            _itemService = itemService;
            _subDivisionService = subDivisionService;
            _accountService = accountService;
        }
        public IActionResult Index(Guid subDivisionId,string search="",Sort sort=Sort.Null)
        {
            return View(_itemService.GetList(subDivisionId,search,sort));
        }
        public IActionResult Details(Guid id)
        {
            return View(_itemService.GetOne(id));
        }
        public IActionResult Create()
        {
            ViewBag.SubDivisions = new SelectList(_subDivisionService.Get(),"Id","Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateItemVM civm)
        {
            if (User.FindFirst("Id")!=null&&_accountService.IsAdmin(Guid.Parse(User.FindFirstValue("Id"))))
            {
                Item item = new Item
                {
                    Name = civm.Name,
                    Id = Guid.NewGuid(),
                    Count = civm.Count,
                    DateOfAdding = DateTime.Now,
                    Description = civm.Description,
                    ImgPath = civm.ImgPath,
                    Manufacturer = civm.Manufacturer,
                    Price = civm.Price,
                    SubDivisionId = civm.SubDivisionId
                };
                _itemService.Create(item);
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult Update(Guid id)
        {
            ViewBag.SubDivisions = new SelectList(_subDivisionService.Get(), "Id", "Name");
            return View(_itemService.GetOne(id));
        }
        [HttpPost]
        public IActionResult Update(Item item)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirstValue("Id"))))
            {
                _itemService.Update(item);
            }
            return RedirectToAction("Details",new {Id=item.Id});
        }
        public IActionResult Delete(Guid id)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirstValue("Id"))))
            {
                _itemService.Delete(id);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
