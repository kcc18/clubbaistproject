using System.Diagnostics;
using BAIS3230Project.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3230Project.Pages.Applications
{
    public class ApprovalModel : PageModel
    {
        [BindProperty]
        public Application Application { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            BCS requestDirector = new();
            Application = requestDirector.FindApplicationByID(id);

            if (Application == null)
            {
                return NotFound();
            }

            return Page();
        }


        // TODO:
        // Create 3 methods. One for approval, one for reject and for cancelling buttons
        // approval will create a new member in the member table and change the application status from wait-listed, on-hold etc to approve and create a member
        // reject will change the status to denied or w/e it is and not create a new member
        // cancel will just re-direct back to the page
        // we will just manually assign roles to the member/user after they have been added to the db

        public IActionResult OnPostApprove()
        {
            BCS RequestDirector = new();
            var newApplication = RequestDirector.FindApplicationByID(Application.ApplicationID);

            if (newApplication == null)
            {
                Message = "Error. Application not found.";
                return Page();
            }

            Member newMember = new Member
            {
                LastName = newApplication.LastName,
                FirstName = newApplication.FirstName,
                Address = newApplication.Address,
                PostalCode = newApplication.PostalCode,
                Phone = newApplication.Phone,
                Email = newApplication.Email,
                MembershipTier = newApplication.MembershipTier,
                City = newApplication.City,
                Province = newApplication.Province,
            };

            RequestDirector.CreateMember(newMember);

            newApplication.ApplicationStatus = "Accepted";
            RequestDirector.ModifyApplication(newApplication);

            TempData["Message"] = "Application approved successfully.";
            return RedirectToPage("./ApprovalIndex");
        }

        public IActionResult OnPostReject()
        {
            BCS RequestDirector = new();
            var application = RequestDirector.FindApplicationByID(Application.ApplicationID);

            if (application == null)
            {
                Message = "Error: Application not found.";
                return Page();
            }

            // Update the application status to Denied
            application.ApplicationStatus = "Denied";
            RequestDirector.ModifyApplication(application);

            TempData["Message"] = "Application rejected.";
            return RedirectToPage("./ApprovalIndex");
        }

    }
}
