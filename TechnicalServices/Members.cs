using BAIS3230Project.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3230Project.TechnicalServices
{
    public class Members
    {
        private string _connectionString;

        //public Members()
        //{
        //    // constructor logic
        //    ConfigurationBuilder DatabaseUsersBuilder = new();
        //    DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
        //    DatabaseUsersBuilder.AddJsonFile("appsettings.json");
        //    IConfiguration DatabaseUserConfiguration = DatabaseUsersBuilder.Build();
        //    _connectionString = DatabaseUserConfiguration.GetConnectionString("DefaultConnection")!;
        //}

        public Members()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Members>(); 

            IConfiguration config = builder.Build();

            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        // Add Member

        public bool AddMember(Member newMember)
        {
            bool Success = false;

            SqlConnection MyDataSource = new();
            // MyDataSource.ConnectionString = @""; // Use your connection string
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand AddMemberCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddMember"
            };

            SqlParameter AddMemberCommandParameter;

            // Adding parameters for AddMember stored procedure
            AddMemberCommandParameter = new()
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.LastName
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.FirstName
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@Address",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.Address
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@PostalCode",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.PostalCode
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@Phone",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.Phone
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.Email
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@MembershipTier",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.MembershipTier
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@City",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.City ?? (object)DBNull.Value  // Use DBNull if null
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@Province",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.Province ?? (object)DBNull.Value
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@AccountStatus",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.AccountStatus ?? (object)DBNull.Value
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@IsShareholder",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.IsShareholder
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            AddMemberCommandParameter = new()
            {
                ParameterName = "@MembershipStartDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                SqlValue = newMember.MembershipStartDate
            };
            AddMemberCommand.Parameters.Add(AddMemberCommandParameter);

            // Execute the command
            AddMemberCommand.ExecuteNonQuery();

            MyDataSource.Close();
            Success = true;

            return Success;
        }

        // view all members 
        public List<Member> GetAllMembers()
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            List<Member> memberList = new();

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("GetAllMembers", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Member member = new Member
                {
                    MemberID = reader.GetInt32(0),
                    FullName = reader["FullName"].ToString(),
                };

                memberList.Add(member);
            }
            reader.Close();
            MyDataSource.Close();

            return memberList;
        }


        // get member by email

        public Member GetMemberByEmail(string email)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            Member member = null;

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("GetMemberByEmail", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Email", email);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                member = new Member
                {
                    MemberID = reader.GetInt32(reader.GetOrdinal("MemberID")),
                    Email = reader["Email"].ToString()
                };

            }

            reader.Close();
            MyDataSource.Close();

            return member;
        }


        // get member account status
        public string GetMemberAccountStatus(int memberId)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            string accountStatus = null;

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("GetMemberAccountStatus", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@MemberID", memberId);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                accountStatus = reader["AccountStatus"].ToString();
            }

            reader.Close();
            MyDataSource.Close();

            return accountStatus;
        }

        // Get member by id

        public Member GetMemberByID(int memberId)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            Member? member = null;

            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("GetMemberByID", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@MemberID", memberId);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                member = new Member
                {
                    MemberID = reader.GetInt32(reader.GetOrdinal("MemberID")),
                    LastName = reader["LastName"].ToString(),
                    FirstName = reader["FirstName"].ToString(),
                    Address = reader["Address"].ToString(),
                    PostalCode = reader["PostalCode"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    MembershipTier = reader["MembershipTier"].ToString(),
                    City = reader["City"].ToString(),
                    Province = reader["Province"].ToString(),
                    AccountStatus = reader["AccountStatus"].ToString(),
                    IsShareholder = Convert.ToBoolean(reader["IsShareholder"]),
                    MembershipStartDate = Convert.ToDateTime(reader["MembershipStartDate"])
                };
            }

            reader.Close();
            MyDataSource.Close();

            return member;
        }

    }
}
