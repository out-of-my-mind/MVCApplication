using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class Genre
    {
        public virtual int GenreId { set; get; }
        public virtual string Name { set; get; }
        public virtual string Description { set; get; }
        public virtual List<Album> Albums { set; get; }
    }
}