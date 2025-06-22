using BAIS3230Project.Validation;
using System.ComponentModel.DataAnnotations;

namespace BAIS3230Project.Domain
{
    public class TeeTime
    {
        public int TeeTimeID { get; set; }

        [Required(ErrorMessage = "Please select a date.")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Date must be today or later and cannot go past September 31st(end of season)")]
        public DateTime TeeDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Please select a tee time.")]
        public TimeSpan ScheduledTeeTime { get; set; }

        public int MemberID { get; set; }

        [Required(ErrorMessage = "Please enter the number of players.")]
        [Range(1, 4, ErrorMessage = "Number of players must be between 1 and 4.")]
        public int NumberOfPlayers { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits (no dashes or spaces).")]
        public string Phone { get; set; }

        [Range(0, 4, ErrorMessage = "Number of carts must be between 0 and 4.")]
        public int NumberOfCarts { get; set; }
    }
}
