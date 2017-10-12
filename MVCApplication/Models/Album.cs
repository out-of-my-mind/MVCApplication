using MVCApplication.Infrastructrue;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplication.Models
{
    public class Album : IValidatableObject
    {
        public virtual int AlbumId { set; get; }
        public virtual int GenreId { set; get; }
        public virtual int ArtistId { set; get; }

        [Required]
        [StringLength(10)]
        [MaxWords(10,ErrorMessage ="There are too many words")]
        public virtual string Title { set; get; }
        public virtual decimal Price { set; get; }
        public virtual string AlbumArtUrl { set; get; }
        public virtual Genre Genre { set; get; }
        public virtual Artist Artist{ set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title != null && Title.Split(' ').Length > 10)
            {
                yield return new ValidationResult("Too many words", new[] { "LastName" });
            }
        }
    }
}