using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismPlaces.Data.Models
{
    
    public class Government
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public string Name { get; set; }
        public ICollection<Place> Places{ get; set; }

        public ICollection<ApplicationUser> ApplicationUsers{ get; set; }
    }
}
