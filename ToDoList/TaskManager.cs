using System;

namespace ToDoList
{
    internal class TaskManager
    {
        private List<Task> tasks;
        private uint nextId;
        public TaskManager()
        {
            tasks = new List<Task>();
            nextId = 1;
        }
        public void InsertTask(string Specification)
        {
            tasks.Add(new Task(nextId++, Specification));
        }

        public void InsertLoadedTask(uint id, string Specification, byte completionStage,DateTime timeToCreate)
        {
            try
            {
                tasks.Add(new Task(id, Specification) { CompletionStage = completionStage, TimeToCreate = timeToCreate});
                nextId = tasks.Max(t => t.Id) + 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void ChangingCompStatus(uint id, byte completionStage)
        {
            var foundedTask = tasks.FirstOrDefault(t => t.Id == id);
            if (foundedTask != null)
            {
                foundedTask.CompletionStage = completionStage;
            }
        }

        public void DeleteTask(uint id)
        {
            var foundedTask = tasks.FirstOrDefault(t => t.Id == id);
            if (foundedTask != null)
            {
                tasks.Remove(foundedTask);
                NumerateTasks();
            }
        }
        public List<Task> GetTasks()
        {
            return tasks;
        }
        public List<Task> GetTasks(byte completionStage)
        {
            return tasks.Where(t => t.CompletionStage == completionStage).ToList();
        }

        private void NumerateTasks()
        {
            uint changedId = 1;
            foreach (var task in tasks)
            {
                task.Id = changedId++;
            }
            nextId = changedId;
        }
    }
}
