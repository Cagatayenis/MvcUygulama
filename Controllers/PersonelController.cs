using PesonelMVUI.Models.EntityFramework;
using PesonelMVUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PesonelMVUI.Controllers
{
    [Authorize(Roles = "A,U")]
    public class PersonelController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();
        public ActionResult Index()
        {
            List<Personel> model = new List<Personel>();
            model = db.Personel.Include(u => u.Departman).ToList();
            return View(model);
        }
        
        public ActionResult Yeni()
        {
            PersonelFormViewModel model = new PersonelFormViewModel();
            model.Departmanlar = db.Departman.ToList();
            model.Personel = new Personel();
            return View("PersonelForm", model);
        }

        public ActionResult Kaydet(Personel personel)
        {
            if(!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel=personel

                };
                return View("PersonelForm",model);
            }
            if (personel.Id == 0) //Ekleme İşlemi
            {
                db.Personel.Add(personel);
            }
            else //Güncelleme İşlemi
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            PersonelFormViewModel guncellenecek = new PersonelFormViewModel();
            guncellenecek.Departmanlar = db.Departman.ToList();
            guncellenecek.Personel = db.Personel.Find(id);
            if (guncellenecek == null)
                return HttpNotFound();
            return View("PersonelForm", guncellenecek);

        }

        public ActionResult Sil(int id)
        {
            Personel silinecek = new Personel();
            silinecek = db.Personel.Find(id);
            if(silinecek==null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(silinecek);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}