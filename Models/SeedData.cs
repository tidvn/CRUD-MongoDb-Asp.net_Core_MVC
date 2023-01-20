using Microsoft.Extensions.DependencyInjection;
using Dasp5.Services;
using System;
using System.Linq;

namespace Dasp5.Models
{
    public static class SeedData
    {
        public static List<Movie> mv()
        {
            List<Movie> lst = new List<Movie>();
            lst.Add(
            new Movie
            {
                Title = "When Harry Met Sally",
                ReleaseDate = DateTime.Parse("1989-2-12"),
                Genre = "Romantic Comedy",
                Price = 7.99M,
                Rating = "R"
            });
            lst.Add(new Movie
            {
                Title = "Ghostbusters ",
                ReleaseDate = DateTime.Parse("1984-3-13"),
                Genre = "Comedy",
                Price = 8.99M,
                Rating = "PG"
            });

            lst.Add(new Movie
            {
                Title = "Ghostbusters 2",
                ReleaseDate = DateTime.Parse("1986-2-23"),
                Genre = "Comedy",
                Price = 9.99M,
                Rating = "PG"
            });

            lst.Add(new Movie 
            {
                Title = "Rio Bravo",
                ReleaseDate = DateTime.Parse("1959-4-15"),
                Genre = "Western",
                Price = 3.99M,
                Rating = "G"
            });

            return lst;
        }
    }
}