using System;
using System.ComponentModel.DataAnnotations;
using BAIS3230Project.Validation;

namespace BAIS3230Project.Domain
{
    public class StandingTeeTime
    {
        public int StandingTeeTimeID { get; set; }

        [Required(ErrorMessage = "Please select a member.")]
        public int MemberID { get; set; }

        [Required(ErrorMessage = "Please enter a start date.")]
        [FutureDate(ErrorMessage = "Date must be today or later and cannot go past September 31st(end of season)")]
        public DateTime RequestedStartDate { get; set; } = DateTime.Now;

        public DateTime? RequestedEndDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please enter a requested tee time.")]
        public TimeSpan RequestedTeeTime { get; set; }

        public int? PriorityNumber { get; set; }

        public int? EmployeeID { get; set; }

        [Required(ErrorMessage = "Please enter the number of players.")]
        [Range(4, 4, ErrorMessage = "Number of players must be 4.")]
        public int? NumberOfPlayers { get; set; }


        [Required(ErrorMessage = "Please enter the occurrence date.")]
        public DateTime OccurrenceDate { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string? Status { get; set; }
    }
}
