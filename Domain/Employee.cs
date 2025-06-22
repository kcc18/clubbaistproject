using BAIS3230Project.Validation;
using System.ComponentModel.DataAnnotations;

namespace BAIS3230Project.Domain
{
    public class Employee
    {
        public int EmployeeID { get; set; } // Assume this is auto-generated or managed by DB

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        // Optional, used internally for display
        public string FullName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        [StringLength(2, ErrorMessage = "Province must be a 2-letter code (e.g., AB, ON).")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Province must be a valid 2-letter code.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid Canadian postal code format.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Job title is required.")]
        [StringLength(50, ErrorMessage = "Job title cannot exceed 50 characters.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Hire date is required.")]
        [CustomValidation(typeof(ValidationHelpers), nameof(ValidationHelpers.ValidateHireDate))]
        public DateTime HireDate { get; set; }
    }

}
