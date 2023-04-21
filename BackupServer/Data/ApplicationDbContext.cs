using Microsoft.EntityFrameworkCore;
using BackupServer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCopy> ItemsCopies { get; set; }
        //public DbSet<Item> DirectoryItems { get; set; }
        //public DbSet<ItemCopy> DirectoryItemsCopies { get; set; }


    }

}
