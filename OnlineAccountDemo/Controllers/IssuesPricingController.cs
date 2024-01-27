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
        //private Employee empData;
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
            ViewBag.ModelList = _db.BrandModel.ToList();
            ViewBag.BrandList = _db.BrandCategory.ToList();
            ViewBag.IssuesList = _db.ModelIssues.ToList();

            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListIssuesPricing()
        {
            ViewBag.ModelList = _db.BrandModel.ToList();
            ViewBag.BrandList = _db.BrandCategory.ToList();
            ViewBag.IssuesList = _db.ModelIssues.ToList();
            List<IssuePricing> priceList = _db.IssuePricing.ToList();
            return View(priceList);
        }

    }
}
