using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internet_magazin.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Internet_magazin.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        public async void add()
        {
            Guid cartId =cartService.Create();
            var claims = User.Claims.ToList();
            claims.Remove(claims.FirstOrDefault(cl=>cl.Type=="CartId"));
            claims.Add(new Claim("CartId",cartId.ToString()));
            ClaimsIdentity claimsi = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsi), new AuthenticationProperties { IsPersistent = true, ExpiresUtc = new DateTimeOffset(DateTime.Now.AddMonths(6)) });
        }
    }
}
