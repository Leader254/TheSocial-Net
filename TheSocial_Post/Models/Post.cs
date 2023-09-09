using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TheSocial_Post.Models.Dtos;

namespace TheSocial_Post.Models
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public IEnumerable<CommentsDto>? Comments { get; set; }
    }
}