using eTickets.Data.Enums;
using eTickets.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Data.ViewModel
{
    public class NewMovieVM //since we are not using this file to insert data into database so we can remove id and relation definitions, compare with Model.cs to know what all properties were removed
    {
        //now including id for edit function
        public int Id { get; set; } 

        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Display(Name = "Movie Poster URL")]
        [Required(ErrorMessage = "Movie Poster Url is required")]

        public string ImageURL { get; set; }
        [Display(Name = "Movie Start Date")]
        [Required(ErrorMessage = "Movie Start Date is required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Movie end Date")]
        [Required(ErrorMessage = "Movie End Date is required")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Category is required")]
        public MovieCategory MovieCategory { get; set; } // we are creating enum and therefor we will add all enums in data folder

        //relationships
        [Display(Name = "Select actor(s) ")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int> ActorIds { get; set; } // list of int as we will get actor ids from view and then store ids in database by using relationship definition
        [Display(Name = "Select a cinema ")]
        [Required(ErrorMessage = "Movie cinemas is required")]
        public int CinemaId { get; set; }
        [Display(Name = "Select a producer ")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int ProducerId { get; set; }
    }
}

    

