using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // task1
        Thread newThread1 = new Thread(TypingNumbers);
        Thread newThread2 = new Thread(DisplayLoresIpsum);
        newThread1.Start();
        newThread2.Start();
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Main Thread ID: {Thread.CurrentThread.ManagedThreadId}, Number: {i}");
            Thread.Sleep(150);
        }

        //task2
        await HelloAsync("Anna");

        Console.ReadLine();
    }

    static void TypingNumbers()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}, Number: {i}");
            Thread.Sleep(100);
        }
    }
    static void DisplayLoresIpsum()
    {
        try
        {
            string text = File.ReadAllText("C:\\Users\\User\\source\\repos\\lab1\\LoremIpsum.txt");
            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' });
            Console.WriteLine("enter the amount you want to withdraw");
            int count = int.Parse(Console.ReadLine());
            if (count <= words.Length)
            {
                Console.WriteLine($" {count} words file 'Lorem ipsum':");
                for (int i = 0; i < count; i++)
                {
                    Console.Write($"{words[i]} ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"The entered number of all of them is too large ({words.Length}).");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception open file: {ex.Message}");

        }

    }

    static async Task HelloAsync(string name)
    {
        Console.WriteLine("Task2");
        Console.WriteLine("Доброго дня, " + name);
        await ProcessTasksAsync();
        Console.WriteLine("До побачення, " + name);
    }
    static async Task ProcessTasksAsync()
    {
        Console.WriteLine("Початок обробки асинхронних задач.");
        var tasks = new List<Task<int>>();
        for (int i = 1; i <= 2; i++)
        {
            tasks.Add(TaskAddAsync(i));
        }
        await Task.WhenAll(tasks);

        Console.WriteLine("Обробка асинхронних задач завершена.");
        foreach (var task in tasks)
        {
            Console.WriteLine($"Результат обробки: {task.Result}");
        }
    }
    static async Task<int> TaskAddAsync(int taskId)
    {
        Console.WriteLine($"Початок обробки task {taskId}.");
        await Task.Delay(taskId * 1000);
        Console.WriteLine($"Завершення обробки task {taskId}.");
        return taskId + 100;
    }

}
