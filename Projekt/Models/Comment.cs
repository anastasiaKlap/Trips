using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Text")]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 1000 characters.")]
        public string Text { get; set; }
        public int PostID { get; set; }
        public int ProfilID { get; set; }
        public virtual Post Post { get; set; }
        public virtual Profil Profil { get; set; }


    }
}