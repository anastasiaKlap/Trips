using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Profil
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30, ErrorMessage = "Login cannot be longer than 30 characters.")]
        public string Login { get; set; }

        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
        public String FirstName { get; set; }

        [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
        public String LastName { get; set; }

        [StringLength(9, ErrorMessage = "Phone number cannot be longer than 9 characters.")]
        public String PhoneNumber { get; set; }
        public String Address { get; set; }

        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
    }
}