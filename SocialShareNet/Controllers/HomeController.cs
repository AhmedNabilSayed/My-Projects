using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAp.Models;

namespace WebAp.Controllers
{
    public class HomeController : Controller
    {
        private cmodel db = new cmodel();
        
        public ActionResult Index()
        {
            Session["userid"] = "0";
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string email = form["email"].ToString();
            string password = form["pass"].ToString();

           user user=db.Users.Where(m => m.email == email && password == password).First();
           if (user == null)
            {
                ViewBag.Message = " your pass not correct";
                return View();
            }

            Session["userid"] = user.id.ToString();

            return RedirectToAction("Index","Profile");
        }

        public ActionResult signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signin(FormCollection form , HttpPostedFileBase photo)
        {

            if(form["pass"].ToString() != form["repass"].ToString())
            {
                ViewBag.mss = "your pass not mach";
                return View();
            }
            HttpPostedFileBase postedFile = Request.Files["photo"];
            string path = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
            user user = new user();
            user.fname = form["fname"].ToString();
            user.lname = form["lname"].ToString();
            user.username = form["username"].ToString();
            user.password = form["pass"].ToString();
            user.email = form["email"].ToString();
            user.phono = form["phone"].ToString();
            user.photo = "/Uploads/" + Path.GetFileName(postedFile.FileName);
            db.Users.Add(user);
            db.SaveChanges();
            ViewBag.mss = "your accunt is created";
            return RedirectToAction("Index");
        }

    }
}