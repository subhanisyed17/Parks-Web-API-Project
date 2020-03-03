using Microsoft.EntityFrameworkCore;
using ParkyAPI.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<NationalPark> NationalParks { get; set; }

        public DbSet<Trail> Trails { get; set; }
    }
}
