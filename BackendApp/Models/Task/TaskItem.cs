using System;
using System.ComponentModel.DataAnnotations;
using BackendApp.Models.Task;

namespace BackendApp.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
        
        // Project relation
        public Project Project { get; set; }
        public int? ProjectId { get; set; }
        
        // Priority relation
        public Priority Priority { get; set; }
        public int PriorityId { get; set; }

        // Tag relation (assuming a one-to-many relation for simplicity)
        public Tag Tag { get; set; }
        public int TagId { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
