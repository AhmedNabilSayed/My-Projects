using ArticlProject.Core;
using ArticlProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArticlProject.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IDataHelper<AuthorPost> dataHelper;
        private readonly IDataHelper<Author> dataHelperForAuthor;
        private readonly IDataHelper<Category> dataHelperForCategory;
        private readonly IWebHostEnvironment webHost;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserStore<IdentityUser> userStore;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly Code.FilesHelper filesHelper;
        private int pageItem;
        private Task<AuthorizationResult> result;
        private string UserId;

        public PostController(
            IDataHelper<AuthorPost> dataHelper,
            IDataHelper<Author> dataHelperForAuthor,
            IDataHelper<Category> dataHelperForCategory,
            IWebHostEnvironment webHost,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager)
        {
            this.dataHelper = dataHelper;
            this.dataHelperForAuthor = dataHelperForAuthor;
            this.dataHelperForCategory = dataHelperForCategory;
            this.webHost = webHost;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.userStore = userStore;
            this.signInManager = signInManager;
            filesHelper = new Code.FilesHelper(this.webHost);
            pageItem = 10;
            

        }
        
        // GET: HomeController
        public ActionResult Index(int? id)
        {
            SetUser();
            if (result.Result.Succeeded) 
            {
                // Admin
                if (id == null || id == 0)
                {
                    return View(dataHelper.GetAllData().Take(pageItem));

                }
                else
                {
                    var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(pageItem);
                    return View(data);
                }

            }else
            {
                
                if (id == null || id == 0)
                {
                    return View(dataHelper.GetDataByUser(UserId).Take(pageItem));

                }
                else
                {
                    var data = dataHelper.GetDataByUser(UserId).Where(x => x.Id > id).Take(pageItem);
                    return View(data);
                }

            }

            // User => User id
        }

        public ActionResult Search(string SearchItem)
        {
            SetUser();
            if (result.Result.Succeeded)
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
            else
                {
                if (SearchItem == null)
                {
                    return View("Index", dataHelper.GetDataByUser(UserId));
                }
                else
                {
                    return View("Index", dataHelper.Search(SearchItem).Where(x=>x.UserId==UserId).ToList());

                }
            }
              
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            SetUser();
            return View(dataHelper.Find(id));

        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            SetUser();
            return View();

        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoreView.AuthorPostView collection)
        {
            SetUser();
            try
            {
                var Post = new AuthorPost
                {
                    AddedDate = DateTime.Now,
                    Author = collection.Author,
                    AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CategoryId = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "Images")
                };
                dataHelper.Add(Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            var authorPost = dataHelper.Find(id);
            CoreView.AuthorPostView authorPostView = new CoreView.AuthorPostView
            {
                AddedDate = authorPost.AddedDate,
                Author = authorPost.Author,
                AuthorId = authorPost.AuthorId,
                Category = authorPost.Category,
                CategoryId = authorPost.CategoryId,
                FullName = authorPost.FullName,
                PostCategory = authorPost.PostCategory,
                PostDescription = authorPost.PostDescription,
                PostTitle = authorPost.PostTitle,
                UserId = authorPost.UserId,
                UserName = authorPost.UserName,
                Id = authorPost.Id
            };
            return View(authorPostView);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CoreView.AuthorPostView collection)
        {
            SetUser();
            try
            {
                var Post = new AuthorPost
                {
                    AddedDate = DateTime.Now,
                    Author = collection.Author,
                    AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CategoryId = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "Images"),
                    Id= collection.Id
                };
                dataHelper.Edit(id,Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AuthorPost collection)
        {
            try
            {
                dataHelper.Delete(id);
                string filePath = "~/Images/" + collection.PostImageUrl;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void SetUser()
        {
            result = authorizationService.AuthorizeAsync(User, "Admin");
            //UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }
    }
}
