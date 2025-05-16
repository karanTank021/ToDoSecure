using ToDoSecure.DTO;
using ToDoSecure.Models;

namespace ToDoSecure.Services.Interface
{
    public interface IToDoServices
    {
        Task<List<ToDoItems>> GetAllAsync();
        Task<ToDoItems> GetByIdAsync(int id);
        Task<ToDoDTO> CreateAsync(ToDoDTO dto);
        Task<ToDoDTO> UpdateAsync(int id, ToDoDTO dto);
        Task<ToDoItems> DeleteAsync(int id);
    }
}
