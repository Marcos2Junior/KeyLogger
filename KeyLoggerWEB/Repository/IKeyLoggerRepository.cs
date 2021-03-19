using KeyLoggerWEB.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeyLoggerWEB.Repository
{
    public interface IKeyLoggerRepository
    {
        Task<Log> AddLogAsync(string origin);
        Task<Log> GetLogByOriginAsync(string origin);
        Task<bool> UpdateLogAsync(Log log);
        Task<List<Log>> GetAllAsync();
    }
}
