using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using CrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApi
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { 
            
        }
        public DbSet<Quote> Quotes { get; set; }
    }
}
