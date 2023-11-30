using Microsoft.EntityFrameworkCore;
using ZealandIdApi.Models;

namespace ZealandIdApi.EDbContext
{
    public class ZealandIdDbContext : DbContext
    {
        public ZealandIdDbContext(DbContextOptions<ZealandIdDbContext> options) : base(options)
        {

        }

        public DbSet<Sensor> Sensorer { get; set; }
    }
}
