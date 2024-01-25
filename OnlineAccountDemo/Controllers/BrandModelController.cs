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
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListBrandModel()
        {
            List<BrandModel> modelList = _db.BrandModel.Include(x=>x.BrandCategory).ToList();
            return View(modelList);
        }

    }
}
