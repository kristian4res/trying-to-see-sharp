using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class TaskManagerService
    {
        private List<ToDoTask> _taskList;
        private readonly DataService _dataService;
        private int _nextId;


        public TaskManagerService(DataService dataService)
        {
            _dataService = dataService;
            _taskList = _dataService.LoadTasks();
            _nextId = _taskList.Any() ? _taskList.Max(t => t.Id) + 1 : 1;
        }
        public void AddTask(string title, List<string> objectives, bool IsCompleted = false)
        {
            try
            {
                if (title.Length == 0)
                {
                    Console.WriteLine("Task titles cannot be empty, please write a title.");
                    return;
                }

                var toDoItem = new ToDoTask
                {
                    Id = _nextId++,
                    Title = title,
                    Objectives = objectives,
                    IsCompleted = IsCompleted
                };
                _taskList.Add(toDoItem);

                if (_dataService.SaveTasks(_taskList))
                {
                    Console.WriteLine($"Successfully added task, {toDoItem.Title}, to To Do list at {toDoItem.CreatedAt}\n");
                    toDoItem.DisplayToDoTaskDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding task: {ex.Message}");
                return;
            }
        }

        public void ViewTask()
        {
            return;
        }

        public void UpdateTask()
        {
            return;
        }

        public void DeleteTask()
        {
            return;
        }

        public void DisplayTaskDetails()
        {
            return;
        }

        public void MarkTaskAsComplete()
        {
            return;
        }
    }
}