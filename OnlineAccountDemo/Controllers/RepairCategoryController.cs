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
