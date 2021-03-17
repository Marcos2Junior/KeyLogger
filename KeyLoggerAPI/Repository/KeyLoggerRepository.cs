using KeyLoggerAPI.Context;
using KeyLoggerAPI.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyLoggerAPI.Repository
{
    public class KeyLoggerRepository : IKeyLoggerRepository
    {
        protected readonly KeyLoggerContext _context;

        public KeyLoggerRepository(KeyLoggerContext context) 
        {
            _context = context;
        }

        public async Task<bool> AddLogAsync(string origin)
        {
            await _context.AddAsync(new Log
            {
                DateTime = DateTime.UtcNow,
                Origin = origin
            });

            return await _context.SaveChangesAsync() > 0;
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
    }
}
