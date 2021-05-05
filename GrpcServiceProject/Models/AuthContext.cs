using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProject.Models
{
    public class AuthContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public AuthContext(DbContextOptions<AuthContext> opt):base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Auth");
        }
    }

    public class User
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string passwd { get; set; }
    }
}
