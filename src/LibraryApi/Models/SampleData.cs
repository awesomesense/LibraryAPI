using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Models
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<AppDbContext>();

            // Sample Data for MS SQL database

            /*
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(
                    new AuthorItem { FirstName = "Lesya", LastName = "Ukrainka" },
                    new AuthorItem { FirstName = "Miguel", LastName = "Cervantes" },
                    new AuthorItem { FirstName = "Charles", LastName = "Dickens" }
                    );

                context.SaveChanges();
            }
            */

            try
            {
                //if (context.Database.EnsureCreated()) //.AsRelational().Exists())
                if (!context.Books.Any()) // если данные по книгам в бд отсутствуют, вставим авторов и книги
                {
                    var lesya = context.Authors.Add(
                        new AuthorItem { FirstName = "Lesya", LastName = "Ukrainka" }).Entity;
                    var cervantes = context.Authors.Add(
                        new AuthorItem { FirstName = "Miguel", LastName = "Cervantes" }).Entity;
                    var dickens = context.Authors.Add(
                        new AuthorItem { FirstName = "Charles", LastName = "Dickens" }).Entity;

                    context.Books.AddRange(
                        new BookItem()
                        {
                            Title = "Lisova Pisnya",
                            Year = 1911,
                            Author = lesya
                        },
                        new BookItem()
                        {
                            Title = "Don Quixote",
                            Year = 1617,
                            Author = cervantes
                        },
                        new BookItem()
                        {
                            Title = "David Copperfield",
                            Year = 1850,
                            Author = dickens
                        }
                    );
                    context.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                // TODO: handle an exception
            }
        }
    }
}
