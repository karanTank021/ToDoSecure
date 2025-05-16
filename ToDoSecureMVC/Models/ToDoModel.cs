using System.Text.Json.Serialization;

namespace ToDoSecureMVC.Models
{
    public class ToDoModel
    {
        public int Id { get; set; }       
        public string? Task { get; set; }       
        public bool IsCompleted { get; set; }
    }
}
