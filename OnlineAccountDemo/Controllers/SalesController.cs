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
    public class SalesController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public SalesController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult CreateSales(Sales model_)
        {
            Sales _Sales = new Sales();
            _Sales.BrandId = model_.BrandId;
            _Sales.ModelId = model_.ModelId;
            _Sales.Colorid = model_.Colorid;
            _Sales.StorageId = model_.StorageId;
            _Sales.Quantity = model_.Quantity;
            _Sales.Rate = model_.Rate;
            _Sales.Total = model_.Total;
            _Sales.PaymentMode = model_.PaymentMode;
            _Sales.Remarks = model_.Remarks;
            _Sales.EmpId = model_.EmpId;
            _Sales.InvoiceNum = model_.InvoiceNum;
            _Sales.IMEINumber = model_.IMEINumber;
            _Sales.Status = true;
            _Sales.Deleted = false;
            _Sales.CreatedBy = _ActiveUser.Name;
            _Sales.UpdatedBy = _ActiveUser.Name;

            _db.Sales.Add(_Sales);
            _db.SaveChanges();
            return RedirectToAction("ListSales");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreateSales()
        {
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.StorageList = _db.StorageCapacity.Where(s => s.Status).ToList();
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListSales()
        {
            // List<Sales> empList = _db.Sales.ToList();
            ViewBag.ModelList = _db.BrandModel.ToList();
            List<Sales> sales = _db.Sales.Where(x=>x.Status && !x.Deleted)
    .Include(ra => ra.BrandCategory)
    .Include(ra => ra.ModelColor)
    .Include(ra => ra.Employees)
    .ToList();
            return View(sales);
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult SalesReport()
        {
            ViewBag.ModelList = _db.BrandModel.Where(x => x.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(x => x.Status).ToList();
            ViewBag.IssuesList = _db.ModelIssues.Where(x => x.Status).ToList();
            ViewBag.StorageList = _db.StorageCapacity.Where(s => s.Status).ToList();

            List<Sales> sales = _db.Sales
                .Include(ra => ra.BrandCategory)
                .Include(ra => ra.ModelColor)
                .Include(ra => ra.Employees)
                .ToList();
            ViewBag.SalesList = sales;

            return View(sales);
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult UpdateSales(int? id)
        {
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.StorageList = _db.StorageCapacity.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();

            Sales? _Sales = _db.Sales.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_Sales);
        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult UpdateSales(Sales _tran)
        {
            Sales? _Sales = _db.Sales.Where(x => x.Id == _tran.Id)
              .FirstOrDefault();

            _Sales!.BrandId = _tran.BrandId;
            _Sales.ModelId = _tran.ModelId;
            _Sales.Colorid = _tran.Colorid;
            _Sales.EmpId = _tran.EmpId;
            _Sales.IMEINumber = _tran.IMEINumber;
            _Sales.Quantity = _tran.Quantity;
            _Sales.Rate = _tran.Rate;
            _Sales.Total = _tran.Total;
            _Sales.PaymentMode = _tran.PaymentMode;
            _Sales.Remarks = _tran.Remarks;
            _Sales.InvoiceNum = _tran.InvoiceNum;
            _Sales.UpdatedBy = _ActiveUser.Name;
            _Sales.UpdatedDate = DateTime.Now;
            _db.Sales.Update(_Sales);
            _db.SaveChanges();
            return RedirectToAction("ListSales");
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult DeleteSales(int? id)
        {
            ViewBag.EmployeesList = _db.Employees.Where(s => s.Status).ToList();
            ViewBag.ModelColor = _db.ModelColor.Where(s => s.Status).ToList();
            ViewBag.BrandList = _db.BrandCategory.Where(s => s.Status).ToList();
            ViewBag.ModelList = _db.BrandModel.Where(s => s.Status).ToList();
            ViewBag.StatusList = _db.JobStatus.Where(s => s.Status).ToList();
            ViewBag.PriceList = _db.IssuePricing.Where(s => s.Status).ToList();
            //todo: Have to implement INITCOMMON For ViewBag LISTS
            Sales? _Sales = _db.Sales.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_Sales);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult DeleteSales(Sales _tran)
        {
            Sales? _Sales = _db.Sales.Where(x => x.Id == _tran.Id)
              .FirstOrDefault();

            _Sales!.Status = false;
            _Sales.Deleted = true;
            _Sales.UpdatedBy = _ActiveUser.Name;
            _Sales.UpdatedDate = DateTime.Now;
            _db.Sales.Update(_Sales);
            _db.SaveChanges();
            return RedirectToAction("ListSales");
        }

    }
}
