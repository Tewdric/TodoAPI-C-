using System.ComponentModel.DataAnnotations;

namespace TodoList.Dtos
{
    public class TaskUpdateDto
    {
        [Required]
        public string Description { get; set; } = string.Empty;

        public bool IsDone { get; set; }
    }
}