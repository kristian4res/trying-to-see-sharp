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
            // TODO: Use helper methods
            return;
        }

        public void UpdateTaskAsComplete()
        {   

            return;
        }

        public void DeleteTask()
        {
            return;
        }

        // Helper methods
        public void DisplayAllTasks()
        {
            if (_taskList.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine("Current tasks:");
            Console.WriteLine("========================================");
            foreach (var task in _taskList.OrderBy(t => t.Id))
            {
                Console.WriteLine($"ID: {task.Id} | Title: {task.Title} | Completed: {task.IsCompleted} | Created at: {task.CreatedAt}");
            }
        }
        public void DisplayTaskDetails()
        {
            // TODO: Display specied task using id
            // QUESTIONS: Can I access the tasklist like a hash map? to get O(1) time? Or should I convert it? If so, that's still another process? Is it in the right DS to begin with?
            return;
        }

        public ToDoTask? GetTaskById(int id)
        {
            if (_taskList.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return null;
            }

            foreach (var task in _taskList)
            {
                if (task.Id == id)
                {
                    return task;
                }
            }

            Console.WriteLine($"Cannot find task with specified ID: {id}");
            return null;
        }

        public void MarkTaskAsComplete()
        {
            return;
        }
    }
}