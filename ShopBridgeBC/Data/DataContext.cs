using Microsoft.EntityFrameworkCore;
using ShopBridgeBC.Model;
using System.ComponentModel.DataAnnotations;

namespace ShopBridgeBC.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Inventory> InventoryItems { get; set; }

        public DbSet<User> Users { get; set; }
    }    
}
