using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Reservation
    {
        [Key]
        public int ID { get; set; } 
        public int ProfilId { get; set; }
        public int TripId { get; set; }

        public virtual Profil Profil { get; set; }
        public virtual Trip Trip { get; set; }

    }
}