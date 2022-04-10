using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Trip
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Name { get; set; } 

        public string Photo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }

        public int NumberOfPlaces { get; set; }
        public int Number { get; set; }

        public bool IsReserved { get; set; }

        public virtual List<Reservation> Reservations { get; set; }
    }
}