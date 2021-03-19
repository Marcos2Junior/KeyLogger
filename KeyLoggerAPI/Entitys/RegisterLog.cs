using System;

namespace KeyLoggerWEB.Entitys
{
    public class RegisterLog
    {
        /// <summary>
        /// chave primaria
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// chave estrangeira de log
        /// </summary>
        public int LogID { get; set; }
        public Log Log { get; set; }
        /// <summary>
        /// registro do log
        /// </summary>
        public string Register { get; set; }
        /// <summary>
        /// data de cadastro
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}