using Microsoft.AspNetCore.Mvc;
using OnlineAccountDemo.Data;
using OnlineAccountDemo.Models;
using OnlineAccountDemo.Helper;
using OnlineAccountDemo.CustomAttributes;
using OnlineAccountDemo.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace OnlineAccountDemo.Controllers
{
    [GeneralAuthorization]
    public class UserAccountDetailsController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public UserAccountDetailsController(ApplicationDbContext db) : base()
        {
            _db = db;

        }
        [UserAuthorization]
        public IActionResult AccountDetails()
        {
           Users? UserAccDetails = _db.Users.Where(x=>x.Id==_ActiveUser.Id).Include(x=>x.UsersAccount).FirstOrDefault();
            UserInfoVM userInfo = new UserInfoVM()
            {
                Id= _ActiveUser.Id,
                Address =UserAccDetails.Address,
                Name =UserAccDetails.Name,
                Email = UserAccDetails.Email,
                AccountNumber = UserAccDetails.UsersAccount.AccountNumber,
                TotalBalance=UserAccDetails.UsersAccount.TotalBalance,
                CreatedDate= UserAccDetails.CreatedDate,
                UpdatedDate= UserAccDetails.UsersAccount.UpdatedDate,
                Status= UserAccDetails.Status
            };
            return View(userInfo);
        }

        public UserInfoVM InitCommon()
        {
            Users? UserAccDetails = _db.Users.Where(x => x.Id == _ActiveUser.Id).Include(x => x.UsersAccount).FirstOrDefault();
            UserInfoVM userInfo = new UserInfoVM()
            {
                Id = _ActiveUser.Id,
                Address = UserAccDetails.Address,
                Name = UserAccDetails.Name,
                Email = UserAccDetails.Email,
                AccountNumber = UserAccDetails.UsersAccount.AccountNumber,
                TotalBalance = UserAccDetails.UsersAccount.TotalBalance,
                CreatedDate = UserAccDetails.CreatedDate,
                UpdatedDate = UserAccDetails.UsersAccount.UpdatedDate,
                Status = UserAccDetails.Status
            };
            return userInfo;
        }


        [HttpGet]
        public IActionResult accDeposit(int id)
        {
            UserInfoVM userInfo = InitCommon();
            return View(userInfo);
        }
        [HttpPost]
            public IActionResult accDeposited(int amt)
        {
            UsersAccount? userAccInfo = _db.UsersAccount.Where(x => x.UserId == _ActiveUser.Id).FirstOrDefault();
            userAccInfo.TotalBalance = userAccInfo.TotalBalance + amt;
            userAccInfo.UpdatedDate = DateTime.Now;
            _db.UsersAccount.Update(userAccInfo);
            _db.SaveChanges();
            return RedirectToAction("AccountDetails");
        }

        [HttpGet]
        public IActionResult accWithdraw(int id)
        {
            UserInfoVM userInfo = InitCommon();
            return View(userInfo);
        }
        [HttpPost]
        public IActionResult accWithdrawal(int amt)
        {
            try
            {
                UsersAccount? userAccInfo = _db.UsersAccount.Where(x => x.UserId == _ActiveUser.Id).FirstOrDefault();
                if (userAccInfo.TotalBalance > amt)
                {
                    userAccInfo.TotalBalance = userAccInfo.TotalBalance - amt;
                    userAccInfo.UpdatedDate = DateTime.Now;
                    _db.UsersAccount.Update(userAccInfo);
                    _db.SaveChanges();
                }
                else
                {
                    throw new Exception("Your Balance is Low");
                }

            }
            catch (Exception ex)
            {
            }

            return RedirectToAction("AccountDetails");
        }
        /* public IActionResult accWithdrawl(Record record, String Type)
         {
             Employee empData = SessionService.GetSession(HttpContext);
             Record value = _db.Records.Find(record.Id);
             value.Task = record.Task;
             _db.SaveChanges();
             if (empData.IsAdmin)
             {
                 return RedirectToAction("Index", "Admin");
             }
             return RedirectToAction("EmployeeTask");
         }
         public IActionResult deleteTask(int taskId)
         {
             Employee empData = SessionService.GetSession(HttpContext);
             Record data = _db.Records.Find(taskId);
             _db.Records.Remove(data);
             _db.SaveChanges();
             if (empData.IsAdmin)
             {
                 return RedirectToAction("Index", "Admin");
             }
             return RedirectToAction("EmployeeTask");

         }*/
    }
}
