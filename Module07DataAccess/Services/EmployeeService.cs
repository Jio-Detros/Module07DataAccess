using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Module07DataAccess.Model;
using MySql.Data.MySqlClient;

namespace Module07DataAccess.Services
{
    public class EmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService()
        {
            var dbService = new DatabaseConnectionService();
            _connectionString = dbService.GetConnectionString();
        }

        public async Task<List<Employee>> GetAllEmployeesAsync() // Updated method name
        {
            var employees = new List<Employee>(); // Updated variable name
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                // Retrieve Data
                var cmd = new MySqlCommand("SELECT * FROM tblEmployee", conn); // Updated table name

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        employees.Add(new Employee // Correctly mapping to Employee properties
                        {
                            EmployeeID = reader.GetInt32("EmployeeID"), // Updated property name
                            Name = reader.GetString("Name"),
                            Address = reader.GetString("Address"), // Added Address
                            Email = reader.GetString("Email"),     // Added Email
                            ContactNo = reader.GetString("ContactNo")
                        });
                    }
                }
            }
            return employees; // Correctly returning the employee list
        }
    }
}
