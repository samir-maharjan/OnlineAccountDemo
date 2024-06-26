﻿using Microsoft.AspNetCore.Mvc;
using OnlineAccountDemo.Data;
using OnlineAccountDemo.Models;
using System.Security.Cryptography;
using System.Text;

namespace OnlineAccountDemo.Controllers
{
    // [GeneralAuthorization]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsersController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Users users)
        {
            try
            {
                Users newData = new Users();
                newData.Name = users.Name;
                newData.Address = users.Address;
                newData.Email = users.Email;
                newData.Password = HashPassword(users.Password);
                Random random = new Random();

                char[] digits = new char[10];
                for (int i = 0; i < 10; i++)
                {
                    digits[i] = (char)('0' + random.Next(10));
                }

                newData.UsersAccount = new UsersAccount()
                {
                    AccountNumber = new string(digits),
                    TotalBalance = 0

                };
                _db.Users.Add(newData);
                _db.SaveChanges();
                return RedirectToAction("Index", "Login");


            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Login");
             }



        }
        public string HashPassword(string password)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(password);
                    byte[] hashBytes = sha256.ComputeHash(bytes);
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }

                    return builder.ToString();
                }
            }
            catch(Exception ex)
            {
                return "Issue faced During Creating Password!";
            }
            
        }
    }
}
