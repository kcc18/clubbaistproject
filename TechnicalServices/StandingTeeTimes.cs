using BAIS3230Project.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3230Project.TechnicalServices
{
    public class StandingTeeTimes
    {

        private string _connectionString;
        //public StandingTeeTimes()
        //{
        //    // constructor logic
        //    ConfigurationBuilder DatabaseUsersBuilder = new();
        //    DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
        //    DatabaseUsersBuilder.AddJsonFile("appsettings.json");
        //    IConfiguration DatabaseUserConfiguration = DatabaseUsersBuilder.Build();
        //    _connectionString = DatabaseUserConfiguration.GetConnectionString("DefaultConnection")!;
        //}


        public StandingTeeTimes()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<StandingTeeTimes>(); 

            IConfiguration config = builder.Build();

            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public bool BookStandingTeeTime(StandingTeeTime request)
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
                    CommandText = "AddStandingTeeTimeReservation"
                };

                // Add parameters from the StandingTeeTimeReservationRequest object
                command.Parameters.Add(new SqlParameter("@MemberID", SqlDbType.Int) { SqlValue = request.MemberID });
                command.Parameters.Add(new SqlParameter("@RequestedStartDate", SqlDbType.Date) { SqlValue = request.RequestedStartDate });
                command.Parameters.Add(new SqlParameter("@RequestedEndDate", SqlDbType.Date) { SqlValue = (object)request.RequestedEndDate ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@RequestedTeeTime", SqlDbType.Time) { SqlValue = request.RequestedTeeTime });

                // Handle nullable fields correctly
                command.Parameters.Add(new SqlParameter("@PriorityNumber", SqlDbType.Int) { SqlValue = (object)request.PriorityNumber ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int) { SqlValue = (object)request.EmployeeID ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@NumberOfPlayers", SqlDbType.Int) { SqlValue = request.NumberOfPlayers });

                // NEW FIELDS
                command.Parameters.Add(new SqlParameter("@OccurrenceDate", SqlDbType.Date) { SqlValue = request.OccurrenceDate });
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 20) { SqlValue = (object)request.Status ?? DBNull.Value });



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


        // View standing tee time by id

        public StandingTeeTime GetStandingTeeTimeById(int standingTeeTimeID)
        {
            StandingTeeTime existingStandingTeeTime = null;


            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("GetStandingTeeTimeByID", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@standingTeeTimeID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = standingTeeTimeID
            };

            cmd.Parameters.Add(param);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                existingStandingTeeTime = new StandingTeeTime
                {
                    StandingTeeTimeID = reader.GetInt32(reader.GetOrdinal("StandingTeeTimeID")),
                    MemberID = reader.GetInt32(reader.GetOrdinal("MemberID")),
                    RequestedStartDate = reader.GetDateTime(reader.GetOrdinal("RequestedStartDate")),
                    RequestedEndDate = reader.IsDBNull(reader.GetOrdinal("RequestedEndDate"))
                            ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("RequestedEndDate")),
                    RequestedTeeTime = reader.GetTimeSpan(reader.GetOrdinal("RequestedTeeTime")),
                    PriorityNumber = reader.IsDBNull(reader.GetOrdinal("PriorityNumber"))
                            ? (int?)null : reader.GetInt32(reader.GetOrdinal("PriorityNumber")),
                    EmployeeID = reader.IsDBNull(reader.GetOrdinal("EmployeeID"))
                            ? (int?)null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                    NumberOfPlayers = reader.IsDBNull(reader.GetOrdinal("NumberOfPlayers"))
                            ? 0 : reader.GetInt32(reader.GetOrdinal("NumberOfPlayers")),
                    OccurrenceDate = reader.GetDateTime(reader.GetOrdinal("OccurrenceDate")),
                    Status = reader.IsDBNull(reader.GetOrdinal("Status"))
                            ? null : reader.GetString(reader.GetOrdinal("Status"))

                };

            }

            reader.Close();
            MyDataSource.Close();
            return existingStandingTeeTime;
        }

        // View all Standing Tee Time

        public List<StandingTeeTime> GetAllStandingTeeTimes()
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            List<StandingTeeTime> standingTeeTimeList = new();

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("GetAllStandingTeeTimes", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                StandingTeeTime standingTeeTime = new StandingTeeTime
                {
                    StandingTeeTimeID = reader.GetInt32(reader.GetOrdinal("StandingTeeTimeID")),
                    MemberID = reader.GetInt32(reader.GetOrdinal("MemberID")),
                    RequestedStartDate = reader.GetDateTime(reader.GetOrdinal("RequestedStartDate")),
                    RequestedEndDate = reader.IsDBNull(reader.GetOrdinal("RequestedEndDate"))
                        ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("RequestedEndDate")),
                    RequestedTeeTime = reader.GetTimeSpan(reader.GetOrdinal("RequestedTeeTime")),
                    PriorityNumber = reader.IsDBNull(reader.GetOrdinal("PriorityNumber"))
                        ? (int?)null : reader.GetInt32(reader.GetOrdinal("PriorityNumber")),
                    EmployeeID = reader.IsDBNull(reader.GetOrdinal("EmployeeID"))
                        ? (int?)null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                    NumberOfPlayers = reader.IsDBNull(reader.GetOrdinal("NumberOfPlayers"))
                        ? 0 : reader.GetInt32(reader.GetOrdinal("NumberOfPlayers")),
                    OccurrenceDate = reader.GetDateTime(reader.GetOrdinal("OccurrenceDate")),
                    Status = reader.IsDBNull(reader.GetOrdinal("Status"))
                        ? null : reader.GetString(reader.GetOrdinal("Status"))

                };

                standingTeeTimeList.Add(standingTeeTime);
            }

            reader.Close();
            MyDataSource.Close();

            return standingTeeTimeList;
        }


        // Update standing tee time

        public bool UpdateStandingTeeTime(StandingTeeTime activeStandingTeeTime)
        {

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("UpdateStandingTeeTime", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter()
            {
                ParameterName = "@StandingTeeTimeID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.StandingTeeTimeID
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@MemberID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.MemberID
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@RequestedStartDate",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.RequestedStartDate
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@RequestedEndDate",
                SqlDbType = SqlDbType.DateTime,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.RequestedEndDate
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@RequestedTeeTime",
                SqlDbType = SqlDbType.Time,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.RequestedTeeTime
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@PriorityNumber",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.PriorityNumber
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@NumberOfPlayers",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.NumberOfPlayers
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@EmployeeID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.EmployeeID
            };
            cmd.Parameters.Add(param);

            param = new SqlParameter()
            {
                ParameterName = "@OccurrenceDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.OccurrenceDate
            };
            cmd.Parameters.Add(param);


            param = new SqlParameter()
            {
                ParameterName = "@Status",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = activeStandingTeeTime.Status
            };
            cmd.Parameters.Add(param);

            int rowsAffected = cmd.ExecuteNonQuery();
            MyDataSource.Close();

            // if statement here maybe if it returns, if not throw error?
            Console.WriteLine(" - Standing Tee Time updated.");


            return rowsAffected > 0;
        }

        // delete standing tee time
        public bool DeleteStandingTeeTime(int standingTeeTimeID)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("DeleteStandingTeeTimeReservation", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@StandingTeeTimeID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = standingTeeTimeID
            };
            cmd.Parameters.Add(param);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Success - Standing Tee Time deleted.");

            MyDataSource.Close();
            return rowsAffected > 0;
        }

        // logic for standing tee time reservation
        //public static List<StandingTeeTime> GetStandingTeeTimesForDate(DateTime targetDate, List<StandingTeeTime> allStandingTeeTimes)
        //{
        //    return allStandingTeeTimes
        //        .Where(s =>
        //            s.ApprovedTeeTime != default &&
        //            s.ApprovedDate != default &&
        //            targetDate >= s.RequestedStartDate &&
        //            targetDate <= s.RequestedEndDate &&
        //            targetDate.DayOfWeek == s.ApprovedDate.DayOfWeek
        //        )
        //        .ToList();
        //}

        // method for booking a standing tee time reservation (checks if user already has one)
        public List<StandingTeeTime> FindStandingTeeTimesByMemberId(int memberId)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            List<StandingTeeTime> standingTeeTimeList = new();

            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("FindStandingTeeTimeByMemberId", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Add the MemberID parameter
            cmd.Parameters.AddWithValue("@MemberID", memberId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                StandingTeeTime standingTeeTime = new StandingTeeTime
                {
                    StandingTeeTimeID = reader.GetInt32(0),
                    MemberID = reader.GetInt32(1),
                    RequestedStartDate = reader.GetDateTime(2),
                    RequestedTeeTime = reader.GetTimeSpan(3),
                    PriorityNumber = reader.GetInt32(4),
                    EmployeeID = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                    NumberOfPlayers = reader.GetInt32(6),
                    OccurrenceDate = reader.GetDateTime(7),
                    Status = reader.IsDBNull(8) ? null : reader.GetString(8)



                    // StandingTeeTimeID = reader.GetInt32(reader.GetOrdinal("StandingTeeTimeID")),
                    // MemberID = reader.GetInt32(reader.GetOrdinal("MemberID")),
                    // RequestedDayOfWeek = reader.GetString(reader.GetOrdinal("RequestedDayOfWeek")),
                    // RequestedStartDate = reader.GetDateTime(reader.GetOrdinal("RequestedStartDate")),
                    // RequestedEndDate = reader.IsDBNull(reader.GetOrdinal("RequestedEndDate"))
                    //? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("RequestedEndDate")),
                    // RequestedTeeTime = reader.GetTimeSpan(reader.GetOrdinal("RequestedTeeTime")),
                    // PriorityNumber = reader.IsDBNull(reader.GetOrdinal("PriorityNumber"))
                    //                  ? (int?)null : reader.GetInt32(reader.GetOrdinal("PriorityNumber")),
                    // ApprovedTeeTime = reader.IsDBNull(reader.GetOrdinal("ApprovedTeeTime"))
                    //                   ? (TimeSpan?)null : reader.GetTimeSpan(reader.GetOrdinal("ApprovedTeeTime")),
                    // EmployeeID = reader.IsDBNull(reader.GetOrdinal("EmployeeID"))
                    //              ? (int?)null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                    // ApprovedDate = reader.IsDBNull(reader.GetOrdinal("ApprovedDate"))
                    //                ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovedDate")),
                    // NumberOfPlayers = reader.IsDBNull(reader.GetOrdinal("NumberOfPlayers"))
                    //             ? 0 // or 4 if you want to default to a valid value
                    //             : reader.GetInt32(reader.GetOrdinal("NumberOfPlayers"))
                };

                standingTeeTimeList.Add(standingTeeTime);
            }

            reader.Close();
            MyDataSource.Close();

            return standingTeeTimeList;
        }


        // inserting reoccurring standing tee time (2 methods for functionality)

        public bool InsertReoccurringStandingTeeTime(StandingTeeTime reservation, DateTime occurrenceDate)
        {
            SqlConnection connection = new();
            connection.ConnectionString = _connectionString;
            connection.Open();

            try
            {
                SqlCommand command = new()
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AddStandingTeeTimeReservation"
                };

                // Required parameters
                command.Parameters.Add(new SqlParameter("@MemberID", SqlDbType.Int) { SqlValue = reservation.MemberID });
                command.Parameters.Add(new SqlParameter("@RequestedStartDate", SqlDbType.Date) { SqlValue = reservation.RequestedStartDate });
                command.Parameters.Add(new SqlParameter("@RequestedEndDate", SqlDbType.Date) { SqlValue = reservation.RequestedEndDate });
                command.Parameters.Add(new SqlParameter("@RequestedTeeTime", SqlDbType.Time) { SqlValue = reservation.RequestedTeeTime });

                // Optional parameters
                command.Parameters.Add(new SqlParameter("@PriorityNumber", SqlDbType.Int) { SqlValue = (object)reservation.PriorityNumber ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int) { SqlValue = (object)reservation.EmployeeID ?? DBNull.Value });
                command.Parameters.Add(new SqlParameter("@NumberOfPlayers", SqlDbType.Int) { SqlValue = reservation.NumberOfPlayers });

                // New fields
                command.Parameters.Add(new SqlParameter("@OccurrenceDate", SqlDbType.Date) { SqlValue = occurrenceDate });
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, 20) { SqlValue = (object)reservation.Status ?? "Pending" });

                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting recurring tee time: {ex.Message}");
                return false;
            }
        }


        //public bool CreateReoccurringStandingTeeTime(StandingTeeTime reservation)
        //{
        //    if (!reservation.RequestedEndDate.HasValue)
        //        throw new ArgumentException("RequestedEndDate must be provided.");

        //    DateTime currentDate = reservation.RequestedStartDate;
        //    DayOfWeek targetDay = reservation.OccurrenceDate.DayOfWeek;

        //    while (currentDate.DayOfWeek != targetDay)
        //        currentDate = currentDate.AddDays(1);

        //    bool allSuccess = true;

        //    while (currentDate <= reservation.RequestedEndDate.Value)
        //    {
        //        bool success = InsertReoccurringStandingTeeTime(reservation, currentDate);
        //        if (!success)
        //        {
        //            allSuccess = false;
        //            Console.WriteLine($"Failed to insert tee time on {currentDate.ToShortDateString()}");
        //        }

        //        currentDate = currentDate.AddDays(7);
        //    }

        //    return allSuccess;
        //}
        public bool CreateReoccurringStandingTeeTime(StandingTeeTime reservation)
        {
            if (!reservation.RequestedEndDate.HasValue)
                throw new ArgumentException("RequestedEndDate must be provided.");

            DateTime occurrenceDate = reservation.OccurrenceDate; // Start from the correct OccurrenceDate

            bool allSuccess = true;

            while (occurrenceDate <= reservation.RequestedEndDate.Value)
            {
                bool success = InsertReoccurringStandingTeeTime(reservation, occurrenceDate);
                if (!success)
                {
                    allSuccess = false;
                    Console.WriteLine($"Failed to insert tee time on {occurrenceDate.ToShortDateString()}");
                }

                occurrenceDate = occurrenceDate.AddDays(7); // Advance by a week
            }

            return allSuccess;
        }



    }
}
