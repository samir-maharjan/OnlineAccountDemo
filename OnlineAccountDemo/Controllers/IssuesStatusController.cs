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
    public class IssuesStatusController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public IssuesStatusController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
            public IActionResult CreateIssueStatus(JobStatus model_)
        {
            JobStatus _stat = new JobStatus();
            _stat.StatusTitle = model_.StatusTitle;
            _stat.StatusCode = model_.StatusCode;
            _stat.Status = true;
            _stat.Deleted = false;
            _stat.CreatedBy = _ActiveUser.Name;
            _stat.UpdatedBy = _ActiveUser.Name;

            _db.JobStatus.Add(_stat);
            _db.SaveChanges();
            return RedirectToAction("ListIssueStatus");
        }

        [UserAuthorization]
        [HttpGet]   
        public IActionResult CreateIssueStatus()
        {
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListIssueStatus()
        {
            List<JobStatus> statusList = _db.JobStatus.ToList();
            return View(statusList);
        }

    }
}
