using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApp.Models.Task
{
    public enum PriorityLevel
    {
        High,
        Medium,
        Low
    }

    public class Priority
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public PriorityLevel Level { get; set; } 
        public ICollection<TaskItem> Tasks { get; set; } 
        
    }
}