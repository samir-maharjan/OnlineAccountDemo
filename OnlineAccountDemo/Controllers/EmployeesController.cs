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
    public class EmployeesController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public EmployeesController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult CreateEmp (Employees model_)
        {
            Employees _emp = new Employees();
            _emp.EmpName = model_.EmpName;
            _emp.EmpCode = model_.EmpCode;
            _emp.EmpDesignation = model_.EmpDesignation;
            _emp.Status = true;
            _emp.Deleted = false;
            _emp.CreatedBy = _ActiveUser.Name;
            _emp.UpdatedBy = _ActiveUser.Name;

            _db.Employees.Add(_emp);
            _db.SaveChanges();
            return RedirectToAction("ListEmp");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreateEmp()
        {
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListEmp()
        {
            List<Employees> empList = _db.Employees.ToList();
            return View(empList);
        }

    }
}
