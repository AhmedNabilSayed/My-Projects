using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAp.Models
{
    public class cmodel:DbContext
    {
        public cmodel() : base("fase11")
        {
        }

        public DbSet<user> Users { get; set; }
        public DbSet<post> Posts { get; set; }
        public DbSet<comment> Comments { get; set; }
        public DbSet<fr_re> fr_Res {get; set;}
        public DbSet<friend> friends { get; set; }
        public DbSet<react> reacts { get; set; }

    }


    public class user
    {
        public int id { get; set; }
        public string fname { get; set; }
        public string photo { get; set;}
        public string city {get; set;}
        public string countey {get; set;}
        public string email { get; set; }
        public string phono { get; set; }
        public string lname { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public virtual List<post> posts { get; set; }
        public virtual List <comment> Comments { get; set; }
    }

    public class post
    {
        public int id { get; set; }
        public string posttext { get; set; }
        public int  privet {get; set;}


        public virtual user user { get; set; }
        public virtual List<comment> Comments { get; set; }
        public virtual List<react> Reacts { get; set; }
    }

    public class comment
    {
        public int id { get; set; }
        public string commenttext { get; set; }
        public virtual user user { get; set; }
        public virtual post Post { get; set; }

    }
    public class fr_re
    {
        public int id { get; set; }

        public int sendid { get; set; }
        public virtual user sender {get; set;}

        public int resid { get; set; }
        public virtual user resver {get; set;}
    }


    public class friend
    {
        public int id { get; set; }

        public int fe1 { get; set; }
        public virtual user friendone { get; set;}
        public int fr2 { get; set; }
        public virtual user friendtow { get; set;}
    }

    public class react
    {
        public int id { get; set; }
        public int idpost { get; set; }
        public int iduser { get; set; }
        public int rea { get; set; }
        public user user { get; set; }
        public post post { get; set; }
    }
}