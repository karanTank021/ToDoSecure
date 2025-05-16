using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoSecure.DAL;
using ToDoSecure.DTO;
using ToDoSecure.Services.Interface;

namespace ToDoSecure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoServices _toDoServices;
        private readonly ApplicationDbContext _context;

        public ToDoController(IToDoServices toDoServices, ApplicationDbContext context)
        {
            _toDoServices = toDoServices;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var get = await _toDoServices.GetAllAsync();
            return Ok(get);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _toDoServices.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTask(ToDoDTO toDo)
        {
            var create = await _toDoServices.CreateAsync(toDo);
            return Ok(create);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTask(int id, ToDoDTO toDo)
        {
            var updated = await _toDoServices.UpdateAsync(id, toDo);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }


        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            var todo = await _context.ToDoItems.FindAsync(id);
            if (todo == null)
                return NotFound();

            _context.ToDoItems.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
