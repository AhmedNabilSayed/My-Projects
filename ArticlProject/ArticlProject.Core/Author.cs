using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlProject.Core
{
    public class Author
    {
        [Required]
        [Display(Name ="المعرف")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "معرف المستخدم")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "الأسم الكامل")]
        public string FullName { get; set; }
        [Display(Name = "الصورة")]
        public string? ProfileImageUrl { get; set; }
        [Display(Name = "السيرة الذاتية")]
        public string? Bio { get; set; }
        [Display(Name = "فيسبوك")]
        public string? Facebook { get; set; }

        [Display(Name = "انستجرام")]
        public string? Instagram { get; set; }

        [Display(Name = "تويتر")]
        public string ?Twitter { get; set; }

        // Navigtion
        public virtual List<AuthorPost> AuthorPosts { get; set; }



    }
}
