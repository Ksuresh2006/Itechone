using Microsoft.EntityFrameworkCore;
using POCInventory.Model;

namespace POCInventory.DBContext
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> inventoryDbContext):base(inventoryDbContext)
        {
           
        }
        public DbSet<Inventory> inventory { get; set; }
        

    }
}
