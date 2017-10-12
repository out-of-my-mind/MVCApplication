using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class MusicStoreDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<MVCApplication.Models.MVCMusicStoreDB>
    {
        protected override void Seed(MVCMusicStoreDB context)
        {
            context.Artists.Add(new Artist { Name="A1 D1 meola"});
            context.Genres.Add(new Genre { Name = "Rock"});
            context.Albums.Add(new Album {
                Artist = new Artist { Name="Rush"},
                Genre = new Genre { Name="Rock"},
                Price = 9.9m,
                Title = "Caravan"
            });
            base.Seed(context);
        }
    }
}