using System.Data;
using Microsoft.Data.SqlClient;
using BAIS3230Project.Domain;
using System.Data.Common;
using System.Diagnostics;


namespace BAIS3230Project.TechnicalServices
{
    public class Events
    {

        private string _connectionString;

        //public Events()
        //{
        //    // constructor logic
        //    ConfigurationBuilder DatabaseUsersBuilder = new();
        //    DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
        //    DatabaseUsersBuilder.AddJsonFile("appsettings.json");
        //    IConfiguration DatabaseUserConfiguration = DatabaseUsersBuilder.Build();
        //    _connectionString = DatabaseUserConfiguration.GetConnectionString("DefaultConnection")!;
        //}

        public Events()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Events>(); 

            IConfiguration config = builder.Build();

            _connectionString = config.GetConnectionString("DefaultConnection")!;

        }
        public bool AddEvent(Event newEvent)
        {
            bool Success = false;

            SqlConnection MyDataSource = new();
            //MyDataSource.ConnectionString = @"";
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand AddEventCommand = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddEvent"
            };

            SqlParameter AddEventCommandParameter;

            AddEventCommandParameter = new()
            {
                ParameterName = "@EventName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newEvent.EventName
            };

            AddEventCommand.Parameters.Add(AddEventCommandParameter);

            AddEventCommandParameter = new()
            {
                ParameterName = "@EventDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                SqlValue = newEvent.EventDate
            };

            AddEventCommand.Parameters.Add(AddEventCommandParameter);

            AddEventCommandParameter = new()
            {
                ParameterName = "@StartTime",
                SqlDbType = SqlDbType.Time,
                Direction = ParameterDirection.Input,
                SqlValue = newEvent.StartTime
            };

            AddEventCommand.Parameters.Add(AddEventCommandParameter);

            AddEventCommandParameter = new()
            {
                ParameterName = "@EndTime",
                SqlDbType = SqlDbType.Time,
                Direction = ParameterDirection.Input,
                SqlValue = newEvent.EndTime
            };

            AddEventCommand.Parameters.Add(AddEventCommandParameter);


            AddEventCommandParameter = new()
            {
                ParameterName = "@EventType",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = newEvent.EventType
            };

            AddEventCommand.Parameters.Add(AddEventCommandParameter);

            AddEventCommand.ExecuteNonQuery();

            MyDataSource.Close();
            Success = true;

            return Success;
        }

        public Event GetEventById(int eventId)
        {
            Event existingEvent = null;

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("GetEventByID", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@EventID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = eventId
            };

            cmd.Parameters.Add(param);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                existingEvent = new Event
                {
                    EventID = reader.GetInt32(0),  // Assuming EventID is the first column
                    EventName = reader["EventName"].ToString(),  // Get EventName as string
                    EventDate = reader.GetDateTime(2),  // Assuming EventDate is the third column, adjust as needed
                    StartTime = reader.GetTimeSpan(3),  // Assuming StartTime is the fourth column, adjust as needed
                    EndTime = reader.GetTimeSpan(4),  // Assuming EndTime is the fifth column, adjust as needed
                    EventType = reader["EventType"].ToString()  // Get EventType as string
                };

            }

            reader.Close();
            MyDataSource.Close();
            return existingEvent;
        }

        public List<Event> GetAllEvents()
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            List<Event> eventList = new();

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("GetAllEvents", MyDataSource)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Event existingEvent = new Event
                {
                    EventID = reader.GetInt32(0),
                    EventName = reader["EventName"].ToString(),
                    EventDate = reader.GetDateTime(2),
                    StartTime = reader.GetTimeSpan(3),
                    EndTime = reader.GetTimeSpan(4),
                    EventType = reader["EventType"].ToString()
                };

                eventList.Add(existingEvent);
            }

            reader.Close();
            MyDataSource.Close();

            return eventList;
        }

        public bool UpdateEvent(Event existingEvent)
        {
            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            try
            {
                MyDataSource.Open();
                SqlCommand cmd = new SqlCommand("UpdateEvent", MyDataSource);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = new SqlParameter()
                {
                    ParameterName = "@EventID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = existingEvent.EventID
                };
                cmd.Parameters.Add(param);

                param = new SqlParameter()
                {
                    ParameterName = "@EventName",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = existingEvent.EventName
                };
                cmd.Parameters.Add(param);

                param = new SqlParameter()
                {
                    ParameterName = "@EventDate",
                    SqlDbType = SqlDbType.Date,
                    Direction = ParameterDirection.Input,
                    SqlValue = existingEvent.EventDate
                };
                cmd.Parameters.Add(param);

                param = new SqlParameter()
                {
                    ParameterName = "@StartTime",
                    SqlDbType = SqlDbType.Time,
                    Direction = ParameterDirection.Input,
                    SqlValue = existingEvent.StartTime
                };
                cmd.Parameters.Add(param);

                param = new SqlParameter()
                {
                    ParameterName = "@EndTime",
                    SqlDbType = SqlDbType.Time,
                    Direction = ParameterDirection.Input,
                    SqlValue = existingEvent.EndTime
                };
                cmd.Parameters.Add(param);

                param = new SqlParameter()
                {
                    ParameterName = "@EventType",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = existingEvent.EventType
                };
                cmd.Parameters.Add(param);

                int rowsAffected = cmd.ExecuteNonQuery();
                MyDataSource.Close();
                Console.WriteLine(" - Event updated.");

                return rowsAffected > 0;

            }
            catch (Exception ex)
            {
                if (MyDataSource.State == ConnectionState.Open)
                {
                    MyDataSource.Close();
                }
                return false;
            }

        }

        public bool DeleteEvent(int eventID)
        {

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;

            MyDataSource.Open();
            SqlCommand cmd = new SqlCommand("DeleteEvent", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@EventID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = eventID
            };
            cmd.Parameters.Add(param);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine("Sucess - Event Deleted");


            MyDataSource.Close();
            return rowsAffected > 0;
        }

        // logic for handling blocking off of time slots if an event is booked on that day
        public List<Event> GetEventsByDate(DateTime date)
        {
            List<Event> events = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = _connectionString;
            MyDataSource.Open();

            SqlCommand cmd = new SqlCommand("GetEventsByDate", MyDataSource);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@Date",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                SqlValue = date
            };

            cmd.Parameters.Add(param);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var currentEvent = new Event
                {
                    EventID = reader.GetInt32(0),
                    EventName = reader["EventName"].ToString(),
                    EventDate = reader.GetDateTime(2),
                    StartTime = reader.GetTimeSpan(3),
                    EndTime = reader.GetTimeSpan(4),
                    EventType = reader["EventType"].ToString()
                };

                events.Add(currentEvent);
            }

            reader.Close();
            MyDataSource.Close();

            return events;
        }

        // checking if date is blocked by an event
        public bool IsDateBlockedByEvent(DateTime date)
        {
            bool isBlocked = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("CheckIfDateIsBlockedByEvent", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@DateToCheck",
                        SqlDbType = SqlDbType.Date,
                        Direction = ParameterDirection.Input,
                        SqlValue = date.Date
                    });

                    // Output parameter
                    SqlParameter outputParam = new SqlParameter
                    {
                        ParameterName = "@IsBlocked",
                        SqlDbType = SqlDbType.Bit,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    isBlocked = Convert.ToBoolean(outputParam.Value);
                }
            }

            return isBlocked;
        }


    }
}
