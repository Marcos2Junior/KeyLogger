using System;
using System.Collections.Generic;

namespace KeyLoggerWEB.Entitys
{
    public class Log
    {
        /// <summary>
        /// chave primaria
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Data de cadastro
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Origem
        /// </summary>
        public string Origin { get; set; }
        /// <summary>
        /// registros
        /// </summary>
        public List<RegisterLog> RegisterLogs { get; set; }
    }
}
