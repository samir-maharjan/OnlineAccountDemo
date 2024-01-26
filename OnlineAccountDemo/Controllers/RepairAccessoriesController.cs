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
            _repair.Price = model_.Price;
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
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListRepairTransaction()
        {
           // List<RepairAccessories> empList = _db.RepairAccessories.ToList();
            List<RepairAccessories> empList = _db.RepairAccessories
    .Include(ra => ra.BrandCategory)
        .ThenInclude(bm => bm.BrandModel).ThenInclude(x=>x.br)
    .Include(ra => ra.ModelColor)
    .Include(ra => ra.ModelIssues)
    .Include(ra => ra.Employees)
    .Include(ra => ra.JobStatus)
    .ToList();
            return View(empList);
        }

    }
}
