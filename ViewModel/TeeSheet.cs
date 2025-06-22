using BAIS3230Project.Domain;

namespace BAIS3230Project.ViewModel
{
    public class TeeSheet
    {
        public int? TeeTimeID { get; set; }
        public int? StandingTeeTimeID { get; set; }
        public int? EventID { get; set; } // <-- Add this!

        public int? MemberID { get; set; }

        public TimeSpan TeeTime { get; set; }
        public DateTime TeeDate { get; set; }

        public int? NumberOfPlayers { get; set; }
        public string? Phone { get; set; }
        public int? NumberOfCarts { get; set; }

        public string Type { get; set; } // "Regular", "Standing", or "Event"
    }
}
