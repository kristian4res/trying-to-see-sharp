using System.Text.Json;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class DataService
    {
        private readonly string _filePath;

        public DataService(string filePath = "Data/todoList.json")
        {
            _filePath = filePath;
        }

        public List<ToDoTask> LoadTasks()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<ToDoTask>();
                }

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<ToDoTask>>(json) ?? new List<ToDoTask>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                return new List<ToDoTask>();
            }
        } 

        public bool SaveTasks(List<ToDoTask> taskList)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(taskList, options);
                File.WriteAllText(_filePath, json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
                return false;
            }
        }
    }
}