using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendApp.Models.Task
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }
    }
}
