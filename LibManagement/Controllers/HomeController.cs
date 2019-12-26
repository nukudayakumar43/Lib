using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibManagement.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using LibManagementModel;

namespace LibManagement.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "BookDetail");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Index(UserDetail userDetail)
        {
            
            if (ModelState.IsValid)
            {
                string userName = userDetail.UserName;
                string password = userDetail.Password;
                if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
                {
                    return RedirectToAction("Login");
                }

                //Check the user name and password  
                //Here can be implemented checking logic from the database  
                ClaimsIdentity identity;
                if (userName == "Admin" && password == "password")
                {

                    //Create the identity for the user  
                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                }
                else
                {
                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "user")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                }
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "BookDetail");

            }
            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
