using System;
using System.Text.Json;

namespace ToDoList
{
    internal class FileManager
    {
        private readonly string PathToFile;

        public FileManager(string PathToFile)
        {
            this.PathToFile = PathToFile;
        }

        public void SaveList(List<Task> tasks)
        {
            try { 

            var json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(PathToFile, json);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        
        public List<Task> LoadSavedList()
        {
            try
            {
                if (!File.Exists(PathToFile))
                {
                    return new List<Task>();
                }
                else
                {
                    var json = File.ReadAllText(PathToFile);
                    return JsonSerializer.Deserialize<List<Task>>(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.ReadKey();
                return new List<Task>();
            }
        }
    }
}
