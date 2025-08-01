using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TodoList.Models 
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<TodoTask> Tasks { get; set; } = new List<TodoTask>();
    }
}