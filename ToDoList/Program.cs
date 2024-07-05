using System.Text;
using ToDoList;

class Program
{
    private static TaskManager taskManager = new TaskManager();
    private static FileManager fileManager = new FileManager("records.json"); //Находится в папке "..\ToDoList\bin\Debug\net8.0"

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8; // Возможность взаимодействовать на кирилице
        // Загрузка предыдущего сохранения
        Console.WriteLine("Желаете ли загрузить предыдущую сессию? (Y/N)");
        var flag = Console.ReadLine();
        switch (flag)
        {
            case "Y":
                var tasks = fileManager.LoadSavedList();
                foreach (var task in tasks)
                {
                    taskManager.InsertLoadedTask(task.Id, task.Specification, task.CompletionStage, task.TimeToCreate);
                }
                break;
            case "N":
                {
                    new List<Task>();
                    break;
                }
            default:
                new List<Task>();
                break;
        }
        //-----------------
        while (true)
        {
            Console.Clear();
            ChangeColorMenu("=============== Список задач ===============");
            ChangeColorMenu("|               Версия 1.0.0               |");
            ChangeColorMenu("|    1. Добавить задачу                    |");
            ChangeColorMenu("|    2. Изменить этап выполнения задачи    |");
            ChangeColorMenu("|    3. Удалить задачу                     |");
            ChangeColorMenu("|    4. Просмотр всех задач                |");
            ChangeColorMenu("|    5. Вывод задач с фильтром             |");
            ChangeColorMenu("|    6. Сохранить и выйти                  |");
            ChangeColorMenu("|    7. Завершить без сохранения           |");
            ChangeColorMenu("|                                          |");
            ChangeColorMenu("============================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Выберите опцию:");
            Console.Write(" ->");
            var choice = Console.ReadLine();
            Console.ResetColor();

            switch (choice)
            {
                case "1":
                    InsertNewTask();
                    break;
                case "2":
                    ChangeTypeTask();
                    break;
                case "3":
                    DeleteTask();
                    break;
                case "4":
                    OutputAllTasks();
                    break;
                case "5":
                    ChooseTypeOfTasks();
                    break;
                case "6":
                    SaveAndExit();
                    return;
                case "7":
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Неверная опция, попробуйте снова.");
                    Console.ResetColor();
                    break;
            }
            //Завершение работы
            Console.WriteLine(" Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }
    }

    private static void InsertNewTask()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(" Введите описание задачи:");
        try
        {
            var specification = Console.ReadLine();
            taskManager.InsertTask(specification);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Задача добавлена!");
            Console.ResetColor();
        }
        catch(Exception e)
            {
            Console.WriteLine(" Exception: " + e.Message);
        }
        Console.ResetColor();
    }
    
    private static void ChangeTypeTask()
    {
        Console.Clear();
        Console.WriteLine(" Введите ID задачи этап завершения которой хотите изменить:");
        if (uint.TryParse(Console.ReadLine(), out uint id))
        {
        //Добавить вывод текущего статуса задачи
            Console.WriteLine(" Введите этап задачи (Невыполненные[0], Начато выполнение[1], Выполнено[2]):");
            try
            {
                string CLStage;
                byte completionStage;
                do
                {
                    Console.Clear();
                    Console.WriteLine(" ID выбранной задачи: " + id);
                    CLStage = Console.ReadLine();
                    completionStage = byte.Parse(CLStage);
                } while (completionStage < 0 || completionStage > 2);
                taskManager.ChangingCompStatus(id, completionStage);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Изменение произведено успешно");
            }
            catch (Exception e)
            {
                Console.WriteLine(" Exception: " + e.Message);
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Неверный ID");
        }
        Console.ResetColor();
    }
    
    private static void DeleteTask()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(" Введите ID задачи для удаления:");
        if (uint.TryParse(Console.ReadLine(), out uint id))
        {
            taskManager.DeleteTask(id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Задача удалена!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Неверный ID");
        }
        Console.ResetColor();
    }
  
    private static void OutputAllTasks()
    {
        Console.Clear();
        Console.WriteLine(" Все задачи:");
        foreach (var task in taskManager.GetTasks())
        {
            switch (task.CompletionStage)
            {
                case 0:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    }
                case 1:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    }
                case 2:
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    }
            }

            Console.WriteLine(task);
        }
        Console.ResetColor();
    }
    
    private static void ChooseTypeOfTasks()
    {
        Console.Clear();
        ChangeColorMenu("========== Этап задачи ==========");
        ChangeColorMenu("|    1. Невыполненные задачи    |");
        ChangeColorMenu("|    2. Начатые задачи          |");
        ChangeColorMenu("|    3. Завершенные задачи      |");
        ChangeColorMenu("=================================");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(" Выберите опцию:");
        Console.Write(" ->");

        var choice = Console.ReadLine();

        Console.ResetColor();

        List<Task> tasks;
        switch (choice)
        {
            case "1":
                {
                    tasks = taskManager.GetTasks(0);
                    Console.WriteLine("Невыполненные задачи");
                    break;
                }
            case "2":
                {
                    tasks = taskManager.GetTasks(1);
                    Console.WriteLine("Начатые задачи");
                    break;
                }
            case "3":
                {
                    tasks = taskManager.GetTasks(2);
                    Console.WriteLine("Выполненные задачи");
                    break;
                }
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неверная опция");
                return;
        }
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    private static void SaveAndExit()
    {
        fileManager.SaveList(taskManager.GetTasks());
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Сохранение прошло успешно. Завершение программы");
        Console.ResetColor();
    }

    static void ChangeColorMenu(string message)
    {
        foreach (char c in message)
        {
            if (c == '|' || c == '=')
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(c);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(c);
                Console.ResetColor();
            }
        }
        Console.WriteLine();
    }
}

