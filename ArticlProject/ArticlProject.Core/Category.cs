using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlProject.Core
{
    public class Category
    {
        [Required]
        [Display(Name = "المعرف")]
        public int Id { get; set; }

        [Required(ErrorMessage ="هذا الحقل مطلوب ")]
        [Display(Name = "اسم الصنف")]
        [MaxLength(50,ErrorMessage ="اعلي قيمة للادخال هي 50 حرف")]
        [MinLength(2,ErrorMessage ="ادني قيمة للادخال هي حرفان")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        // Navigtion

        public virtual List<AuthorPost>  AuthorPosts { get; set; }
    }
}
