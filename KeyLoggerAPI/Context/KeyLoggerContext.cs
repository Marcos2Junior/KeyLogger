using KeyLoggerWEB.Entitys;
using Microsoft.EntityFrameworkCore;

namespace KeyLoggerWEB.Context
{
    public class KeyLoggerContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<RegisterLog> RegisterLogs { get; set; }
        public KeyLoggerContext(DbContextOptions<KeyLoggerContext> options) : base(options)
        {
        }
    }
}
