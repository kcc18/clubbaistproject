using System.ComponentModel.DataAnnotations;
using BAIS3230Project.Validation;

namespace BAIS3230Project.Domain
{
    public class Application
    {
        public int ApplicationID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string Address { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Province must be a 2-letter code (e.g., AB, BC).")]
        public string Province { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$", ErrorMessage = "Postal Code must be in a valid Canadian format (e.g., A1A 1A1).")]
        public string PostalCode { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits (no dashes or spaces).")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(ValidationHelpers), nameof(ValidationHelpers.ValidateDOB))]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [StringLength(100, ErrorMessage = "Occupation cannot exceed 100 characters.")]
        public string Occupation { get; set; }

        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public string CompanyName { get; set; }

        [StringLength(100, ErrorMessage = "Company address cannot exceed 100 characters.")]
        public string CompanyAddress { get; set; }

        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$", ErrorMessage = "Postal Code must be in a valid Canadian format (e.g., A1A 1A1).")]
        public string CompanyPostalCode { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Company phone number must be 10 digits (no dashes or spaces).")]
        public string CompanyPhone { get; set; }

        public int ShareholderOne { get; set; }

        public int ShareholderTwo { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Application status cannot exceed 20 characters.")]
        public string ApplicationStatus { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(ValidationHelpers), nameof(ValidationHelpers.ValidateFutureOrToday))]
        public DateTime SubmissionDate { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Membership tier cannot exceed 30 characters.")]
        public string MembershipTier { get; set; }

    }
}
