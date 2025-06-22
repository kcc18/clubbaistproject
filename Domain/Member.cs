using BAIS3230Project.Validation;
using System.ComponentModel.DataAnnotations;

namespace BAIS3230Project.Domain
{
    public class Member
    {
        public int MemberID { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        // Usually filled programmatically, no validation needed
        public string FullName { get; set; }

        [Required, StringLength(100)]
        public string Address { get; set; }

        [Required, RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid Canadian postal code format.")]
        public string PostalCode { get; set; }

        [Required, Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20, MinimumLength = 10)]
        public string Phone { get; set; }

        [Required, EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(Gold|Silver|Bronze)$", ErrorMessage = "Membership tier must be Gold, Silver, or Bronze.")]
        public string MembershipTier { get; set; }


        [Required, StringLength(50)]
        public string City { get; set; }

        [Required, StringLength(2, ErrorMessage = "Use the 2-letter province code (e.g., AB, BC).")]
        public string Province { get; set; }


        // ([AccountStatus]='Suspended' OR [AccountStatus]='Inactive' OR [AccountStatus]='Good')
        [Required, StringLength(20)]
        public string AccountStatus { get; set; }

        public bool IsShareholder { get; set; }

        [CustomValidation(typeof(ValidationHelpers), nameof(ValidationHelpers.ValidateFutureOrToday))]
        public DateTime MembershipStartDate { get; set; }
    }
}
