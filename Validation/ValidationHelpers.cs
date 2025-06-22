using System.ComponentModel.DataAnnotations;

namespace BAIS3230Project.Validation
{
    public static class ValidationHelpers
    {
        public static ValidationResult ValidateDOB(DateTime dateOfBirth, ValidationContext context)
        {
            if (dateOfBirth >= DateTime.Today)
                return new ValidationResult("Date of birth must be in the past.");

            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;

            if (age < 16)
                return new ValidationResult("You must be at least 16 years old.");

            if (age > 100)
                return new ValidationResult("Date of birth is too far in the past. Age cannot be more than 100 years.");

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateFutureOrToday(DateTime date, ValidationContext context)
        {
            if (date.Date < DateTime.Today)
                return new ValidationResult("Submission date cannot be in the past.");

            return ValidationResult.Success;
        }
        public static ValidationResult ValidateHireDate(DateTime hireDate, ValidationContext context)
        {
            if (hireDate.Date > DateTime.Today)
            {
                return new ValidationResult("Hire date cannot be in the future.");
            }

            if (hireDate.Year < 1970)
            {
                return new ValidationResult("Hire date is too far in the past.");
            }

            return ValidationResult.Success;
        }

        public static class EventValidationHelpers
        {
            public static ValidationResult ValidateEndTimeAfterStartTime(TimeSpan endTime, ValidationContext context)
            {
                var instance = context.ObjectInstance as BAIS3230Project.Domain.Event;
                if (instance == null)
                    return ValidationResult.Success;

                if (endTime <= instance.StartTime)
                {
                    return new ValidationResult("End time must be after start time.");
                }

                return ValidationResult.Success;
            }
        }


    }
}
