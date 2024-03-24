using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAp.Models;

namespace WebAp.Controllers
{
    public class reactController : ApiController
    {
        private cmodel db = new cmodel();
        public string post()
        {
            int iduser = Convert.ToInt32(HttpContext.Current.Request.Form["iduser"]);
            int idpost = Convert.ToInt32(HttpContext.Current.Request.Form["idpost"]);
            int rea = Convert.ToInt32(HttpContext.Current.Request.Form["react"]);




            db.reacts.RemoveRange(db.reacts.Where(m => m.idpost == idpost && m.iduser == iduser).ToList());
            db.SaveChanges();

            react react = new react();

            react.iduser = iduser;
            react.user = db.Users.Find(iduser);

            react.idpost = idpost;

            react.post = db.Posts.Find(idpost);
            react.rea = rea;
            db.reacts.Add(react);
            db.SaveChanges();
            
            return "";
        } 
    }
}
