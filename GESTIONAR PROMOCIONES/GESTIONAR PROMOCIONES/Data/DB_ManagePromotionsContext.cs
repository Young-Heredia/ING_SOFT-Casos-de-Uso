using Microsoft.EntityFrameworkCore;

namespace GESTIONAR_PROMOCIONES.Data
{
    public class DB_ManagePromotionsContext:DbContext
    {
        public DB_ManagePromotionsContext(DbContextOptions<DB_ManagePromotionsContext> options) : base(options) { }
    }
}
