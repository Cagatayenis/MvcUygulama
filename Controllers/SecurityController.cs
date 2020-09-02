using PesonelMVUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PesonelMVUI.Controllers
{
    
    public class SecurityController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            Kullanici kulindb = new Kullanici();
            kulindb= db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre==kullanici.Sifre);
            if(kulindb!=null)
            {
                FormsAuthentication.SetAuthCookie(kulindb.Ad, false);
                return RedirectToAction("MakaleListe", "Departman");
            }
            else
            {
                ViewBag.Mesaj = "Gecersiz Kullanici Adi Veya Şifre";
                return View();
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}