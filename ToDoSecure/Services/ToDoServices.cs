using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoSecure.DAL;
using ToDoSecure.DTO;
using ToDoSecure.Models;
using ToDoSecure.Services.Interface;

namespace ToDoSecure.Services
{
    public class ToDoServices : IToDoServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ToDoServices(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ToDoDTO> CreateAsync(ToDoDTO toDo)
        {
            var create = new ToDoItems()
            {
                Task = toDo.Task,
                IsCompleted = toDo.IsCompleted,
            };
            
            await _dbContext.ToDoItems.AddAsync(create);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ToDoDTO>(create);
        }

        public async Task<ToDoItems> DeleteAsync(int id)
        {
            var remove = await _dbContext.ToDoItems.FindAsync(id);
            if(remove != null)
            {
                _dbContext.ToDoItems.Remove(remove);
                await _dbContext.SaveChangesAsync();
            }
            return remove;
        }

        public async Task<List<ToDoItems>> GetAllAsync()
        {
            return await _dbContext.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItems> GetByIdAsync(int id)
        {
            return await _dbContext.ToDoItems.FindAsync(id);
        }

        public async Task<ToDoDTO> UpdateAsync(int id, ToDoDTO toDo)
        {
            var update = await _dbContext.ToDoItems.FindAsync(id);
            if(update != null)
            {
                update.Task = toDo.Task;
                update.IsCompleted = toDo.IsCompleted;
                _dbContext.ToDoItems.Update(update);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<ToDoDTO>(update);
            }
            return null;
        }
    }
}
