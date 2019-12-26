﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LibManagement.Models;
using LibManagementModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LibManagement.Controllers
{
    public class BookDetailController : Controller
    {
        private readonly AppSettings _appSettings;

        public BookDetailController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            List<BookDetail> list = new List<BookDetail>();
            using (HttpClient httpClient = new HttpClient())
            {

                HttpResponseMessage responseMessage = httpClient.GetAsync($"{_appSettings.WEBAPI}/api/Library").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    list = responseMessage.Content.ReadAsAsync<List<BookDetail>>().Result;
                }
            }


            return View(list);
        }


        [Authorize]
        [ActionName("BookFilter")]
        public IActionResult Index(string id)
        {
            List<BookDetail> list = new List<BookDetail>();
            using (HttpClient httpClient = new HttpClient())
            {

                HttpResponseMessage responseMessage = httpClient.GetAsync($"{_appSettings.WEBAPI}/api/Library/BookSearch?search=" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    list = responseMessage.Content.ReadAsAsync<List<BookDetail>>().Result;
                }
            }
            return View("Index", list);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(BookDetail bookDetail)
        {
            if (ModelState.IsValid)
            {
                List<BookDetail> list = new List<BookDetail>();
                using (HttpClient httpClient = new HttpClient())
                {

                    HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync($"{_appSettings.WEBAPI}/api/Library", bookDetail).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        list = responseMessage.Content.ReadAsAsync<List<BookDetail>>().Result;
                    }
                }

                return RedirectToAction("Index");
            }
            return View();
        }

        //[HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {

                HttpResponseMessage responseMessage = httpClient.DeleteAsync($"{_appSettings.WEBAPI}/api/Library/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    
                }

            }
            return RedirectToAction("Index");

        }

    }
}