using System.Data;
using BAIS3230Project.Domain;
using BAIS3230Project.ViewModel;
using Microsoft.Data.SqlClient;

namespace BAIS3230Project.TechnicalServices
{
    public class TeeTimes
    {
        private string _connectionString;

        //public TeeTimes()
        //{
        //    // constructor logic
        //    ConfigurationBuilder DatabaseUsersBuilder = new();
        //    DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
        //    DatabaseUsersBuilder.AddJsonFile("appsettings.json");
        //    IConfiguration DatabaseUserConfiguration = DatabaseUsersBuilder.Build();
        //    _connectionString = DatabaseUserConfiguration.GetConnectionString("DefaultConnection")!;
        //}

        public TeeTimes()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<TeeTimes>(); 

            IConfiguration config = builder.Build();

            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public bool BookTeeTime(TeeTime request)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            try
            {
                SqlCommand command = new()
                {
                    Connection = MyDataSource,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AddTeeTime"
                };

                // Add parameters from the BookTeeTimeRequest object
                command.Parameters.Add(new SqlParameter("@TeeDate", SqlDbType.Date) { SqlValue = request.TeeDate });
                command.Parameters.Add(new SqlParameter("@ScheduledTeeTime", SqlDbType.Time) { SqlValue = request.ScheduledTeeTime });
                command.Parameters.Add(new SqlParameter("@MemberID", SqlDbType.Int) { SqlValue = request.MemberID });
                command.Parameters.Add(new SqlParameter("@NumberOfPlayers", SqlDbType.Int) { SqlValue = request.NumberOfPlayers });
                command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 15) { SqlValue = request.Phone });
                command.Parameters.Add(new SqlParameter("@NumberOfCarts", SqlDbType.Int) { SqlValue = request.NumberOfCarts });

                // Execute stored procedure
                int rowsAffected = command.ExecuteNonQuery();
                MyDataSource.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // View all Tee Times

        public List<TeeTime> GetAllTeeTimes()
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            List<TeeTime> teeTimeList = new();

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("GetAllTeeTimes", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TeeTime teeTime = new TeeTime
                {
                    TeeTimeID = reader.GetInt32(0),
                    TeeDate = reader.GetDateTime(1),
                    ScheduledTeeTime = reader.GetTimeSpan(2),
                    MemberID = reader.GetInt32(3),
                    NumberOfPlayers = reader.GetInt32(4),
                    Phone = reader["Phone"].ToString(),
                    NumberOfCarts = reader.GetInt32(6),
                };

                teeTimeList.Add(teeTime);
            }

            reader.Close();
            MyDataSource.Close();

            return teeTimeList;
        }


        // View tee time by id

        public TeeTime GetTeeTimeById(int teeTimeID)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand TeeTimeCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetTeeTime"


            };

            SqlParameter TeeTimeCommandParameter = new()
            {
                ParameterName = "@TeeTimeID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = teeTimeID
            };

            TeeTimeCommand.Parameters.Add(TeeTimeCommandParameter);
            SqlDataReader DataReader = TeeTimeCommand.ExecuteReader();

            TeeTime teeTime = null;

            // Ensure program is always created, even if no rows are found
            if (DataReader.HasRows && DataReader.Read())
            {
                teeTime = new TeeTime
                {
                    TeeTimeID = DataReader.GetInt32(0),
                    TeeDate = DataReader.GetDateTime(1),  // TeeDate is at index 0
                    ScheduledTeeTime = DataReader.GetTimeSpan(2),  // ScheduledTeeTime is at index 1
                    MemberID = DataReader.GetInt32(3),  // MemberID is at index 2
                    NumberOfPlayers = DataReader.GetInt32(4),  // NumberOfPlayers is at index 3
                    Phone = DataReader["Phone"].ToString(),  // Phone is at index 4 (assuming it's in the query)
                    NumberOfCarts = DataReader.GetInt32(6),  // NumberOfCarts is at index 5
                };
            }

            DataReader.Close();
            MyDataSource.Close();
            return teeTime;
        }

        // Update tee time

        public bool UpdateTeeTime(TeeTime activeTeeTime)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();


            SqlCommand cmd = new SqlCommand("UpdateTeeTime", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter()
            {
                ParameterName = "@TeeTimeID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = activeTeeTime.TeeTimeID
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@TeeDate",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input,
                SqlValue = activeTeeTime.TeeDate
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@ScheduledTeeTime",
                SqlDbType = SqlDbType.Time,
                Direction = ParameterDirection.Input,
                SqlValue = activeTeeTime.ScheduledTeeTime
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@MemberID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = activeTeeTime.MemberID
            };
            cmd.Parameters.Add(param);
            param = new SqlParameter()
            {
                ParameterName = "@NumberOfPlayers",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = activeTeeTime.NumberOfPlayers
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@Phone",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = activeTeeTime.Phone
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@NumberOfCarts",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = activeTeeTime.NumberOfCarts
            };
            cmd.Parameters.Add(param);

            int rowsAffected = cmd.ExecuteNonQuery();
            MyDataSource.Close();
            Console.WriteLine(" - Tee Time updated.");

            return rowsAffected > 0;
        }

        // delete  tee time

        public bool DeleteTeeTime(int teeTimeID)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("DeleteTeeTime", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@TeeTimeID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = teeTimeID
            };
            cmd.Parameters.Add(param);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Success - Tee Time deleted.");

            MyDataSource.Close();
            return rowsAffected > 0;
        }


        // get tee time by email (for MyReservations page)
        public List<TeeTime> GetTeeTimesByEmail(string email)
        {
            var reservations = new List<TeeTime>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetTeeTimesByEmail", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservations.Add(new TeeTime
                        {
                            TeeTimeID = (int)reader["TeeTimeID"],
                            TeeDate = (DateTime)reader["TeeDate"],
                            ScheduledTeeTime = (TimeSpan)reader["ScheduledTeeTime"],
                            MemberID = (int)reader["MemberID"],
                            NumberOfPlayers = reader["NumberOfPlayers"] as int? ?? 0,
                            Phone = reader["Phone"]?.ToString() ?? string.Empty,
                            NumberOfCarts = reader["NumberOfCarts"] as int? ?? 0,
                        });

                    }
                }
            }

            return reservations;
        }
    }
}
