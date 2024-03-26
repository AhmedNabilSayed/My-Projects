using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismPlaces.Data.Models
{
    public class ApplicationUser :IdentityUser
    {
        [ForeignKey("Government")]
        public int GovernmentId { get; set; }
        public Government Government { get; set; }

        public ICollection<Place>? Places { get; set; }
    }
}
