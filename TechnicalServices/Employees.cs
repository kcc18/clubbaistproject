using System.Data;
using BAIS3230Project.Domain;
using Microsoft.Data.SqlClient;

namespace BAIS3230Project.TechnicalServices
{
    public class Employees
    {
        private string _connectionString;

        //public Employees()
        //{
        //    // constructor logic
        //    ConfigurationBuilder DatabaseUsersBuilder = new();
        //    DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
        //    DatabaseUsersBuilder.AddJsonFile("appsettings.json");
        //    IConfiguration DatabaseUserConfiguration = DatabaseUsersBuilder.Build();
        //    _connectionString = DatabaseUserConfiguration.GetConnectionString("DefaultConnection")!;
        //}

        public Employees()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Employees>(); 

            IConfiguration config = builder.Build();

            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }
        public List<Employee> GetAllEmployees()
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            List<Employee> employeeList = new();

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("GetAllEmployees", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Employee employee = new Employee
                {
                    EmployeeID = reader.GetInt32(0),
                    FullName = reader["FullName"].ToString(),
                    //FirstName = reader["FirstName"].ToString(),
                    //Address = reader["Address"].ToString(),
                    //City = reader["City"].ToString(),
                    //Province = reader["Province"].ToString(),
                    //PostalCode = reader["PostalCode"].ToString(),
                    //Phone = reader["Phone"].ToString(),
                    //Email = reader["Email"].ToString(),
                    //JobTitle = reader["JobTitle"].ToString(),
                    //HireDate = reader.GetDateTime(10)
                };

                employeeList.Add(employee);
            }

            reader.Close();
            MyDataSource.Close();

            return employeeList;
        }
    }
}
