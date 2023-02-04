using System;
using System.ComponentModel.DataAnnotations;

namespace InterviewsApp.Core.DTOs
{
    public class UpdateCommentDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [StringLength(200, ErrorMessage = "Loc.Message.CommentSize")]
        public string Comment { get; set; }
    }
}
