using eTickets.Data;
using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Display(Name = "Movie Name")]
        [Required(ErrorMessage ="Movie Name Is Required")]
        public string Name { get; set; }

        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Movie Description Is Required")]
        public string Description { get; set; }

        [Display(Name = "Price In $")]
        [Required(ErrorMessage = "Price In $ Is Required")]
        public double Price { get; set; }

        [Display(Name = "Poster URL")]
        [Required(ErrorMessage = "Poster URL Is Required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Movie Start Date")]
        [Required(ErrorMessage = "Movie Start Date Is Required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie End Date")]
        [Required(ErrorMessage = "Movie End Date Is Required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select A Category")]
        [Required(ErrorMessage = "Movie Category Is Required")]

        public MovieCategory MovieCategory { get; set; }

        [Display(Name = "Select Actor(s)")]
        [Required(ErrorMessage = "Movie Actor(s) Is Required")]

        public List<int> ActorIds { get; set; }

        [Display(Name = "Select A Cinema")]
        [Required(ErrorMessage = "Movie Cinema Is Required")]

        public int CinemaId { get; set; }

        [Display(Name = "Select A Producer")]
        [Required(ErrorMessage = "Producer Name Is Required")]

        public int ProducerId { get; set; }
    } 
}
