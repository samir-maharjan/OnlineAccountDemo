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
    public class RepairAccessoriesController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public RepairAccessoriesController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult CreateRepairTransaction (RepairAccessories model_)
        {
            RepairAccessories _repair = new RepairAccessories();
            _repair.BrandId = model_.BrandId;
            _repair.ModelId = model_.ModelId;
            _repair.Colorid = model_.Colorid;
            _repair.IssueId = model_.IssueId;
            _repair.EmpId = model_.EmpId;
            _repair.StatusId = model_.StatusId;
            _repair.IMEINumber = model_.IMEINumber;
            _repair.BatteryPercent = model_.BatteryPercent;
            var _Price = _db.IssuePricing.Where(x=>x.IssueBrandId== model_.BrandId && x.IssueModelId== model_.ModelId && x.IssuesId == model_.IssueId).Select(x=>x.IssuePrice).FirstOrDefault();
            _repair.Price = _Price == null? 0 : _Price;
            _repair.Status = true;
            _repair.Deleted = false;
            _repair.CreatedBy = _ActiveUser.Name;
            _repair.UpdatedBy = _ActiveUser.Name;

            _db.RepairAccessories.Add(_repair);
            _db.SaveChanges();
            return RedirectToAction("ListRepairTransaction");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreateRepairTransaction()
        {
            ViewBag.EmployeesList = _db.Employees.ToList();
            ViewBag.IssuesList = _db.ModelIssues.ToList();
            ViewBag.ModelColor = _db.ModelColor.ToList();
            ViewBag.BrandList = _db.BrandCategory.ToList();
            ViewBag.ModelList = _db.BrandModel.ToList();
            ViewBag.StatusList = _db.JobStatus.ToList();
            ViewBag.PriceList = _db.IssuePricing.ToList();
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListRepairTransaction()
        {
            // List<RepairAccessories> empList = _db.RepairAccessories.ToList();
            ViewBag.ModelList = _db.BrandModel.ToList();
            List<RepairAccessories> empList = _db.RepairAccessories
    .Include(ra => ra.BrandCategory)
    .Include(ra => ra.ModelColor)
    .Include(ra => ra.ModelIssues)
    .Include(ra => ra.Employees)
    .Include(ra => ra.JobStatus)
    .ToList();
            return View(empList);
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult RepairTransactionReport()
        {
            ViewBag.ModelList = _db.BrandModel.ToList();
            ViewBag.BrandList = _db.BrandCategory.ToList();
            ViewBag.IssuesList = _db.ModelIssues.ToList();
            ViewBag.PricingList = _db.IssuePricing.ToList();

            List<RepairAccessories> repairList = _db.RepairAccessories
    .Include(ra => ra.BrandCategory)
/*        .ThenInclude(bm => bm.BrandModel)*/
    .Include(ra => ra.ModelColor)
    .Include(ra => ra.ModelIssues)
    .Include(ra => ra.Employees)
    .Include(ra => ra.JobStatus)
    .ToList();
            ViewBag.RepairList = repairList;
            /*List<VMRepairTransactionReport> _report = new List<VMRepairTransactionReport>();

            foreach (var item in repairList.GroupBy(x=>x.BrandId))
            {
                foreach (var item1 in item.GroupBy(x=>x.ModelId))
                {
                    var issues = item1.Select(x => x.IssueId).Distinct().ToList();
                    foreach (var item2 in issues)
                    {
                        _report.Add(new VMRepairTransactionReport
                        {
                            Brand = item.Select(x => x.BrandCategory.BrandTitle).FirstOrDefault(),
                            Model = item1.Key,
                            ModelIssues = item2,
                            IssueCount= item1.Where(x=>x.ModelId == item2).Count()
                        });
                    }

                }

            }*/

            return View(repairList);
        }

    }
}
