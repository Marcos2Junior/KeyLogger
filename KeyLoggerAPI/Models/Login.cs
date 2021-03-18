using System.ComponentModel.DataAnnotations;

namespace KeyLoggerAPI.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Insira a sua senha")]
        public string Password { get; set; }
        public string Message { get; set; }
    }
}
