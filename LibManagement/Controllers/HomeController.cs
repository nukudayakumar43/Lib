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
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace LibManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppSettings _appSettings;
        

        public HomeController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        //[ValidateAntiForgeryToken]
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
                bool isExist = false;

                using (HttpClient httpClient = new HttpClient())
                {

                    HttpResponseMessage responseMessage = httpClient.GetAsync($"{_appSettings.WEBAPI}/api/UserDetail?username="+userName+"&password="+password).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        isExist = responseMessage.Content.ReadAsAsync<bool>().Result;
                    }
                }


                if (userName == "Admin" && isExist
                       )
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

        //[ValidateAntiForgeryToken]
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
