using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Internet_magazin.Models;
using Internet_magazin.Services;
using Internet_magazin.ViewModels;

namespace Internet_magazin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IAccountService _accountService;
        private readonly IItemInOrderService _itemInOrderService;
        public OrderController(IOrderService orderService, ICartService cartService, IAccountService accountService, IItemInOrderService itemInOrderService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _accountService = accountService;
            _itemInOrderService = itemInOrderService;
        }
        public IActionResult list()
        {
            return View(_orderService.GetAll());
        }
        public IActionResult Create()
        {
            if (User.FindFirst("Id") != null)
            {
                Account acc = _accountService.Get(Guid.Parse(User.FindFirstValue("Id")));
                if (acc != null)
                {
                    if (User.FindFirstValue("CartId") != null && _cartService.Check(Guid.Parse(User.FindFirstValue("CartId"))))
                    {
                        Order order = new Order()
                        {
                            PhoneNumber = acc.PhoneNumber,
                            Address = acc.Address,
                            Id = Guid.NewGuid(),
                            Date = DateTime.Now,
                            BuyerName = acc.Name,
                            BuyerLastName = acc.LastName,
                            Email = acc.Email,
                            AccountId = acc.Id,
                            Сondition = "open"
                        };
                        _orderService.Create(order);
                        _itemInOrderService.MoveFromCart(Guid.Parse(User.FindFirstValue("CartId")),order.Id);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateOrderVM COVM)
        {
            if (User.FindFirstValue("CartId") != null&&_cartService.Check(Guid.Parse(User.FindFirstValue("CartId"))))
            {
                Order order=new Order()
                {
                    PhoneNumber = COVM.PhoneNumber,
                    Address = COVM.Address,
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    BuyerName = COVM.Name,
                    BuyerLastName = COVM.LastName,
                    Email = COVM.Email,
                    Сondition = "open"
                };
                _orderService.Create(order);
                _itemInOrderService.MoveFromCart(Guid.Parse(User.FindFirstValue("CartId")), order.Id);

            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult Update(Guid id)
        {
            return View(_orderService.Get(id));
        }
        [HttpPost]
        public IActionResult Update(Order order)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirst("Id").Value)))
            {
                _orderService.Update(order);
            }
            return RedirectToAction("list");
        }
        public IActionResult Delete(Guid id)
        {
            if (User.FindFirst("Id") != null && _accountService.IsAdmin(Guid.Parse(User.FindFirst("Id").Value)))
            {
                _orderService.Delete(id);
            }
            return RedirectToAction("list");
        }
    }
}
