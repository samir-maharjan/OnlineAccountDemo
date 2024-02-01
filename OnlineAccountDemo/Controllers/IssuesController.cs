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
    public class IssuesController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public IssuesController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
            public IActionResult CreateIssues(ModelIssues model_)
        {
            ModelIssues issues_model = new ModelIssues();
            issues_model.IssueTitle = model_.IssueTitle;
            issues_model.IssueCode = model_.IssueCode;
            issues_model.Status = true;
            issues_model.Deleted = false;
            issues_model.CreatedBy = _ActiveUser.Name;
            issues_model.UpdatedBy = _ActiveUser.Name;

            _db.ModelIssues.Add(issues_model);
            _db.SaveChanges();
            return RedirectToAction("ListIssues");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreateIssues()
        {
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListIssues()
        {
            List<ModelIssues> issuesList = _db.ModelIssues.Where(x=>x.Status).ToList();
            return View(issuesList);
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult UpdateIssue(int? id)
        {

            ModelIssues? _ModelIssues = _db.ModelIssues.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_ModelIssues);
        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult UpdateIssue(ModelIssues issue)
        {
            ModelIssues? _ModelIssues = _db.ModelIssues.Where(x => x.Id == issue.Id)
              .FirstOrDefault();

            _ModelIssues!.IssueCode = issue.IssueCode;
            _ModelIssues.IssueTitle = issue.IssueTitle;
            _ModelIssues.UpdatedBy = _ActiveUser.Name;
            _ModelIssues.UpdatedDate = DateTime.Now;
            _db.ModelIssues.Update(_ModelIssues);
            _db.SaveChanges();
            return RedirectToAction("ListIssues");
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult DeleteIssue(int? id)
        {
            ModelIssues? _ModelIssues = _db.ModelIssues.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_ModelIssues);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult DeleteIssue(ModelIssues issue)
        {
            ModelIssues? _ModelIssues = _db.ModelIssues.Where(x => x.Id == issue.Id)
              .FirstOrDefault();

            _ModelIssues!.Status = false;
            _ModelIssues.Deleted = true;
            _ModelIssues.UpdatedBy = _ActiveUser.Name;
            _ModelIssues.UpdatedDate = DateTime.Now;
            _db.ModelIssues.Update(_ModelIssues);
            _db.SaveChanges();
            return RedirectToAction("ListIssues");
        }

    }
}
