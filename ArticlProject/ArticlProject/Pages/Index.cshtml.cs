using ArticlProject.Core;
using ArticlProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArticlProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataHelper<Core.Category> dataHelperForCategory;
        private readonly IDataHelper<AuthorPost> dataHelperForPost;
        public readonly int NoOfItem;



        public IndexModel(
            ILogger<IndexModel> logger,
            IDataHelper<Core.Category> dataHelperForCategory,
            IDataHelper<Core.AuthorPost> dataHelperForPost
            )
        {
            _logger = logger;
            this.dataHelperForCategory = dataHelperForCategory;
            this.dataHelperForPost = dataHelperForPost;
            NoOfItem = 6;

            ListOfCategory = new List<Core.Category>();
                
            ListOfPost = new List<AuthorPost>();
            
        }

        public int FirstPost { get; set; }
        public int LastPost { get; set; }
        public List<Core.Category> ListOfCategory { get; set; }
        public List<Core.AuthorPost> ListOfPost { get; set; }

        
        public void OnGet(string LoadState, string CategoryName, string search,int id)
        {
            GetAllCategory();
            if (dataHelperForPost.GetAllData().Count() > 0)
            {
                FirstPost = dataHelperForPost.GetAllData().First().Id;
                LastPost = dataHelperForPost.GetAllData().Last().Id;
            }
            //FirstPost= dataHelperForPost.GetAllData().First().Id;
            //LastPost= dataHelperForPost.GetAllData().Last().Id;

            if (LoadState == null || LoadState == "All")
            {
                GetAllPost();
            }

            else if (LoadState == "ByCategory")
            {
                GetDataByCategoryName(CategoryName);
            }
            else if(LoadState=="Search")
            {
                SearchData(search);   
            }
            else if(LoadState=="Next")
            {
                GetNextData(id);
            }

            else if (LoadState == "Prev")
            {
                GetNextData(id-NoOfItem-1);
            }
        }


        // Get Category

        private void GetAllCategory()
        {
            ListOfCategory = dataHelperForCategory.GetAllData();
        }

        // Get All Post

        private void  GetAllPost()
        {
            ListOfPost = dataHelperForPost.GetAllData().Take(NoOfItem).ToList();
        }

        private void GetDataByCategoryName(string CategoryName)
        {
            ListOfPost = dataHelperForPost.GetAllData().Where(x=>x.PostCategory == CategoryName).Take(NoOfItem).ToList();
        }

        private void SearchData(string SearchItem)
        {
            ListOfPost = dataHelperForPost.Search(SearchItem);
        }

        private void GetNextData(int id)
        {
            ListOfPost = dataHelperForPost.GetAllData().Where(x => x.Id > id).Take(NoOfItem).ToList();
        } 
       
    }
}