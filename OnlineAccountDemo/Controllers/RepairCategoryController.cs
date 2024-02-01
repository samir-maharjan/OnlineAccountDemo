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
            List<BrandCategory> brandList = _db.BrandCategory.Where(x=>x.Status).ToList();
            return View(brandList);
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult UpdateBrand(int? id)
        {
            BrandCategory? _brand = _db.BrandCategory.Where(x => x.Id == id)
                .FirstOrDefault();
           
            return View(_brand);
        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult UpdateBrand(BrandCategory brand)
        {
            BrandCategory? _brand = _db.BrandCategory.Where(x => x.Id == brand.Id)
              .FirstOrDefault();

            _brand!.BrandCode = brand.BrandCode;
            _brand.BrandTitle = brand.BrandTitle;
            _brand.UpdatedBy = _ActiveUser.Name;
            _brand.UpdatedDate = DateTime.Now;
            _db.BrandCategory.Update(_brand);
            _db.SaveChanges();
            return RedirectToAction("ListBrand");
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult DeleteBrand(int? id)
        {
            BrandCategory? _brand = _db.BrandCategory.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_brand);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult DeleteBrand(BrandCategory brand)
        {
            BrandCategory? _brand = _db.BrandCategory.Where(x => x.Id == brand.Id)
              .FirstOrDefault();

            _brand!.Status = false;
            _brand.Deleted = true;
            _brand.UpdatedBy = _ActiveUser.Name;
            _brand.UpdatedDate = DateTime.Now;
            _db.BrandCategory.Update(_brand);
            _db.SaveChanges();
            return RedirectToAction("ListBrand");
        }

    }
}
