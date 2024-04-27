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
            Inventory _inventory = new Inventory();
            _inventory.BrandId = model_.BrandId;
            _inventory.ModelId = model_.ModelId;
            _inventory.Colorid = model_.Colorid;
            _inventory.IssueId = model_.IssueId;
            _inventory.StorageId = model_.StorageId;
            _inventory.Quantity = model_.Quantity;
            _inventory.Remarks = model_.Remarks;
            _inventory.EmpId = model_.EmpId;
            _inventory.IMEINumber = model_.IMEINumber;
            _inventory.BatteryPercent = model_.BatteryPercent;
            _inventory.Status = true;
            _inventory.Deleted = false;
            _inventory.CreatedBy = _ActiveUser.Name;
            _inventory.UpdatedBy = _ActiveUser.Name;

            _db.Inventory.Add(_inventory);
            _db.SaveChanges();
            return RedirectToAction("ListInventory");
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
            ViewBag.StorageList = _db.StorageCapacity.Where(s => s.Status).ToList();
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
            ViewBag.StorageList = _db.StorageCapacity.Where(s => s.Status).ToList();

            List<Inventory> repairList = _db.Inventory
                .Include(ra => ra.BrandCategory)
                .Include(ra => ra.ModelColor)
                .Include(ra => ra.ModelIssues)
                .Include(ra => ra.Employees)
                .ToList();
            ViewBag.RepairList = repairList;

            return View(repairList);
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult UpdateInventory(int? id)
        {
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.StorageList = _db.StorageCapacity.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();

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
            _Inventory.IMEINumber = _tran.IMEINumber;
            _Inventory.BatteryPercent = _tran.BatteryPercent;
            var _Price = _db.IssuePricing.Where(x => x.IssueBrandId == _tran.BrandId && x.IssueModelId == _tran.ModelId && x.IssuesId == _tran.IssueId).Select(x => x.IssuePrice).FirstOrDefault();
            _Inventory.UpdatedBy = _ActiveUser.Name;
            _Inventory.UpdatedDate = DateTime.Now;
            _db.Inventory.Update(_Inventory);
            _db.SaveChanges();
            return RedirectToAction("ListInventory");
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
            return RedirectToAction("ListInventory");
        }

    }
}
