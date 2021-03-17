using KeyLoggerAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyLoggerAPI.Controllers
{
    [ApiController]
    public class KeyLoggerController : Controller
    {
        private readonly IKeyLoggerRepository _repository;

        public KeyLoggerController(IKeyLoggerRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(string data)
        {
            return Ok();
        }
    }
}
