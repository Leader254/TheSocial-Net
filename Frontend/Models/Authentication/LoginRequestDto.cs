﻿using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.Authentication
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
