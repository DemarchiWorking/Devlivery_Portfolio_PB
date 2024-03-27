
namespace Devlivery.Model.Domain.Auth
{
    public class Jwt
    {
        public bool autenticado { get; set; } = false;
        public string expiracao { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
        public string mensagem { get; set; } = string.Empty;
        public string usuario { get; set; } = string.Empty;
    }
}