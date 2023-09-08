using System;
using System.ComponentModel.DataAnnotations;
using BackendApp.Model.Auth;

namespace BackendApp.Models.Task
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public DateTime DatePosted { get; set; }

        public TaskItem Task { get; set; }
        public int TaskItemId { get; set; }
        
        public ApplicationUser PostedBy { get; set; }
        public string PostedById { get; set; }
    }
}
