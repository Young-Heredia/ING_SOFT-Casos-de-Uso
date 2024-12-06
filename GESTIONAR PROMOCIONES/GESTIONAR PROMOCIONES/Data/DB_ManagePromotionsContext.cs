using GESTIONAR_PROMOCIONES.Models;
using Microsoft.EntityFrameworkCore;

namespace GESTIONAR_PROMOCIONES.Data
{
    public class DB_ManagePromotionsContext:DbContext
    {
        public DB_ManagePromotionsContext(DbContextOptions<DB_ManagePromotionsContext> options) : base(options) { }



        public DbSet<Promotion> Promions { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
