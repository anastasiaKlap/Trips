using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Title")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Text")]
        [StringLength(5000, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 5000 characters.")]
        public string Text { get; set; }
        public string Image { get; set; }
        public int ProfileId { get; set; }


        public virtual List<Comment> Comments { get; set; }
        public virtual Profil Profil { get; set; }
    }
}