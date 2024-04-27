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
    public class StorageCapacityController : Controller
    {
        //private Employee empData;
        private readonly ApplicationDbContext _db;
        private Users _ActiveUser => SessionService.GetSession(HttpContext);
        public StorageCapacityController(ApplicationDbContext db) : base()
        {
            _db = db;

        }

        [UserAuthorization]
        [HttpPost]
            public IActionResult CreateStorageCapacity(StorageCapacity model_)
        {
            StorageCapacity storage_model = new StorageCapacity();
            storage_model.StorageTitle = model_.StorageTitle;
            storage_model.Status = true;
            storage_model.Deleted = false;
            storage_model.CreatedBy = _ActiveUser.Name;
            storage_model.UpdatedBy = _ActiveUser.Name;

            _db.StorageCapacity.Add(storage_model);
            _db.SaveChanges();
            return RedirectToAction("ListStorageCapacity");
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult CreateStorageCapacity()
        {
            return View();
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult ListStorageCapacity()
        {
            List<StorageCapacity> storageList = _db.StorageCapacity.Where(x=>x.Status).ToList();
            return View(storageList);
        }

        [UserAuthorization]
        [HttpGet]
        public IActionResult UpdateStorageCapacity(int? id)
        {

            StorageCapacity? _StorageCapacity = _db.StorageCapacity.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_StorageCapacity);
        }

        [UserAuthorization]
        [HttpPost]
        public IActionResult UpdateStorageCapacity(StorageCapacity storage)
        {
            StorageCapacity? _StorageCapacity = _db.StorageCapacity.Where(x => x.Id == storage.Id)
              .FirstOrDefault();

            _StorageCapacity!.StorageTitle = storage.StorageTitle;
            _StorageCapacity.UpdatedBy = _ActiveUser.Name;
            _StorageCapacity.UpdatedDate = DateTime.Now;
            _db.StorageCapacity.Update(_StorageCapacity);
            _db.SaveChanges();
            return RedirectToAction("ListStorageCapacity");
        }


        [UserAuthorization]
        [HttpGet]
        public IActionResult DeleteStorageCapacity(int? id)
        {
            StorageCapacity? _StorageCapacity = _db.StorageCapacity.Where(x => x.Id == id)
                .FirstOrDefault();

            return View(_StorageCapacity);
        }


        [UserAuthorization]
        [HttpPost]
        public IActionResult DeleteStorageCapacity(StorageCapacity storage)
        {
            StorageCapacity? _StorageCapacity = _db.StorageCapacity.Where(x => x.Id == storage.Id)
              .FirstOrDefault();

            _StorageCapacity!.Status = false;
            _StorageCapacity.Deleted = true;
            _StorageCapacity.UpdatedBy = _ActiveUser.Name;
            _StorageCapacity.UpdatedDate = DateTime.Now;
            _db.StorageCapacity.Update(_StorageCapacity);
            _db.SaveChanges();
            return RedirectToAction("ListStorageCapacity");
        }

    }
}
