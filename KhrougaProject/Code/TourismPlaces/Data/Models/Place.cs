
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TourismPlaces.Data.Models;

namespace TourismPlaces.Data
{
    public class Place
    {

        [Key]
        public int Id { get; set; }
        [Required]
    
        public string Name { get; set; }
        public String Address { get; set; }
        public decimal EntrancePrice { get; set; }
        public DateTime DateOfCreation { get; set; }
        public decimal rate { get; set; }
        public string Details { get; set; }

       
        [ForeignKey("Government")]
        public int GovernmentId  { get; set; }
        public  Government Government { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsApproved { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
