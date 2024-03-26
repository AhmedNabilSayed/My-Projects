using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAp.Models;

namespace WebAp.Controllers
{
    public class ProfileController : Controller
    {
        private cmodel db = new cmodel();
        // GET: Profile
        public ActionResult Index()
        {
            user user = db.Users.Find(Convert.ToInt32(Session["userid"]));
            return View(user);
        }
        public ActionResult addpost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addpost(FormCollection form)
        {
            post post = new post();
            
            post.posttext = form["posttext"].ToString();
            post.privet = Convert.ToInt32(form["pr"]);

            post.user = db.Users.Find(Convert.ToInt32(Session["userid"]));
            
            db.Posts.Add(post);
            db.SaveChanges();
            return View();
        }





        public ActionResult Alluser()
        {
            int x = Convert.ToInt32(Session["userid"]);
            List<user> users = db.Users.Where(m => m.id != x).ToList();
            List<fr_re> fr_Res = db.fr_Res.Where(m => m.sendid == x).ToList();
            List<friend> friends = db.friends.Where(m => m.fe1 == x).ToList();
            foreach(var item in friends)
            {
                users.Remove(item.friendtow);
            }

            foreach (var item in fr_Res)
            {
                users.Remove(item.resver);
            }
            return View(users);
        }







        public ActionResult addfriend(int? id)
        {
            int x =Convert.ToInt32(Session["userid"]);
            int idi = Convert.ToInt32(id);

            try
            {
                List<fr_re> fr_Res =
                    db.fr_Res.Where(
                        m => m.sendid == x && m.resid == idi)
                    .ToList();
                db.fr_Res.RemoveRange(fr_Res);
                db.SaveChanges();
            }
            catch
            {

            }

            fr_re fr_Re = new fr_re();

            fr_Re.sendid = x;
            fr_Re.sender = db.Users.Find(x);

            fr_Re.resid = idi;
            fr_Re.resver = db.Users.Find(idi);

            db.fr_Res.Add(fr_Re);
            db.SaveChanges();

            return RedirectToAction("Alluser");
        }
        public ActionResult my_req()
        {
            int x = Convert.ToInt32(Session["userid"]);
            List<fr_re> fr_Res 
                = db.fr_Res.Where(m => m.sendid == x).ToList();
            return View(fr_Res);
        }

        public ActionResult cancal(int? id)
        {
            int x = Convert.ToInt32(Session["userid"]);
            int idi = Convert.ToInt32(id);

            List<fr_re> fr_Res =
                    db.fr_Res.Where(m => m.sendid == x && m.resid == idi)
                    .ToList();

            db.fr_Res.RemoveRange(fr_Res);
            db.SaveChanges();
            
            
            
            return RedirectToAction("my_req");
        }



        public ActionResult friendrequst()
        {
            int x = Convert.ToInt32(Session["userid"]);
            
            List<fr_re> fr_Res 
                = db.fr_Res.Where(m => m.resid == x).ToList();
            return View(fr_Res);
        }




        public ActionResult accapt(int? id)
        {

            int x = Convert.ToInt32(Session["userid"]);
            int idi = Convert.ToInt32(id);

            try
            {
                db.friends.RemoveRange(
                    db.friends.Where(m => m.fe1 == x && m.fr2 == idi).ToList());
                db.SaveChanges();

                db.friends.RemoveRange(db.friends.Where(m => m.fe1 == idi && m.fr2 == x).ToList());
                db.SaveChanges();
            }
            catch{
            }

            friend friend = new friend();
            
            friend.fe1 = x;
            friend.friendone = db.Users.Find(x);

            friend.fr2 = idi;
            friend.friendtow = db.Users.Find(idi);

            db.friends.Add(friend);
            
            db.SaveChanges();

            friend.fr2 = x;
            friend.friendtow = db.Users.Find(x);

            friend.fe1 = idi;
            friend.friendone = db.Users.Find(idi);
            
            db.friends.Add(friend);

            db.SaveChanges();

           db.fr_Res.RemoveRange(db.fr_Res.Where(m => m.resid == x && m.sendid == idi).ToList());
           db.SaveChanges();
           return RedirectToAction("friendrequst");
        }

        public ActionResult reject(int? id)
        {
            int x = Convert.ToInt32(Session["userid"]);
            int idi = Convert.ToInt32(id);
            db.fr_Res.RemoveRange(db.fr_Res.Where(m => m.resid == x && m.sendid == idi).ToList());
            db.SaveChanges();
            return RedirectToAction("friendrequst");
        }
        public ActionResult allfriend()
        {
            int x = Convert.ToInt32(Session["userid"]);
            
            return View(db.friends.Where(m => m.fe1 == x).ToList());
        }
        [HttpPost]
        public ActionResult searchname(FormCollection form)
        {
            string name = form["sname"].ToString();
            int x = Convert.ToInt32(Session["userid"]);
            List<user> users= db.Users.Where(m => m.fname == name || m.lname == name).ToList();
            List<fr_re> fr_Res = db.fr_Res.Where(m => m.sendid == x).ToList();
            List<friend> friends = db.friends.Where(m => m.fe1 == x).ToList();
            foreach (var item in friends)
            {
                users.Remove(item.friendtow);
            }

            foreach (var item in fr_Res)
            {
                users.Remove(item.resver);
            }

            return View(users);
        }

        [HttpPost]
        public ActionResult searchemail(FormCollection form)
        {
            int x = Convert.ToInt32(Session["userid"]);
            string name = form["sname"].ToString();
            List<user> users = db.Users.Where(m => m.email == name).ToList();
            
            List<fr_re> fr_Res = db.fr_Res.Where(m => m.sendid == x).ToList();
            List<friend> friends = db.friends.Where(m => m.fe1 == x).ToList();
            foreach (var item in friends)
            {
                users.Remove(item.friendtow);
            }

            foreach (var item in fr_Res)
            {
                users.Remove(item.resver);
            }

            return View(users);
        }

        [HttpPost]
        public ActionResult searchphone(FormCollection form)
        {
            int x = Convert.ToInt32(Session["userid"]);
            string name = form["sname"].ToString();
            List<user> users = db.Users.Where(m => m.phono == name).ToList();

            List<fr_re> fr_Res = db.fr_Res.Where(m => m.sendid == x).ToList();
            List<friend> friends = db.friends.Where(m => m.fe1 == x).ToList();
            foreach (var item in friends)
            {
                users.Remove(item.friendtow);
            }

            foreach (var item in fr_Res)
            {
                users.Remove(item.resver);
            }

            return View(users);
        }


        public ActionResult logout()
        {
            return RedirectToAction("index","home");
        }

    }
}