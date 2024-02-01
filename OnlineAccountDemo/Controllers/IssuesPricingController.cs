using Microsoft.AspNetCore.Mvc;
using OnlineAccountDemo.Data;
using OnlineAccountDemo.Models;
using OnlineAccountDemo.Helper;
using OnlineAccountDemo.CustomAttributes;
using OnlineAccountDemo.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;

namespace OnlineAccountDemo.Controllers
{
    [GeneralAuthorization]
    public class IssuesPricingController : Controller
    {
        //private priceloyee priceData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public IssuesPricingController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
            public IActionResult CreatePricing(IssuePricing model_)
        {
            IssuePricing pricing_model = new IssuePricing();
            pricing_model.IssueModelId = model_.IssueModelId;
            pricing_model.IssueBrandId = model_.IssueBrandId;
            pricing_model.IssuesId = model_.IssuesId;
            pricing_model.IssuePrice = model_.IssuePrice;
            pricing_model.Status = true;
            pricing_model.Deleted = false;
            pricing_model.CreatedBy = _ActiveUser.Name;
            pricing_model.UpdatedBy = _ActiveUser.Name;

            _db.IssuePricing.Add(pricing_model);
            _db.SaveChanges();
            return RedirectToAction("ListIssuesPricing");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreatePricing()
        {
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();

            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListIssuesPricing()
        {
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();
            List<IssuePricing> priceList = _db.IssuePricing.Where(x=>x.Status).ToList();
            return View(priceList);
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult UpdatePricing(int? id)
        {

            IssuePricing? _IssuePricing = _db.IssuePricing.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_IssuePricing);
        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult UpdatePricing(IssuePricing price)
        {
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();

            IssuePricing? _IssuePricing = _db.IssuePricing.Where(x => x.Id == price.Id)
              .FirstOrDefault();

            _IssuePricing!.IssuesId = price.IssuesId;
            _IssuePricing.IssueBrandId = price.IssueBrandId;
            _IssuePricing.IssuePrice = price.IssuePrice;
            _IssuePricing.IssueModelId = price.IssueModelId;
            _IssuePricing.UpdatedBy = _ActiveUser.Name;
            _IssuePricing.UpdatedDate = DateTime.Now;
            _db.IssuePricing.Update(_IssuePricing);
            _db.SaveChanges();
            return RedirectToAction("ListIssuesPricing");
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult DeletePricing(int? id)
        {
            ViewBag.ModelList = _db.BrandModel.Where(s=>s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();

            IssuePricing? _IssuePricing = _db.IssuePricing.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_IssuePricing);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult DeletePricing(IssuePricing price)
        {
            IssuePricing? _IssuePricing = _db.IssuePricing.Where(x => x.Id == price.Id)
              .FirstOrDefault();

            _IssuePricing!.Status = false;
            _IssuePricing.Deleted = true;
            _IssuePricing.UpdatedBy = _ActiveUser.Name;
            _IssuePricing.UpdatedDate = DateTime.Now;
            _db.IssuePricing.Update(_IssuePricing);
            _db.SaveChanges();
            return RedirectToAction("ListIssuesPricing");
        }

    }
}
