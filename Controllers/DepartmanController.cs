using PesonelMVUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PesonelMVUI.Controllers
{
    [Authorize(Roles = "A,U")]
    public class DepartmanController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();

        [Authorize]
        public ActionResult Index()
        {
            var departmanlar = db.Departman.ToList();
            return View(departmanlar);
        }

        public ActionResult Yeni()
        {
            return View("DepartmanForm",new Departman());
        }

        [HttpPost]
        public ActionResult Kaydet(Departman Departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            if (Departman.Id == 0)
            {
                db.Departman.Add(Departman);
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(Departman.Id);
                if (guncellenecekDepartman == null)
                    return HttpNotFound();
                guncellenecekDepartman.Ad = Departman.Ad;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("DepartmanForm", model);
        }

        public ActionResult Sil(int id)
        {
            Departman sil = new Departman();
            sil = db.Departman.Find(id);
            if (sil == null)
                return HttpNotFound();
            db.Departman.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}