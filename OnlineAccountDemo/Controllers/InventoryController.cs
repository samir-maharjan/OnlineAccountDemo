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
    public class InventoryController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public InventoryController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult CreateInventory(Inventory model_)
        {
            Inventory _repair = new Inventory();
            _repair.BrandId = model_.BrandId;
            _repair.ModelId = model_.ModelId;
            _repair.Colorid = model_.Colorid;
            _repair.IssueId = model_.IssueId;
            _repair.EmpId = model_.EmpId;
            _repair.StatusId = model_.StatusId;
            _repair.IMEINumber = model_.IMEINumber;
            _repair.BatteryPercent = model_.BatteryPercent;
            var _Price = _db.IssuePricing.Where(x => x.IssueBrandId == model_.BrandId && x.IssueModelId == model_.ModelId && x.IssuesId == model_.IssueId).Select(x => x.IssuePrice).FirstOrDefault();
            _repair.Price = _Price == null ? 0 : _Price;
            _repair.Status = true;
            _repair.Deleted = false;
            _repair.CreatedBy = _ActiveUser.Name;
            _repair.UpdatedBy = _ActiveUser.Name;

            _db.Inventory.Add(_repair);
            _db.SaveChanges();
            return RedirectToAction("ListRepairTransaction");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreateInventory()
        {
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.StatusList = _db.JobStatus.Where(s => s.Status).ToList();
            ViewBag.PriceList = _db.IssuePricing.Where(s => s.Status).ToList();
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListInventory()
        {
            // List<Inventory> empList = _db.Inventory.ToList();
            ViewBag.ModelList = _db.BrandModel.ToList();
            List<Inventory> empList = _db.Inventory
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
        public IActionResult InventoryReport()
        {
            ViewBag.ModelList = _db.BrandModel.Where(x => x.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(x => x.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(x => x.Status).ToList();
            ViewBag.PricingList = _db.IssuePricing.Where(x => x.Status).ToList();

            List<Inventory> repairList = _db.Inventory
                .Include(ra => ra.BrandCategory)
                .Include(ra => ra.ModelColor)
                .Include(ra => ra.ModelIssues)
                .Include(ra => ra.Employees)
                .Include(ra => ra.JobStatus)
                .ToList();
            ViewBag.RepairList = repairList;

            return View(repairList);
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult UpdateInventory(int? id)
        {
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.StatusList = _db.JobStatus.Where(s => s.Status).ToList();
            ViewBag.PriceList = _db.IssuePricing.Where(s => s.Status).ToList();


            Inventory? _Inventory = _db.Inventory.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_Inventory);
        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult UpdateInventory(Inventory _tran)
        {
            Inventory? _Inventory = _db.Inventory.Where(x => x.Id == _tran.Id)
              .FirstOrDefault();

            _Inventory!.BrandId = _tran.BrandId;
            _Inventory.ModelId = _tran.ModelId;
            _Inventory.Colorid = _tran.Colorid;
            _Inventory.IssueId = _tran.IssueId;
            _Inventory.EmpId = _tran.EmpId;
            _Inventory.StatusId = _tran.StatusId;
            _Inventory.IMEINumber = _tran.IMEINumber;
            _Inventory.BatteryPercent = _tran.BatteryPercent;
            var _Price = _db.IssuePricing.Where(x => x.IssueBrandId == _tran.BrandId && x.IssueModelId == _tran.ModelId && x.IssuesId == _tran.IssueId).Select(x => x.IssuePrice).FirstOrDefault();
            _tran.Price = _Price == null ? 0 : _Price;
            _Inventory.UpdatedBy = _ActiveUser.Name;
            _Inventory.UpdatedDate = DateTime.Now;
            _db.Inventory.Update(_Inventory);
            _db.SaveChanges();
            return RedirectToAction("ListRepairTransaction");
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult DeleteInventory(int? id)
        {
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.StatusList = _db.JobStatus.Where(s => s.Status).ToList();
            ViewBag.PriceList = _db.IssuePricing.Where(s => s.Status).ToList();
            //todo: Have to implement INITCOMMON For ViewBag LISTS
            Inventory? _Inventory = _db.Inventory.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_Inventory);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult DeleteInventory(Inventory _tran)
        {
            Inventory? _Inventory = _db.Inventory.Where(x => x.Id == _tran.Id)
              .FirstOrDefault();

            _Inventory!.Status = false;
            _Inventory.Deleted = true;
            _Inventory.UpdatedBy = _ActiveUser.Name;
            _Inventory.UpdatedDate = DateTime.Now;
            _db.Inventory.Update(_Inventory);
            _db.SaveChanges();
            return RedirectToAction("ListRepairTransaction");
        }

    }
}
