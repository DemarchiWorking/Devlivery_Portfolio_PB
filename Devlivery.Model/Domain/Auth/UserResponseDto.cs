﻿namespace Devlivery.Model.Domain.Auth
{
    public class UserResponseDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmado { get; set; }
    }
}
