using ArticlProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlProject.Data.SqlServerEF
{
    public class AuthorEntity : IDataHelper<Author>
    {
        private DBContext db;
        private Author _table;
        public AuthorEntity()
        {
            db = new DBContext();
        }
        public int Add(Author table)
        {
            if (db.Database.CanConnect())
            {
                db.Author.Add(table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int Id)
        {
            if (db.Database.CanConnect())
            {
                _table = Find(Id);
                db.Author.Remove(_table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Edit(int Id, Author table)
        {
            db = new DBContext();
            if (db.Database.CanConnect())
            {
                db.Author.Update(table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public Author Find(int Id)
        {
            if (db.Database.CanConnect())
            {
                return db.Author.Where(x => x.Id == Id).First();
            }
            else
            {
                return null;
            }
        }

        public List<Author> GetAllData()
        {
            if (db.Database.CanConnect())
            {
                return db.Author.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Author> GetDataByUser(string UserId)
        {
            if (db.Database.CanConnect())
                return db.Author.Where(a => a.UserId == UserId).ToList();
            else
                return null;
        }

        public List<Author> Search(string SearchItem)
        {
            if (db.Database.CanConnect())
            {
                return db.Author.Where(
                    x => x.FullName.Contains(SearchItem)
                     ||  x.UserId.Contains(SearchItem)
                     ||  x.Bio.Contains(SearchItem)
                     ||  x.UserName.Contains(SearchItem)
                     ||  x.Facebook.Contains(SearchItem)
                     ||  x.Twitter.Contains(SearchItem)
                     ||  x.Instagram.Contains(SearchItem)
                     || x.Id.ToString().Contains(SearchItem))
                .ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
