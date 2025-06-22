using BAIS3230Project.Validation;
using System.ComponentModel.DataAnnotations;
using static BAIS3230Project.Validation.ValidationHelpers;

namespace BAIS3230Project.Domain
{
    public class Event
    {
        public int EventID { get; set; }

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(50, ErrorMessage = "Event name cannot exceed 50 characters.")]
        public string EventName { get; set; }

        [FutureDate(ErrorMessage = "Date must be today or later and cannot go past September 30th (end of season).")]
        public DateTime EventDate { get; set; } 

        [Required(ErrorMessage = "Start time is required.")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [CustomValidation(typeof(EventValidationHelpers), nameof(EventValidationHelpers.ValidateEndTimeAfterStartTime))]
        public TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = "Event type is required.")]
        [StringLength(50, ErrorMessage = "Event type cannot exceed 50 characters.")]
        public string EventType { get; set; }


    }
}
