using ArticlProject.Core;
using ArticlProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticlProject.Controllers
{
    [Authorize("Admin")]
    public class CategoryController : Controller
    {
        private readonly IDataHelper<Category> dataHelper;
        private int pageItem;
        public CategoryController(IDataHelper<Category> dataHelper)
        {
            this.dataHelper = dataHelper;
            pageItem = 10;
        }
        public ActionResult Index(int?id)
        {
            if(id== null || id==0)
            {
                return View(dataHelper.GetAllData().Take(pageItem));

            }
            else
            {
                var data = dataHelper.GetAllData().Where(x => x.Id>id).Take(pageItem);
                return View(data);
            }
        } 
        public ActionResult Search(string SearchItem)
        {
            if (SearchItem == null)
            {
                return View("Index", dataHelper.GetAllData());
            }
            else
            {
                return View("Index", dataHelper.Search(SearchItem));

            }
        }

        
      
        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category collection)
        {
            try
            {
                var result = dataHelper.Add(collection);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category collection)
        {
            try
            {
                var result = dataHelper.Edit(id,collection);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category collection)
        {
            try
            {
                var result = dataHelper.Delete(id);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
