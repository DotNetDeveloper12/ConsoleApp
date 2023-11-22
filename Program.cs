using CrudeOp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrudeOp.Services
{
    class Program
    {
        static void Main()
        {
            int selectedOption;
            int projectId = 0; // Initialize projectId
           

            do
            {
                Menu.LoadProjectMenu();
              
                Console.Write("Enter your choice: ");
                selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:

                      // View All Projects
                     var projects = ProjectCreation.GetAllProjects();
                      Console.WriteLine("===== All Projects =====");
                      foreach (var project in projects)
                      {
                          Console.WriteLine($"ID: {project.Id}, Code: {project.Code}, Name: {project.Name}, StartDate: {project.StartDate}, FinishDate:{project.FinishDate} ");
                      }
                      break;

                    case 2:
                        // Create New Project
                        

                        Console.Write("Enter code for the new project: ");
                        string projectCode = Console.ReadLine();
                        Console.Write("Enter name for the new project: ");
                        string projectName = Console.ReadLine();
                        Console.Write("Enter start date (yyyy-mm-dd) for the new project: ");
                        DateTime projectStartDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter finish date (yyyy-mm-dd) for the new project: ");
                        DateTime projectFinishDate = DateTime.Parse(Console.ReadLine());

                        ProjectCreation.InsertProject(projectCode, projectName, projectStartDate, projectFinishDate);
                        Console.WriteLine("Project created successfully.");
                        break;

                    case 3:
                        // Update Project
                        Console.Write("Enter the ID of the project to update: ");
                        int projectToUpdate = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter new code for the project: ");
                        string updatedCode = Console.ReadLine();
                        Console.Write("Enter new name for the project: ");
                        string updatedName = Console.ReadLine();
                        Console.Write("Enter new start date (yyyy-mm-dd) for the project: ");
                        DateTime updatedStartDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter new finish date (yyyy-mm-dd) for the project: ");
                        DateTime updatedFinishDate = DateTime.Parse(Console.ReadLine());

                        ProjectCreation.UpdateProject(projectToUpdate, updatedCode, updatedName, updatedStartDate, updatedFinishDate);
                        Console.WriteLine("Project updated successfully.");
                        break;

                    case 4:
                        // Delete Project
                        Console.Write("Enter the ID of the project to delete: ");
                        int projectToDelete = Convert.ToInt32(Console.ReadLine());

                        ProjectCreation.DeleteProject(projectToDelete);
                        Console.WriteLine("Project deleted successfully.");
                        break;
                   

                        //Apply Searching
                    case 5:
                        //Search Projects
                        Console.WriteLine("=====Search Projects=====");
                        Console.WriteLine("Enter Data Name to search by (e.g.,'Name' or 'Code'): ");
                        string SearchTerm = Console.ReadLine();
                        ProjectCreation.SearchProjects(SearchTerm);
                        break;



                    // Apply Sorting 
                    case 6:
                        // Sort Projects
                        Console.WriteLine("===== Sort Projects =====");
                        Console.Write("Enter column name to sort by (e.g., 'Name'): ");
                        string sortColumn = Console.ReadLine();
                        Console.Write("Enter sort order ('asc' or 'desc'): ");
                        string sortOrder = Console.ReadLine();
                        ProjectCreation.SortProjects( sortColumn, sortOrder);
                      //  List<ProjectCreation> sortedProjects = ProjectCreation.SortProjects(sortColumn, sortOrder);
                      //  Console.WriteLine($"===== Sorted Projects (by {sortColumn} in {sortOrder} order) =====");
                      // DisplayProjects(sortedProjects);
                        break;



                    //Paging 
                    case 7:
                        Console.WriteLine("=====View Page Projects=====");
                        Console.Write("Enter PageNumber ");
                        int PageNumber = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter PageSize");
                        int PageSize = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter TotalRecords");
                        int TotalRecords = Convert.ToInt32(Console.ReadLine());
                        ProjectCreation.PagingProjects(PageNumber, PageSize, TotalRecords);
                        break;


                    case 0:
                        Environment.Exit(0); // Exit the program
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                // If a project was selected, display the task menu
                if (selectedOption != 0 && selectedOption != 6)
                {
                    projectId = selectedOption; // Assign the selected project ID
                    
                    Menu.LoadTaskMenu(projectId);
                    Console.Write("Enter your choice: ");
                    selectedOption = Convert.ToInt32(Console.ReadLine());

                    switch (selectedOption)
                    {
                        case 1:
                            // View All Tasks for the selected project
                            var tasks = TaskCreation.GetAllTasks();
                            Console.WriteLine($"===== All Tasks for Project ID {projectId} =====");
                            foreach (var taskItem in tasks)
                            {
                                //  Console.WriteLine($"ID: {task.Id}, Code:{task.Code}");
                                Console.WriteLine( $"ID: {taskItem.Id},Code:{taskItem.Code}, Name: {taskItem.Name}, StartDate: {taskItem.TaskStartDate}, FinishDate: {taskItem.TaskFinishDate}, ProjectId: {projectId}");//task.ProjectId
                            }
                            break;

                        case 2:
                            // Create New Task
                            Console.Write("Enter code for the new task: ");
                            string taskCode = Console.ReadLine();
                            Console.Write("Enter name for the new task: ");
                            string taskName = Console.ReadLine();
                            Console.Write("Enter start date (yyyy-mm-dd) for the new task: ");
                            DateTime taskStartDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter finish date (yyyy-mm-dd) for the new task: ");
                            DateTime taskFinishDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Id of first table");
                            projectId = Convert.ToInt32(Console.ReadLine());

                            TaskCreation.InsertTask(taskCode, taskName, taskStartDate, taskFinishDate, projectId);
                            Console.WriteLine("Task created successfully.");
                            break;

                        case 3:
                            // Update Task
                            Console.Write("Enter the ID of the task to update: ");
                            int taskToUpdate = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter new code for the task: ");
                            string updatedTaskCode = Console.ReadLine();
                            Console.Write("Enter new name for the task: ");
                            string updatedTaskName = Console.ReadLine();
                            Console.Write("Enter new start date (yyyy-mm-dd) for the task: ");
                            DateTime updatedTaskStartDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter new finish date (yyyy-mm-dd) for the task: ");
                            DateTime updatedTaskFinishDate = DateTime.Parse(Console.ReadLine());

                            TaskCreation.UpdateTask(taskToUpdate, updatedTaskCode, updatedTaskName, updatedTaskStartDate, updatedTaskFinishDate, projectId);
                            Console.WriteLine("Task updated successfully.");
                            break;

                        case 4:
                            // Delete Task
                            Console.Write("Enter the ID of the task to delete: ");
                            int taskToDelete = Convert.ToInt32(Console.ReadLine());

                            TaskCreation.DeleteTask(taskToDelete);
                            Console.WriteLine("Task deleted successfully.");
                            break;

                        //Searching
                        case 5:
                            Console.WriteLine("=====Searching tasks====");
                            Console.Write("Enter Data Name to search by(e.g. NAME or Code): ");
                            string SearchTerm = Console.ReadLine();
                            TaskCreation.SearchTasks(SearchTerm);
                            break;

                        
                        // Apply Sorting 
                        case 6:
                            // Sort task
                            Console.WriteLine("===== Sort Tasks =====");
                            Console.Write("Enter column name to sort by (e.g., 'Name'): ");
                            string sortColumn = Console.ReadLine();
                            Console.Write("Enter sort order ('asc' or 'desc'): ");
                            string sortOrder = Console.ReadLine();
                            TaskCreation.SortTasks(sortColumn, sortOrder);
                            break;


                        //Paging
                        case 7:
                            Console.WriteLine("=====View Page Tasks=====");
                            Console.Write("Enter PageNumber ");
                            int PageNumber = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter PageSize");
                            int PageSize = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter TotalRecords");
                            int TotalRecords = Convert.ToInt32(Console.ReadLine());
                            TaskCreation.PagingTasks(PageNumber, PageSize, TotalRecords);
                            break;






                        case 0:
                            // Go back to Project Menu
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }

            } while (selectedOption != 0);
        }

   
    }
}
/*

using System;
using System.Linq;

namespace CrudeOp.Services
{
    class Program
    {
        static void Main()
        {
            int selectedOption;
            int projectId = 0; // Initialize projectId

            do
            {
                Menu.LoadProjectMenu();
                Console.Write("Enter your choice: ");
                selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:
                        // View All Projects with Sorting, Searching, and Paging
                        Console.WriteLine("===== All Projects =====");
                        Console.Write("Enter column name to sort by (e.g., 'Name'): ");
                        string sortColumn = Console.ReadLine();
                        Console.Write("Enter sort order ('asc' or 'desc'): ");
                        string sortOrder = Console.ReadLine();

                        var projects = ProjectCreation.SortProjects(sortColumn, sortOrder);
                        Console.WriteLine($"===== Sorted Projects (by {sortColumn} in {sortOrder} order) =====");
                        DisplayProjects(projects);
                        break;

                    case 2:
                        // Create New Project
                        Console.Write("Enter code for the new project: ");
                        string projectCode = Console.ReadLine();
                        Console.Write("Enter name for the new project: ");
                        string projectName = Console.ReadLine();
                        Console.Write("Enter start date (yyyy-mm-dd) for the new project: ");
                        DateTime projectStartDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter finish date (yyyy-mm-dd) for the new project: ");
                        DateTime projectFinishDate = DateTime.Parse(Console.ReadLine());

                        ProjectCreation.InsertProject(projectCode, projectName, projectStartDate, projectFinishDate);
                        Console.WriteLine("Project created successfully.");
                        break;

                    case 3:
                        // Update Project
                        Console.Write("Enter the ID of the project to update: ");
                        int projectToUpdate = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter new code for the project: ");
                        string updatedCode = Console.ReadLine();
                        Console.Write("Enter new name for the project: ");
                        string updatedName = Console.ReadLine();
                        Console.Write("Enter new start date (yyyy-mm-dd) for the project: ");
                        DateTime updatedStartDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter new finish date (yyyy-mm-dd) for the project: ");
                        DateTime updatedFinishDate = DateTime.Parse(Console.ReadLine());

                        ProjectCreation.UpdateProject(projectToUpdate, updatedCode, updatedName, updatedStartDate, updatedFinishDate);
                        Console.WriteLine("Project updated successfully.");
                        break;

                    case 4:
                        // Delete Project
                        Console.Write("Enter the ID of the project to delete: ");
                        int projectToDelete = Convert.ToInt32(Console.ReadLine());

                        ProjectCreation.DeleteProject(projectToDelete);
                        Console.WriteLine("Project deleted successfully.");
                        break;

                    case 5:
                        // Search Projects
                        Console.Write("Enter search term: ");
                        string searchProjectTerm = Console.ReadLine();

                        var searchedProjects = ProjectCreation.SearchProjects(searchProjectTerm);
                        Console.WriteLine($"===== Searched Projects (by '{searchProjectTerm}') =====");
                        DisplayProjects(searchedProjects);
                        break;

                    case 6:
                        // Sort Projects (existing case)
                        Console.Write("Enter column name to sort by (e.g., 'Name'): ");
                        string sortProjectColumn = Console.ReadLine();
                        Console.Write("Enter sort order ('asc' or 'desc'): ");
                        string sortProjectOrder = Console.ReadLine();

                        var sortedProjects = ProjectCreation.SortProjects(sortProjectColumn, sortProjectOrder);
                        Console.WriteLine($"===== Sorted Projects (by {sortProjectColumn} in {sortProjectOrder} order) =====");
                        DisplayProjects(sortedProjects);
                        break;

                    case 7:
                        // View Paged Projects
                        Console.Write("Enter page number: ");
                        int page = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter page size: ");
                        int pageSize = Convert.ToInt32(Console.ReadLine());

                        var pagedProjects = ProjectCreation.GetPagedProjects(page, pageSize);
                        Console.WriteLine($"===== Paged Projects (Page {page}, Page Size {pageSize}) =====");
                        DisplayProjects(pagedProjects);
                        break;

                    // ... (remaining cases)

                    case 0:
                        Environment.Exit(0); // Exit the program
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                // If a project was selected, display the task menu
                if (selectedOption != 0 && selectedOption != 4)
                {
                    projectId = selectedOption; // Assign the selected project ID

                    Menu.LoadTaskMenu(projectId);
                    Console.Write("Enter your choice: ");
                    selectedOption = Convert.ToInt32(Console.ReadLine());

                    switch (selectedOption)
                    {
                        case 1:
                            // View All Tasks for the selected project
                            Console.WriteLine($"===== All Tasks for Project ID {projectId} =====");
                            Console.Write("Enter column name to sort by (e.g., 'Name'): ");
                            string sortTaskColumn = Console.ReadLine();
                            Console.Write("Enter sort order ('asc' or 'desc'): ");
                            string sortTaskOrder = Console.ReadLine();

                            var tasks = TaskCreation.SortTasks(sortTaskColumn, sortTaskOrder, projectId);
                            Console.WriteLine($"===== Sorted Tasks (by {sortTaskColumn} in {sortTaskOrder} order) =====");
                            DisplayTasks(tasks, projectId);
                            break;

                        case 2:
                            // Create New Task
                            Console.Write("Enter code for the new task: ");
                            string taskCode = Console.ReadLine();
                            Console.Write("Enter name for the new task: ");
                            string taskName = Console.ReadLine();
                            Console.Write("Enter start date (yyyy-mm-dd) for the new task: ");
                            DateTime taskStartDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter finish date (yyyy-mm-dd) for the new task: ");
                            DateTime taskFinishDate = DateTime.Parse(Console.ReadLine());

                            TaskCreation.InsertTask(taskCode, taskName, taskStartDate, taskFinishDate, projectId);
                            Console.WriteLine("Task created successfully.");
                            break;

                        case 3:
                            // Update Task
                            Console.Write("Enter the ID of the task to update: ");
                            int taskToUpdate = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter new code for the task: ");
                            string updatedTaskCode = Console.ReadLine();
                            Console.Write("Enter new name for the task: ");
                            string updatedTaskName = Console.ReadLine();
                            Console.Write("Enter new start date (yyyy-mm-dd) for the task: ");
                            DateTime updatedTaskStartDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter new finish date (yyyy-mm-dd) for the task: ");
                            DateTime updatedTaskFinishDate = DateTime.Parse(Console.ReadLine());

                            TaskCreation.UpdateTask(taskToUpdate, updatedTaskCode, updatedTaskName, updatedTaskStartDate, updatedTaskFinishDate, projectId);
                            Console.WriteLine("Task updated successfully.");
                            break;

                        case 4:
                            // Delete Task
                            Console.Write("Enter the ID of the task to delete: ");
                            int taskToDelete = Convert.ToInt32(Console.ReadLine());

                            TaskCreation.DeleteTask(taskToDelete);
                            Console.WriteLine("Task deleted successfully.");
                            break;

                        case 5:
                            // Search Tasks
                            Console.Write("Enter search term: ");
                            string searchTaskTerm = Console.ReadLine();

                            var searchedTasks = TaskCreation.SearchTasks(searchTaskTerm);
                            Console.WriteLine($"===== Searched Tasks (by '{searchTaskTerm}') =====");
                            DisplayTasks(searchedTasks, projectId);
                            break;

                        case 6:
                            // Sort Tasks
                            Console.Write("Enter column name to sort by (e.g., 'Name'): ");
                            string sortTaskColumnNew = Console.ReadLine();
                            Console.Write("Enter sort order ('asc' or 'desc'): ");
                            string sortTaskOrderNew = Console.ReadLine();

                            var sortedTasks = TaskCreation.SortTasks(sortTaskColumnNew, sortTaskOrderNew, projectId);
                            Console.WriteLine($"===== Sorted Tasks (by {sortTaskColumnNew} in {sortTaskOrderNew} order) =====");
                            DisplayTasks(sortedTasks, projectId);
                            break;

                        case 7:
                            // View Paged Tasks
                            Console.Write("Enter page number: ");
                            int taskPage = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter page size: ");
                            int taskPageSize = Convert.ToInt32(Console.ReadLine());

                            var pagedTasks = TaskCreation.GetPagedTasks(taskPage, taskPageSize, projectId);
                            Console.WriteLine($"===== Paged Tasks (Page {taskPage}, Page Size {taskPageSize}) =====");
                            DisplayTasks(pagedTasks, projectId);
                            break;

                            // ... (remaining cases)
                    }
                }

            } while (selectedOption != 0);
        }

        // Helper method to display projects
        static void DisplayProjects(IQueryable<ProjectCreation> projects)
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"ID: {project.Id}, Code: {project.Code}, Name: {project.Name}, StartDate: {project.StartDate}, FinishDate:{project.FinishDate} ");
            }
        }

        // Helper method to display tasks
        static void DisplayTasks(IQueryable<TaskCreation> tasks, int projectId)
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.Id}, Code:{task.Code}, Name: {task.Name}, StartDate: {task.TaskStartDate}, FinishDate: {task.TaskFinishDate}, ProjectId: {projectId}");
            }
        }
    }
}
*/