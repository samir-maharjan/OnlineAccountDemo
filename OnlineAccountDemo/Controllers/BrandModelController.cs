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
    public class BrandModelController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public BrandModelController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
            public IActionResult CreateBrandModel(BrandModel model_)
        {
            BrandModel brand_model = new BrandModel();
            brand_model.ModelTitle = model_.ModelTitle;
            brand_model.BrandId = model_.BrandId;
            brand_model.ModelCode = model_.ModelCode;
            brand_model.Status = true;
            brand_model.Deleted = false;
            brand_model.CreatedBy = _ActiveUser.Name;
            brand_model.UpdatedBy = _ActiveUser.Name;

            _db.BrandModel.Add(brand_model);
            _db.SaveChanges();
            return RedirectToAction("ListBrandModel");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreateBrandModel()
        {
            ViewBag.BrandList = _db.BrandCategory.Where(x => x.Status).ToList();
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListBrandModel()
        {
            List<BrandModel> modelList = _db.BrandModel.Where(x=>x.Status).Include(x=>x.BrandCategory).ToList();
            return View(modelList);
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult UpdateModel(int? id)
        {
            ViewBag.BrandList = _db.BrandCategory.Where(x=>x.Status).ToList();

            BrandModel? _brandModel = _db.BrandModel.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_brandModel);
        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult UpdateModel(BrandModel brand)
        {
            BrandModel? _brandModel = _db.BrandModel.Where(x => x.Id == brand.Id)
              .FirstOrDefault();

            _brandModel!.ModelCode = brand.ModelCode;
            _brandModel.ModelTitle = brand.ModelTitle;
            _brandModel.UpdatedBy = _ActiveUser.Name;
            _brandModel.UpdatedDate = DateTime.Now;
            _db.BrandModel.Update(_brandModel);
            _db.SaveChanges();
            return RedirectToAction("ListBrandModel");
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult DeleteModel(int? id)
        {
            BrandModel? _brandModel = _db.BrandModel.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_brandModel);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult DeleteModel(BrandModel brand)
        {
            BrandModel? _brandModel = _db.BrandModel.Where(x => x.Id == brand.Id)
              .FirstOrDefault();

            _brandModel!.Status = false;
            _brandModel.Deleted = true;
            _brandModel.UpdatedBy = _ActiveUser.Name;
            _brandModel.UpdatedDate = DateTime.Now;
            _db.BrandModel.Update(_brandModel);
            _db.SaveChanges();
            return RedirectToAction("ListBrandModel");
        }

    }
}
