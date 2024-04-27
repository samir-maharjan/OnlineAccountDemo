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
        public IActionResult CreateRepairTransaction(RepairAccessories model_)
        {
            RepairAccessories _repair = new RepairAccessories();
            _repair.BrandId = model_.BrandId;
            _repair.ModelId = model_.ModelId;
            _repair.Colorid = model_.Colorid;
            _repair.IssueId = model_.IssueId;
            _repair.EmpId = model_.EmpId;
            _repair.StatusId = model_.StatusId;
            _repair.IMEINumber = model_.IMEINumber;
            _repair.Remarks = model_.Remarks;
            /*            var _Price = _db.IssuePricing.Where(x => x.IssueBrandId == model_.BrandId && x.IssueModelId == model_.ModelId && x.IssuesId == model_.IssueId).Select(x => x.IssuePrice).FirstOrDefault();
                        _repair.Price = _Price == null ? 0 : _Price;*/
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
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.StatusList = _db.JobStatus.Where(s => s.Status).ToList();
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListRepairTransaction()
        {
            // List<RepairAccessories> empList = _db.RepairAccessories.ToList();
            ViewBag.ModelList = _db.BrandModel.ToList();
            List<RepairAccessories> empList = _db.RepairAccessories.Where(x=>x.Status && !x.Deleted)
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
            ViewBag.ModelList = _db.BrandModel.Where(x => x.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(x => x.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(x => x.Status).ToList();
            ViewBag.PricingList = _db.IssuePricing.Where(x => x.Status).ToList();

            List<RepairAccessories> repairList = _db.RepairAccessories
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
        public IActionResult UpdateRepairTran(int? id)
        {
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.StatusList = _db.JobStatus.Where(s => s.Status).ToList();

            RepairAccessories? _RepairAccessories = _db.RepairAccessories.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_RepairAccessories);
        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult UpdateRepairTran(RepairAccessories _tran)
        {
            RepairAccessories? _RepairAccessories = _db.RepairAccessories.Where(x => x.Id == _tran.Id)
              .FirstOrDefault();

            _RepairAccessories!.BrandId = _tran.BrandId;
            _RepairAccessories.ModelId = _tran.ModelId;
            _RepairAccessories.Colorid = _tran.Colorid;
            _RepairAccessories.IssueId = _tran.IssueId;
            _RepairAccessories.EmpId = _tran.EmpId;
            _RepairAccessories.StatusId = _tran.StatusId;
            _RepairAccessories.IMEINumber = _tran.IMEINumber;
            _RepairAccessories.Remarks = _tran.Remarks;
            /*            var _Price = _db.IssuePricing.Where(x => x.IssueBrandId == _tran.BrandId && x.IssueModelId == _tran.ModelId && x.IssuesId == _tran.IssueId).Select(x => x.IssuePrice).FirstOrDefault();
                        _tran.Price = _Price == null ? 0 : _Price;*/
            _RepairAccessories.UpdatedBy = _ActiveUser.Name;
            _RepairAccessories.UpdatedDate = DateTime.Now;
            _db.RepairAccessories.Update(_RepairAccessories);
            _db.SaveChanges();
            return RedirectToAction("ListRepairTransaction");
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult DeleteRepairTran(int? id)
        {
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.StatusList = _db.JobStatus.Where(s => s.Status).ToList();
            //todo: Have to implement INITCOMMON For ViewBag LISTS
            RepairAccessories? _RepairAccessories = _db.RepairAccessories.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_RepairAccessories);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult DeleteRepairTran(RepairAccessories _tran)
        {
            RepairAccessories? _RepairAccessories = _db.RepairAccessories.Where(x => x.Id == _tran.Id)
              .FirstOrDefault();

            _RepairAccessories!.Status = false;
            _RepairAccessories.Deleted = true;
            _RepairAccessories.UpdatedBy = _ActiveUser.Name;
            _RepairAccessories.UpdatedDate = DateTime.Now;
            _db.RepairAccessories.Update(_RepairAccessories);
            _db.SaveChanges();
            return RedirectToAction("ListRepairTransaction");
        }

    }
}
