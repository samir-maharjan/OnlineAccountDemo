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
     /*   public IActionResult accDeposit(Record record)
        {
            
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            DateTime currentDateTime = DateTime.Now;
            Record value = new Record();
            value.Task = record.Task;
            value.TaskPerformedDate = currentDateTime;
            value.EmployeeId = _ActiveUser.Id;
            value.Ipaddress = ip;
            _db.Records.Add(value);
            _db.SaveChanges();
            if (_ActiveUser.IsAdmin)
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("EmployeeTask");
        }
        public IActionResult accWithdrawl(Record record, String Type)
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
        }*/
      /*  public IActionResult deleteTask(int taskId)
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
