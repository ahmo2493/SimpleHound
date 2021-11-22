using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHound.SQLDatabase
{
    public class MenuDBContext : DbContext
    {
       
        public DbSet<MenuEntry> MenuEntry { get; set; }
        public DbSet<MenuEmployees> MenuEmployees { get; set; }
        public DbSet<MenuCustomerOrder> MenuCustomerOrder { get; set; }
        public DbSet<MenuKitchenOrder> MenuKitchen { get; set; }
        public DbSet<CustomerCount> CustomerCount { get; set; }

        public MenuDBContext(DbContextOptions<MenuDBContext> options)
            : base(options)
        {
        }
    }
}
