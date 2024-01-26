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
    public class ColorController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public ColorController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
            public IActionResult CreateColor(ModelColor model_)
        {
            ModelColor color_model = new ModelColor();
            color_model.ColorTitle = model_.ColorTitle;
            color_model.ColorCode = model_.ColorCode;
            color_model.Status = true;
            color_model.Deleted = false;
            color_model.CreatedBy = _ActiveUser.Name;
            color_model.UpdatedBy = _ActiveUser.Name;

            _db.ModelColor.Add(color_model);
            _db.SaveChanges();
            return RedirectToAction("ListColor");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreateColor()
        {
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListColor()
        {
            List<ModelColor> colorList = _db.ModelColor.ToList();
            return View(colorList);
        }

    }
}
