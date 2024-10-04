using System;
using System.ComponentModel.DataAnnotations;

namespace MVCapp.Models
{
    public class Organisator
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "De titel mag niet langer zijn dan 100 tekens.")]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EventDateTime { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Voer een geldig bedrag in.")]
        public decimal Cost { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Voer een geldig aantal deelnemers in.")]
        public int MaxParticipants { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "De beschrijving mag niet langer zijn dan 500 tekens.")]
        public string Description { get; set; }

        public string Category { get; set; }

        public string ImagePath { get; set; }
    }
}
