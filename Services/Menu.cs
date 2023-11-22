/*using System;

namespace CrudeOp.Services
{
    public class Menu
    {
        public static void LoadProjectMenu()
        {
            Console.WriteLine("===== Project Menu =====");
            Console.WriteLine("1. View All Projects");
            Console.WriteLine("2. Create New Project");
            Console.WriteLine("3. Update Project");
            Console.WriteLine("4. Delete Project");
            Console.WriteLine("0. Exit");
        }

        public static void LoadTaskMenu(int projectId)
        {
            Console.WriteLine($"===== Task Menu for Project ID {projectId} =====");
            Console.WriteLine("1. View All Tasks");
            Console.WriteLine("2. Create New Task");
            Console.WriteLine("3. Update Task");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("0. Go Back to Project Menu");
        }
    }
}
*/


using System;

namespace CrudeOp.Services
{
    public class Menu
    {
        public static void LoadProjectMenu()
        {
            Console.WriteLine("===== Project Menu =====");
            Console.WriteLine("1. View All Projects");
            Console.WriteLine("2. Create New Project");
            Console.WriteLine("3. Update Project");
            Console.WriteLine("4. Delete Project");
            Console.WriteLine("5. Search Projects");
            Console.WriteLine("6. Sort Projects");
            Console.WriteLine("7. View Page Project");
            Console.WriteLine("0. Exit");
        }

            public static void LoadTaskMenu(int projectId)
            {
                Console.WriteLine($"===== Task Menu for Project ID {projectId} =====");
                Console.WriteLine("1. View All Tasks");
                Console.WriteLine("2. Create New Task");
                Console.WriteLine("3. Update Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Search Tasks");
                Console.WriteLine("6. Sort Tasks");
                Console.WriteLine("7. View Paged Tasks");
                Console.WriteLine("0. Go Back to Project Menu");
            }
        }
    }

