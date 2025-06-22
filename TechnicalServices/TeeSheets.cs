using BAIS3230Project.Domain;
using BAIS3230Project.ViewModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BAIS3230Project.TechnicalServices
{
    public class TeeSheets
    {
        private string _connectionString;

        //public TeeSheets()
        //{
        //    // constructor logic
        //    ConfigurationBuilder DatabaseUsersBuilder = new();
        //    DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
        //    DatabaseUsersBuilder.AddJsonFile("appsettings.json");
        //    IConfiguration DatabaseUserConfiguration = DatabaseUsersBuilder.Build();
        //    _connectionString = DatabaseUserConfiguration.GetConnectionString("DefaultConnection")!;
        //}

        public TeeSheets()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<TeeSheets>(); 

            IConfiguration config = builder.Build();

            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }
        public List<TeeSheet> GetTeeSheetByDate(DateTime teeSheetDate)
        {
            var teeSheet = new List<TeeSheet>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetTeeSheetByDate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TeeSheetDate", teeSheetDate);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sheetEntry = new TeeSheet
                        {
                            TeeTimeID = reader["TeeTimeID"] as int?,
                            StandingTeeTimeID = reader["StandingTeeTimeID"] as int?,
                            EventID = reader["EventID"] as int?,
                            MemberID = reader["MemberID"] as int? ?? 0, 
                            TeeTime = (TimeSpan)reader["TeeTime"],
                            NumberOfPlayers = reader["NumberOfPlayers"] as int?,
                            Phone = reader["Phone"]?.ToString() ?? string.Empty,
                            NumberOfCarts = reader["NumberOfCarts"] as int?,
                            TeeDate = Convert.ToDateTime(reader["TeeDate"]),
                            Type = reader["Type"]?.ToString() ?? "Unknown"
                        };

                        teeSheet.Add(sheetEntry);
                    }
                }
            }

            return teeSheet;
        }

    }
}
