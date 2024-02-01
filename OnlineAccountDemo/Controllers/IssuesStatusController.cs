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
            List<JobStatus> statusList = _db.JobStatus.Where(x=>x.Status).ToList();
            return View(statusList);
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult UpdateIssueStatus(int? id)
        {

            JobStatus? _JobStatus = _db.JobStatus.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_JobStatus);
        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult UpdateIssueStatus(JobStatus job)
        {
            JobStatus? _JobStatus = _db.JobStatus.Where(x => x.Id == job.Id)
              .FirstOrDefault();

            _JobStatus!.StatusCode = job.StatusCode;
            _JobStatus.StatusTitle = job.StatusTitle;
            _JobStatus.UpdatedBy = _ActiveUser.Name;
            _JobStatus.UpdatedDate = DateTime.Now;
            _db.JobStatus.Update(_JobStatus);
            _db.SaveChanges();
            return RedirectToAction("ListIssueStatus");
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult DeleteIssueStatus(int? id)
        {
            JobStatus? _JobStatus = _db.JobStatus.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_JobStatus);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult DeleteIssueStatus(JobStatus job)
        {
            JobStatus? _JobStatus = _db.JobStatus.Where(x => x.Id == job.Id)
              .FirstOrDefault();

            _JobStatus!.Status = false;
            _JobStatus.Deleted = true;
            _JobStatus.UpdatedBy = _ActiveUser.Name;
            _JobStatus.UpdatedDate = DateTime.Now;
            _db.JobStatus.Update(_JobStatus);
            _db.SaveChanges();
            return RedirectToAction("ListIssueStatus");
        }

    }
}
