using System.ComponentModel.DataAnnotations;

namespace TodoList.Models 
{
    public class TodoTask
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}