using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackendApp.Data;
using BackendApp.Models;

namespace BackendApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {

        private readonly TaskManagerContext _context;

        public TaskController(TaskManagerContext context)
        {
            _context = context;
        }


        // GET
        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetTask()
        {
            return _context.TaskItems.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTaskItem(int id)
        {
            var task = _context.TaskItems.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // POST: api/Tasks
        [HttpPost]
        public ActionResult<TaskItem> PostTaskItem(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            _context.SaveChanges();

            return CreatedAtAction("GetTaskItem", new { id = taskItem.Id }, taskItem);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult PutTaskItem(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                if (!_context.TaskItems.Any(t => t.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public ActionResult<TaskItem> DeleteTaskItem(int id)
        {
            var task = _context.TaskItems.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(task);
            _context.SaveChanges();

            return task;
        }
    }


        
}
