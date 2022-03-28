using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Internet_magazin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Internet_magazin.Controllers
{
    public class ItemInCartController : Controller
    {
        private readonly IItemInCartService _itemInCartService;
        private readonly ICartService _cartService;
        public ItemInCartController(IItemInCartService itemInCartService,ICartService cartService)
        {
            _itemInCartService = itemInCartService;
            _cartService = cartService;
        }
        public IActionResult Index()
        {
            if(User.FindFirst("CartId")!=null)
            return View(_itemInCartService.GetFromCart(Guid.Parse(User.FindFirst("CartId").Value)));
            return View();
        }
        public async Task<IActionResult> Add(Guid itemId, ulong count=1)
        {
            Guid cartId;
            if(User.FindFirst("CartId")==null||!_cartService.Check(Guid.Parse(User.FindFirst("CartId").Value)))
            {
                cartId = await RegisterCart();
            }
            else
            {
                cartId = Guid.Parse(User.FindFirstValue("CartId"));
            }
            _itemInCartService.Add(cartId,itemId,count);
            return RedirectToAction("Details", "Item",new {id=itemId});
        }
        public IActionResult Delete(Guid id,ulong count=1)
        {
            _itemInCartService.Delete(id,count);
            return RedirectToAction("Index", "ItemInCart");
        }
        private async Task<Guid> RegisterCart()
        {
            Guid cartId = _cartService.Create();
            var claims = User.Claims.ToList();
            claims.Remove(claims.FirstOrDefault(cl => cl.Type == "CartId"));
            claims.Add(new Claim("CartId", cartId.ToString()));
            ClaimsIdentity claimsi = new ClaimsIdentity(claims, "ApplicationCookie");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsi),new AuthenticationProperties { IsPersistent = true, ExpiresUtc = new DateTimeOffset(DateTime.Now.AddMonths(6))});
            return cartId;
        }
    }
}
