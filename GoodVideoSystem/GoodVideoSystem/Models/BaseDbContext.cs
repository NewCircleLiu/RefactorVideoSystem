using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
            : base("name=VIdeoSystem")
        {

        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Suggest> Suggests { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    }
}