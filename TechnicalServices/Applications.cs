using BAIS3230Project.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3230Project.TechnicalServices
{
    public class Applications
    {
        private string _connectionString;

        //public Applications()
        //{
        //    // constructor logic
        //    ConfigurationBuilder DatabaseUsersBuilder = new();
        //    DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
        //    DatabaseUsersBuilder.AddJsonFile("appsettings.json");
        //    IConfiguration DatabaseUserConfiguration = DatabaseUsersBuilder.Build();
        //    _connectionString = DatabaseUserConfiguration.GetConnectionString("DefaultConnection")!;
        //}

        public Applications()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Applications>(); 

            IConfiguration config = builder.Build();

            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }


        // book application
        public bool AddApplication(Application newApplication)
        {
            bool Success = false;

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand cmd = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddApplication"
            };

            SqlParameter param;

            param = new()
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.LastName
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.FirstName
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@Address",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.Address
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@City",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.City
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@Province",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.Province
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@PostalCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.PostalCode
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@Phone",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.Phone
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.Email
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@DateOfBirth",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.DateOfBirth
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@Occupation",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.Occupation
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@CompanyName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.CompanyName
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@CompanyAddress",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.CompanyAddress
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@CompanyPostalCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.CompanyPostalCode
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@CompanyPhone",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.CompanyPhone
            };
            cmd.Parameters.Add(param);

            // Might have to remove these shareholder fields 
            // I made junction tables to fix this issue
            param = new()
            {
                ParameterName = "@ShareholderOne",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.ShareholderOne
            };
            cmd.Parameters.Add(param);
            param = new()
            {
                ParameterName = "@ShareholderTwo",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.ShareholderTwo
            };
            cmd.Parameters.Add(param);
             
            param = new()
            {
                ParameterName = "@ApplicationStatus",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.ApplicationStatus
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@SubmissionDate",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.SubmissionDate
            };
            cmd.Parameters.Add(param);

            param = new()
            {
                ParameterName = "@MembershipTier",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newApplication.MembershipTier
            };
            cmd.Parameters.Add(param);



            cmd.ExecuteNonQuery();

            MyDataSource.Close();
            Success = true;

            return Success;
        }

        // Get application by id
        public Application GetApplicationById(int applicationId)
        {
            //Event existingEvent = null;
            Application existingApplication = null;

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("GetApplicationByID", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@ApplicationID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = applicationId
            };

            cmd.Parameters.Add(param);

            SqlDataReader reader = cmd.ExecuteReader();

            // test this code block - got the reader tostring, Getints from chatgpt -- havent seen it done before w/ getordinal
            // test it
            if (reader.Read())
            {
                existingApplication = new Application
                {
                    ApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    City = reader.GetString(reader.GetOrdinal("City")),
                    Province = reader.GetString(reader.GetOrdinal("Province")),
                    PostalCode = reader.GetString(reader.GetOrdinal("PostalCode")),
                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                    Occupation = reader.GetString(reader.GetOrdinal("Occupation")),
                    CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                    CompanyAddress = reader.GetString(reader.GetOrdinal("CompanyAddress")),
                    CompanyPostalCode = reader.GetString(reader.GetOrdinal("CompanyPostalCode")),
                    CompanyPhone = reader.GetString(reader.GetOrdinal("CompanyPhone")),
                    ShareholderOne = reader.GetInt32(reader.GetOrdinal("ShareholderOne")),
                    ShareholderTwo = reader.GetInt32(reader.GetOrdinal("ShareholderTwo")),
                    ApplicationStatus = reader.GetString(reader.GetOrdinal("ApplicationStatus")),
                    SubmissionDate = reader.GetDateTime(reader.GetOrdinal("SubmissionDate")),
                    MembershipTier = reader.GetString(reader.GetOrdinal("MembershipTier"))
                };

            }

            reader.Close();
            MyDataSource.Close();
            return existingApplication;
        }


        // View all applications
        public List<Application> GetAllApplications()
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            List<Application> applicationList = new();

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("GetAllApplications", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Application existingApplication = new Application
                {
                    ApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    City = reader.GetString(reader.GetOrdinal("City")),
                    Province = reader.GetString(reader.GetOrdinal("Province")),
                    PostalCode = reader.GetString(reader.GetOrdinal("PostalCode")),
                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                    Occupation = reader.GetString(reader.GetOrdinal("Occupation")),
                    CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                    CompanyAddress = reader.GetString(reader.GetOrdinal("CompanyAddress")),
                    CompanyPostalCode = reader.GetString(reader.GetOrdinal("CompanyPostalCode")),
                    CompanyPhone = reader.GetString(reader.GetOrdinal("CompanyPhone")),
                    ShareholderOne = reader.GetInt32(reader.GetOrdinal("ShareholderOne")),
                    ShareholderTwo = reader.GetInt32(reader.GetOrdinal("ShareholderTwo")),
                    ApplicationStatus = reader.GetString(reader.GetOrdinal("ApplicationStatus")),
                    SubmissionDate = reader.GetDateTime(reader.GetOrdinal("SubmissionDate")),

                    MembershipTier = reader.GetString(reader.GetOrdinal("MembershipTier"))
                };

                applicationList.Add(existingApplication);
            }

            reader.Close();
            MyDataSource.Close();

            return applicationList;
        }

        // Update application

        public bool UpdateApplication(Application existingApplication)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("UpdateApplication", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter()
            {
                ParameterName = "@ApplicationID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.ApplicationID
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.LastName
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.FirstName
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@Address",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.Address
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@City",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.City
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@Province",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.Province
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@PostalCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.PostalCode
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@Phone",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.Phone
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.Email
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@DateOfBirth",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.DateOfBirth
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@Occupation",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.Occupation
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@CompanyName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.CompanyName
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@CompanyAddress",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.CompanyAddress
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@CompanyPostalCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.CompanyPostalCode
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@CompanyPhone",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.CompanyPhone
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@ShareholderOne",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.ShareholderOne
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@ShareholderTwo",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.ShareholderTwo
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@ApplicationStatus",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.ApplicationStatus
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@SubmissionDate",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.SubmissionDate
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@MembershipTier",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = existingApplication.MembershipTier
            };
            cmd.Parameters.Add(param);


            int rowsAffected = cmd.ExecuteNonQuery();
            MyDataSource.Close();
            Console.WriteLine(" - Application updated.");

            return rowsAffected > 0;
        }


        // delete application
        public bool DeleteApplication(int applicationId)
        {

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("DeleteApplication", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@ApplicationID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = applicationId
            };
            cmd.Parameters.Add(param);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Sucess - Application Deleted");


            MyDataSource.Close();
            return rowsAffected > 0;
        }
    }
}
