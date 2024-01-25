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
    public class RepairCategoryController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public RepairCategoryController(ApplicationDbContext db) : base()
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
                Address =UserAccDetails!.Address,
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

        /*        public UserInfoVM InitCommon()
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
        */

        /*        [HttpGet]
                public IActionResult accDeposit(int id)
                {
                    UserInfoVM userInfo = InitCommon();
                    return View(userInfo);
                }*/

        [UserAuthorization]
        [HttpPost]
            public IActionResult CreateBrand(BrandCategory brand)
        {
            BrandCategory brand_cat= new BrandCategory();
            brand_cat.BrandCode = brand.BrandCode;
            brand_cat.BrandTitle = brand.BrandTitle;
            brand_cat.Status = true;
            brand_cat.Deleted = false;
            brand_cat.CreatedBy = _ActiveUser.Name;
            brand_cat.UpdatedBy = _ActiveUser.Name;

            _db.BrandCategory.Add(brand_cat);
            _db.SaveChanges();
            return RedirectToAction("ListBrand");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreateBrand()
        {
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListBrand()
        {
            List<BrandCategory> brandList = _db.BrandCategory.ToList();
            return View(brandList);
        }

    }
}
