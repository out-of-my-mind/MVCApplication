using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MVCApplication.Models
{
    public class MVCMovie
    {
        public int ID { set; get; }

        [StringLength(60, MinimumLength = 3)]
        public string Title { set; get; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime ReleaseDate { set; get; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { set; get; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public double Price { set; get; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(5)]
        public string Rating { get; set; }
    }

}