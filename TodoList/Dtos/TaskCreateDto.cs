using System.ComponentModel.DataAnnotations;

namespace TodoList.Dtos
{
    public class TaskCreateDto
    {
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}