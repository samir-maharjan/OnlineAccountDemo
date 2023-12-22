using OnlineAccountDemo.Helper;
using Microsoft.AspNetCore.Mvc;
using OnlineAccountDemo.Data;
using OnlineAccountDemo.Models;
using System.Security.Cryptography;
using System.Text;

namespace OnlineAccountDemo.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LoginController(ApplicationDbContext db)
        {

            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult authUser(String email, String password)
        {
            if (password != null && email != null)
            {
                Users? user = _db.Users.FirstOrDefault(x => x.Email == email && x.Deleted == false);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Incorrect Email";
                    return RedirectToAction("Index", "Login");
                }
                var passwordValue = user.Password;
                string hashedPassword = HashPassword(password);
                if (passwordValue != hashedPassword)
                {
                    TempData["ErrorMessage"] = "Incorrect password.";
                    return RedirectToAction("Index", "Login");
                }
                SessionService.SetSession(user, HttpContext);

                return RedirectToAction("AccountDetails", "UserAccountDetails");
            }
            TempData["ErrorMessage"] = "Empty Field";
            return RedirectToAction("Index", "Login");

        }

        public string HashPassword(string password)
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

        public IActionResult Logout()
        {
            SessionService.ClearSession(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}
