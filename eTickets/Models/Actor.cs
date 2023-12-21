using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor : IEntityBase //we can remove Id if we want to or else it will be overriden
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Profile Picture")] //you want to display in cshtml file
       [Required(ErrorMessage ="Profile Picture is required")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength =3, ErrorMessage ="Full Name must be between 3 and 50 chars")]

        public string FullName { get; set; }
        [Display(Name = "Biography")]
       [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }

        //relationship
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
