using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class MVCMusicStoreDB : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MVCMusicStoreDB() : base("name=MVCMusicStoreDB")//name指定数据名称
        {
        }

        public System.Data.Entity.DbSet<MVCApplication.Models.Album> Albums { get; set; }

        public System.Data.Entity.DbSet<MVCApplication.Models.Artist> Artists { get; set; }

        public System.Data.Entity.DbSet<MVCApplication.Models.Genre> Genres { get; set; }

        public System.Data.Entity.DbSet<MVCApplication.Models.MVCMovie> Movies { set; get; }
    }
}
