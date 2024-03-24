using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAp.Models;

namespace WebAp.Controllers
{
    public class friendController : Controller
    {
        private cmodel db = new cmodel();

        public ActionResult profile(int? id)
        {
            int idi = Convert.ToInt32(id);
            return View(db.Users.Find(idi));
        }
    }
}