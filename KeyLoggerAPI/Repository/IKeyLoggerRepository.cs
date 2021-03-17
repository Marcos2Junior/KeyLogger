using KeyLoggerAPI.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeyLoggerAPI.Repository
{
    public interface IKeyLoggerRepository
    {
        Task<Log> AddLogAsync(string origin);
        Task<Log> GetLogByOriginAsync(string origin);
        Task<bool> UpdateLogAsync(Log log);
        Task<List<Log>> GetAllAsync();
    }
}
