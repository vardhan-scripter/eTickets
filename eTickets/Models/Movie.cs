using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be in between 3 and 50 chars")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Description must be in between 3 and 100 chars")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required.")]
        public double Price { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required.")]
        public string ImageURL { get; set; }

        [Display(Name = "StartDate")]
        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "EndDate")]
        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime EndDate { get; set; }

        [Display(Name = "MovieCategory")]
        [Required(ErrorMessage = "MovieCategory is required.")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        [Display(Name = "Actors")]
        [Required(ErrorMessage = "Atleast one Actor is required.")]
        public List<Actor_Movie> Actors_Movies { get; set; }
        // Cinema
        public int CinemaId { get; set; }

        [ForeignKey("CinemaId")]
        [Display(Name = "Cinema")]
        [Required(ErrorMessage = "Cinema is required.")]
        public Cinema Cinema { get; set; }
        public int ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        [Display(Name = "Producer")]
        [Required(ErrorMessage = "Producer is required.")]
        public Producer Producer { get; set; }
    }
}
