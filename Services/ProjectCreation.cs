using CrudeOp.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CrudeOp.Services
{
    public class ProjectCreation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }


        //Sorting Data
        public static void SortProjects(string sortColumn, string sortOrder)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                // Assuming spSortProjects is the name of your stored procedure
                string storedProcedureName = "spSort";

                // Execute the stored procedure using Dapper 
               var projects= db.Query<ProjectCreation>(storedProcedureName,
                   new { SortColumn = sortColumn, SortOrder = sortOrder },
                   commandType: CommandType.StoredProcedure);
                foreach (var project in projects)
                {
                    Console.WriteLine($"ID: {project.Id}, Code: {project.Code}, Name: {project.Name}, StartDate: {project.StartDate}, FinishDate:{project.FinishDate} ");
                }
                Console.Read();

            }
        }

        //Searching Data 
        public static void SearchProjects(string SearchTerm)
        {
            using(IDbConnection db = DatabaseConnection.GetConnection())
            {
                string storeProcedureName = "spSearch";

                var searchProjects = db.Query<ProjectCreation>(storeProcedureName,
                    new { searchTerm = SearchTerm }, commandType: CommandType.StoredProcedure);
                foreach(var project in searchProjects)
                {
                    Console.WriteLine($"ID:{project.Id}, Code:{project.Code}, Name:{project.Name}, StartDate:{project.StartDate}, FinishDate:{project.FinishDate}");
                }
                Console.Read();
            }
        }


        //Paging Data
        public static void PagingProjects(int PageNumber, int PageSize,int TotalReocrds)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string storeProcedure = "spPaging";
                var pagingProjects = db.Query<ProjectCreation>(storeProcedure,
                    new { pageNumber = PageNumber, pageSize = PageSize, totalRecords = TotalReocrds },
                    commandType: CommandType.StoredProcedure);
                foreach (var project  in pagingProjects)
                {
                    Console.WriteLine($"ID:{project.Id}, Code:{project.Code}, Name:{project.Name}, StartDate:{project.StartDate}, FinishDate:{project.FinishDate}");
                }
                Console.Read();
                   
            }
        }










        public static void InsertProject(string code, string name, DateTime startDate, DateTime finishDate)
        {
              using (IDbConnection db = DatabaseConnection.GetConnection())
              {
                  db.Execute("InsertProject", new { Code=code, Name=name, start_date = startDate, finish_date = finishDate }, commandType: CommandType.StoredProcedure);
              }
        }
       


        public static void UpdateProject(int id, string code, string name, DateTime startDate, DateTime finishDate)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                db.Execute("UpdateProject", new { Id = id, code, name, start_date = startDate, finish_date = finishDate }, commandType: CommandType.StoredProcedure);
            }
        }

        public static void DeleteProject(int id)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                db.Execute("DeleteProject", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public static ProjectCreation GetProject(int id)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                return db.Query<ProjectCreation>("GetProject", new { Id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public static IQueryable<ProjectCreation> GetAllProjects()
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                return db.Query<ProjectCreation>("GetAllProjects", commandType: CommandType.StoredProcedure).AsQueryable();
            }
        }

        internal static void DisplayProjects(IQueryable<ProjectCreation> projects)
        {
            throw new NotImplementedException();
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
    public class ProjectCreation
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

        // Existing code...

        public static IQueryable<ProjectCreation> SortProjects(string columnName, string sortOrder)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = $"SELECT * FROM Projects ORDER BY {columnName} {sortOrder}";
                List<ProjectCreation> sortedProject = db.Query<ProjectCreation>(query).ToList();
                return sortedProject.AsQueryable();
            }
        }

        public static IQueryable<ProjectCreation> SearchProjects(string searchTerm)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = $"SELECT * FROM Projects WHERE Name LIKE '%{searchTerm}%' OR Code LIKE '%{searchTerm}%'";
                List<ProjectCreation> searchedProjects = db.Query<ProjectCreation>(query).ToList();
                return searchedProjects.AsQueryable();
            }
        }

        public static IQueryable<ProjectCreation> GetPagedProjects(int page, int pageSize)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                int offset = (page - 1) * pageSize;
                string query = $"SELECT * FROM Projects LIMIT {pageSize} OFFSET {offset}";
                List<ProjectCreation> pagedProjects = db.Query<ProjectCreation>(query).ToList();
                return pagedProjects.AsQueryable();
            }
        }

        // Existing code...

        public static void InsertProject(string code, string name, DateTime startDate, DateTime finishDate)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Project (Code, Name, StartDate, FinishDate) VALUES (@Code, @Name, @StartDate, @FinishDate)";
                db.Execute(query, new { Code = code, Name = name, StartDate = startDate, FinishDate = finishDate });
            }
        }

        public static void UpdateProject(int id, string code, string name, DateTime startDate, DateTime finishDate)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE Projects SET Code = @Code, Name = @Name, StartDate = @StartDate, FinishDate = @FinishDate WHERE Id = @Id";
                db.Execute(query, new { Id = id, Code = code, Name = name, StartDate = startDate, FinishDate = finishDate });
            }
        }

        public static void DeleteProject(int id)
        {
            using (IDbConnection db = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Projects WHERE Id = @Id";
                db.Execute(query, new { Id = id });
            }
        }
    }
}
*/