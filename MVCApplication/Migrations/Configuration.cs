//using MVCApplication.Models;

//namespace MVCApplication.Migrations
//{
//    using System;
//    using System.Data.Entity;
//    using System.Data.Entity.Migrations;
//    using System.Linq;

//    internal sealed class Configuration : DbMigrationsConfiguration<MVCApplication.Models.MVCMusicStoreDB>
//    {
//        public Configuration()
//        {
//            AutomaticMigrationsEnabled = true;
//        }

//        protected override void Seed(MVCApplication.Models.MVCMusicStoreDB context)
//        {
//            //  This method will be called after migrating to the latest version.

//            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//            //  to avoid creating duplicate seed data. E.g.
//            //
//            context.Movies.AddOrUpdate(
//              p => p.Title,//∑«÷ÿ∏¥≈–∂œ
//              new MVCMovie {
//                  Title = "When Harry Met Sally",
//                  ReleaseDate = DateTime.Parse("1989-1-11"),
//                  Genre = "Romantic Comedy",
//                  Rating = "PG",
//                  Price = 7.99
//              },
//              new MVCMovie {
//                  Title = "Ghostbusters",
//                  ReleaseDate = DateTime.Parse("1999-1-11"),
//                  Genre = "Comedy",
//                  Rating = "PG",
//                  Price = 8.99
//              },
//              new MVCMovie {
//                  Title = "Ghostbusters 2",
//                  ReleaseDate = DateTime.Parse("1999-6-11"),
//                  Genre = "Comedy",
//                  Rating = "PG",
//                  Price = 9.99
//              }
//            );
//            //
//        }
//    }
//}
