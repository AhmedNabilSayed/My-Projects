using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using WebAp.Models;

namespace WebAp.Controllers
{
    public class CommentsController : ApiController
    {
        private cmodel db = new cmodel();
        public string Post()
        {
            try
            {
                int iduser = Convert.ToInt32(HttpContext.Current.Request.Form["iduser"]);
                int idpost = Convert.ToInt32(HttpContext.Current.Request.Form["idpost"]);
                string comm = HttpContext.Current.Request.Form["comment"];

                comment comment = new comment();
                comment.commenttext = comm;
                comment.user = db.Users.Find(iduser);
                comment.Post = db.Posts.Find(idpost);
                db.Comments.Add(comment);
                db.SaveChanges();
                return "good job";
            }
            catch
            {
                return "bad job";
            }
        }   
    }
}
