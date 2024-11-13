using System;
using System.Collections.Generic;

namespace Assignment_4
{
    public class Project
    {
        private string projectName;
        private string projectManager;
        private DateTime startDate;
        private DateTime endDate;
        private List<string> tasks;
        private bool isCompleted;

        // Constructor to initialize the project details
        public Project(string name, string manager, DateTime start, DateTime end)
        {
            projectName = name;
            projectManager = manager;
            startDate = start;
            endDate = end;
            tasks = new List<string>();
            isCompleted = false;
        }

        // Private method to check if the project is overdue
        private bool IsOverdue()
        {
            return DateTime.Now > endDate && !isCompleted;
        }

        // Method to add a new task to the project
        public void AddTask(string task)
        {
            tasks.Add(task);
            Console.WriteLine($"Task '{task}' added to project '{projectName}'.");
        }

        // Method to mark a task as completed
        public void CompleteTask(string task)
        {
            if (tasks.Contains(task))
            {
                tasks.Remove(task);
                Console.WriteLine($"Task '{task}' completed in project '{projectName}'.");
            }
            else
            {
                Console.WriteLine($"Task '{task}' not found in project '{projectName}'.");
            }
        }

        // Method to mark the project as completed
        public void CompleteProject()
        {
            isCompleted = true;
            Console.WriteLine($"Project '{projectName}' is marked as completed.");
        }

        // Method to display the project status
        public void DisplayStatus()
        {
            Console.WriteLine("\n--- Project Status ---");
            Console.WriteLine($"Project: {projectName}");
            Console.WriteLine($"Managed by: {projectManager}");
            Console.WriteLine($"Start Date: {startDate.ToShortDateString()}");
            Console.WriteLine($"End Date: {endDate.ToShortDateString()}");
            Console.WriteLine($"Tasks Remaining: {tasks.Count}");
            Console.WriteLine($"Status: {(isCompleted ? "Completed" : "In Progress")}");
            if (IsOverdue())
            {
                Console.WriteLine("Warning: The project is overdue!");
            }
            Console.WriteLine("-----------------------\n");
        }

        // Method to list all tasks
        public void ListTasks()
        {
            Console.WriteLine("\n--- Project Tasks ---");
            foreach (var task in tasks)
            {
                Console.WriteLine($"- {task}");
            }
            Console.WriteLine("---------------------\n");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Prompt user for initial project details with validation
            Console.Write("Enter project name: ");
            string projectName = ReadNonEmptyString();

            Console.Write("Enter project manager's name: ");
            string projectManager = ReadNonEmptyString();

            Console.Write("Enter project start date (yyyy-mm-dd): ");
            DateTime startDate = ReadValidDate();

            DateTime endDate;
            do
            {
                Console.Write("Enter project end date (yyyy-mm-dd): ");
                endDate = ReadValidDate();

                if (endDate < startDate)
                {
                    Console.WriteLine("End date cannot be earlier than the start date. Please enter a valid end date.");
                }
            } while (endDate < startDate);

            // Creating an instance of the Project class
            Project project = new Project(projectName, projectManager, startDate, endDate);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Add a Task");
                Console.WriteLine("2. Complete a Task");
                Console.WriteLine("3. Display Project Status");
                Console.WriteLine("4. List All Tasks");
                Console.WriteLine("5. Mark Project as Completed");
                Console.WriteLine("6. Exit");

                Console.Write("\nEnter your choice (1-6): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter task description: ");
                        string taskDescription = ReadNonEmptyString();
                        project.AddTask(taskDescription);
                        break;
                    case "2":
                        Console.Write("Enter the task description to complete: ");
                        string taskToComplete = Console.ReadLine();
                        project.CompleteTask(taskToComplete);
                        break;
                    case "3":
                        project.DisplayStatus();
                        break;
                    case "4":
                        project.ListTasks();
                        break;
                    case "5":
                        project.CompleteProject();
                        break;
                    case "6":
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        // Method to read a non-empty string from the user
        private static string ReadNonEmptyString()
        {
            string input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write("Input cannot be empty. Please enter a valid value: ");
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        // Method to read a valid date from the user
        private static DateTime ReadValidDate()
        {
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.Write("Invalid date format. Please enter a valid date (yyyy-mm-dd): ");
            }
            return date;
        }
    }
}
