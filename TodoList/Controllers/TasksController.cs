using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.Data;
using TodoList.Models;
using TodoList.Dtos;

namespace TodoList.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            
            var userIdString = User.FindFirstValue("id");

            if (userIdString == null)
            {
                
                return Unauthorized();
            }

            var userId = int.Parse(userIdString);

            
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto taskDto)
        {
            var userIdString = User.FindFirstValue("id");

            var newTask = new TodoTask
            {
                Description = taskDto.Description, 
                IsDone = false,
                UserId = int.Parse(userIdString!) 
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasks), new { id = newTask.Id }, newTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpdateDto updatedTaskDto)
        {
            var userIdString = User.FindFirstValue("id");
            var userId = int.Parse(userIdString!);

            var taskFromDb = await _context.Tasks.FindAsync(id);

            if (taskFromDb == null)
            {
                return NotFound();
            }

            if (taskFromDb.UserId != userId)
            {
                return Forbid();
            }

            taskFromDb.Description = updatedTaskDto.Description;
            taskFromDb.IsDone = updatedTaskDto.IsDone;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userIdString = User.FindFirstValue("id");
            var userId = int.Parse(userIdString!);

            var taskFromDb = await _context.Tasks.FindAsync(id);

            if (taskFromDb == null)
            {
                return NotFound();
            }

            if (taskFromDb.UserId != userId)
            {
                return Forbid();
            }


            _context.Tasks.Remove(taskFromDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}