using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSocial_Post.Models.Dtos
{
    public class ResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = true;
        public object? Data { get; set; }
    }
}