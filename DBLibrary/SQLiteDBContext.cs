using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class SQLiteDBContext : DbContext
    {
        public SQLiteDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseSqlite("Data Source=SQLite.db");
            options.UseSqlite($"Data Source=SQLiteDB.db", b => b.MigrationsAssembly("TimeSheets"));
        }
    }
}
