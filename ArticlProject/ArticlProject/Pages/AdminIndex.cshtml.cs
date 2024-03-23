using ArticlProject.Core;
using ArticlProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ArticlProject.Pages
{
    [Authorize]
    public class AdminIndexModel : PageModel
    {
        private readonly IDataHelper<AuthorPost> dataHelper;
        private readonly IDataHelper<Core.Author> dataHelperForAuthors;

        public AdminIndexModel(
         IDataHelper<AuthorPost> dataHelper,
         IDataHelper<ArticlProject.Core.Author> dataHelperForAuthors)
        {
            this.dataHelper = dataHelper;
            this.dataHelperForAuthors = dataHelperForAuthors;
        }

        public int AllPost { get; set; }
        public int PostLastMonth { get; set; }
        public string UserName { get; set; }
        public int PostThisYear { get; set; }
        public void OnGet()
        {

            var datem = DateTime.Now.AddMonths(-1);
            var datey = DateTime.Now.AddYears(-1);
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AllPost = dataHelper.GetDataByUser(userid).Count;
            PostLastMonth = dataHelper.GetDataByUser(userid).Where(x=>x.AddedDate>=datem).Count();
            UserName = dataHelperForAuthors.GetDataByUser(userid).First().FullName;
        }
    }
}
