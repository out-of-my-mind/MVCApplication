using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class Artist
    {
        public virtual int ArtistId { set; get; }
        public virtual string Name { set; get; }
    }
}