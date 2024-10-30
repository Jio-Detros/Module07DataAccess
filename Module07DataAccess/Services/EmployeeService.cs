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


        public async Task<bool> AddEmployeeAsync(Employee newEmployee)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new MySqlCommand("INSERT INTO tblEmployee (Name, Address, Email, ContactNo) VALUES (@Name, @Address, @Email, @ContactNo)", conn);
                    cmd.Parameters.AddWithValue("@Name", newEmployee.Name);
                    cmd.Parameters.AddWithValue("@Address", newEmployee.Address);
                    cmd.Parameters.AddWithValue("@Email", newEmployee.Email);
                    cmd.Parameters.AddWithValue("@ContactNo", newEmployee.ContactNo);

                    var result = await cmd.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Employee Record: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new MySqlCommand("DELETE FROM tblEmployee WHERE EmployeeID = @ID", conn);
                    cmd.Parameters.AddWithValue("@ID", id);

                    var result = await cmd.ExecuteNonQueryAsync();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee record: {ex.Message}");
                return false;
            }
        }
    }
}
