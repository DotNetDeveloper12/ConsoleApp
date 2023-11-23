using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CrudeOp.Services
{
    public class TaskCreation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskFinishDate { get; set; }
        public int ProjectId { get; set; }



        public static void SortTasks(string sortColumn, string sortOrder)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                // Assuming spSortProjects is the name of your stored procedure
                string storedProcedureName = "spSort1";

                // Execute the stored procedure using Dapper
                var tasks = db.Query<TaskCreation>(storedProcedureName,
                    new { SortColumn = sortColumn, SortOrder = sortOrder },
                    commandType: CommandType.StoredProcedure);
                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id}, Code: {task.Code}, Name: {task.Name}, TaskStartDate: {task.TaskStartDate}, TaskFinishDate:{task.TaskFinishDate},projectId:{task.ProjectId} ");
                }
                Console.Read();

            }
        }


        public static void SearchTasks(string SearchTerm)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string storeProcedureName = "spSearch1";
                var tasks = db.Query<TaskCreation>(storeProcedureName,
                    new {searchTerm=SearchTerm},commandType:CommandType.StoredProcedure);
                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id}, Code: {task.Code}, Name: {task.Name}, StartDate: {task.TaskStartDate}, FinishDate:{task.TaskFinishDate},projectId:{task.ProjectId} ");
                }
                Console.Read();
            }
        }



        public static void PagingTasks(int PageNumber, int PageSize, int TotalReocrds)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string storeProcedure = "spPaging1";
                var pagingTasks = db.Query<TaskCreation>(storeProcedure,
                    new { pageNumber = PageNumber, pageSize = PageSize, totalRecords = TotalReocrds },
                    commandType: CommandType.StoredProcedure);
                foreach (var task in pagingTasks)
                {
                    Console.WriteLine($"ID: {task.Id}, Code: {task.Code}, Name: {task.Name}, TaskStartDate: {task.TaskStartDate}, TaskFinishDate:{task.TaskFinishDate},projectId:{task.ProjectId} ");
                }
                Console.Read();

            }
        }




























        public static void InsertTask(string code, string name, DateTime startDate, DateTime finishDate, int projectId)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                db.Execute("InsertTask", new { code, name, task_start_date = startDate, task_finish_date = finishDate, project_id = projectId }, commandType: CommandType.StoredProcedure);
            }
        }

        public static void UpdateTask(int id, string code, string name, DateTime startDate, DateTime finishDate, int projectId)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                db.Execute("UpdateTask", new { id,  code, name, task_start_date = startDate, task_finish_date = finishDate, project_id = projectId }, commandType: CommandType.StoredProcedure);//change  here project_id = projectId
            }
        }

        public static void DeleteTask(int id)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                db.Execute("DeleteTask", new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public static TaskCreation GetTaskById(int id)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                return db.Query<TaskCreation>("GetById", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public static IQueryable<TaskCreation> GetAllTasks()
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                List<TaskCreation> task = db.Query<TaskCreation>("GetAllTasks", commandType: CommandType.StoredProcedure).ToList();
                return task.AsQueryable();
            }
        }
    }
}
/*



using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace CrudeOp.Services
{
    public class TaskCreation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskFinishDate { get; set; }
        public int ProjectId { get; set; }

        // Existing code...

        public static IQueryable<TaskCreation> SortTasks(string columnName, string sortOrder, int projectId)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = $"SELECT * FROM Tasks WHERE ProjectId = {projectId} ORDER BY {columnName} {sortOrder}";
                List<TaskCreation> sortedTasks = db.Query<TaskCreation>(query).ToList();
                return sortedTasks.AsQueryable();
            }
        }

        public static IQueryable<TaskCreation> SearchTasks(string searchTerm)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = $"SELECT * FROM Tasks WHERE Name LIKE '%{searchTerm}%' OR Code LIKE '%{searchTerm}%'";
                List<TaskCreation> searchedTasks = db.Query<TaskCreation>(query).ToList();
                return searchedTasks.AsQueryable();
            }
        }

        public static IQueryable<TaskCreation> GetPagedTasks(int page, int pageSize, int projectId)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                int offset = (page - 1) * pageSize;
                string query = $"SELECT * FROM Tasks WHERE ProjectId = {projectId} LIMIT {pageSize} OFFSET {offset}";
                List<TaskCreation> pagedTasks = db.Query<TaskCreation>(query).ToList();
                return pagedTasks.AsQueryable();
            }
        }

        // Existing code...

        public static void InsertTask(string code, string name, DateTime startDate, DateTime finishDate, int projectId)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Tasks (Code, Name, TaskStartDate, TaskFinishDate, ProjectId) VALUES (@Code, @Name, @TaskStartDate, @TaskFinishDate, @ProjectId)";
                db.Execute(query, new { Code = code, Name = name, TaskStartDate = startDate, TaskFinishDate = finishDate, ProjectId = projectId });
            }
        }

        public static void UpdateTask(int id, string code, string name, DateTime startDate, DateTime finishDate, int projectId)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE Tasks SET Code = @Code, Name = @Name, TaskStartDate = @TaskStartDate, TaskFinishDate = @TaskFinishDate, ProjectId = @ProjectId WHERE Id = @Id";
                db.Execute(query, new { Id = id, Code = code, Name = name, TaskStartDate = startDate, TaskFinishDate = finishDate, ProjectId = projectId });
            }
        }

        public static void DeleteTask(int id)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Tasks WHERE Id = @Id";
                db.Execute(query, new { Id = id });
            }
        }
    }
}
*/