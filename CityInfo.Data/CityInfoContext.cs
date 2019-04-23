﻿using CityInfo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace CityInfo.Data
{
  public class CityInfoContext : DbContext
  {
    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
    {
      Database.Migrate();
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<PointOfInterest> PointsOfInterest { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //  optionsBuilder.UseSqlServer("connectionString");

    //  base.OnConfiguring(optionsBuilder);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      /* Seeding the database with some starting data
       * 
       * Based on this StackOverflow post:
       * https://stackoverflow.com/questions/45148389/how-to-seed-in-entity-framework-core-2
       * Check answer from Blake Mumford
       * 
       */

      modelBuilder.Entity<City>().HasData(
        new City()
        {
          Id = 1,
          Name = "New York City",
          Description = "Big Apple",
        },
        new City()
        {
          Id = 2,
          Name = "Antwerp",
          Description = "Has a cathedral that was never finished",
        },
        new City()
        {
          Id = 3,
          Name = "Paris",
          Description = "Avecs",
        }
      );

      modelBuilder.Entity<PointOfInterest>().HasData(
        new PointOfInterest()
        {
          Id = 1,
          CityId = 1,
          Name = "Broadway",
          Description = "Phantom of the Opera is recommended",
        },
        new PointOfInterest()
        {
          Id = 2,
          CityId = 1,
          Name = "Times Square",
          Description = "Crossroads of the City",
        },
        new PointOfInterest()
        {
          Id = 3,
          CityId = 1,
          Name = "Central Park",
          Description = "Some greenery in the contrete jungle",
        },
        new PointOfInterest()
        {
          Id = 4,
          CityId = 2,
          Name = "Cathedral of Our Lady",
          Description = "Was supposed to have two towers, not just one",
        },
        new PointOfInterest()
        {
          Id = 5,
          CityId = 3,
          Name = "Eiffel Tower",
          Description = "Was originaly only supposed to be a temporary building",
        },
        new PointOfInterest()
        {
          Id = 6,
          CityId = 3,
          Name = "Notre Dame",
          Description = "How is God going to protect you, if he can't even protect his own house",
        }
      );
    }
  }
}
