﻿namespace Devlivery.Model.Domain.Auth
{
    public class UsuarioToken
    {
        public bool Autenticado { get; set; }
        public DateTime Expiracao { get; set; }
        public string Token { get; set; }
        public string Mensagem { get; set; }
        public string Usuario { get; set; }
    }
}