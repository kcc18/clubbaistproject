using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Authorization;

namespace BAIS3230Project.Pages.Applications
{
    [Authorize]
    public class CreateModel : PageModel
    {
        // ApplicationStatus avaliable inputs
        // ([ApplicationStatus]='Waitlisted' OR [ApplicationStatus]='On-Hold' OR [ApplicationStatus]='Denied' OR [ApplicationStatus]='Accepted')
        [BindProperty]
        public Application Application { get; set; }
        public List<Member> MemberOptions { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
            BCS RequestDirector = new();
            MemberOptions = RequestDirector.FindAllMembers();

            if (User.Identity.IsAuthenticated)
            {
                // Get the logged-in user's email
                Application = new Application
                {
                    Email = User.FindFirstValue(ClaimTypes.Email),
                    ApplicationStatus = "On-Hold",
                    SubmissionDate = DateTime.Today // Optional default

                };
            }
        }

        public IActionResult OnPost()
        {
            BCS RequestDirector = new();
            MemberOptions = RequestDirector.FindAllMembers();

            if (ModelState.IsValid)
            {
                RequestDirector.CreateApplication(Application);


                Message = "Application created successfully";
                return RedirectToPage("./Index");
            }
            else
            {
                Message = "Error. Could not create Application.";
                return Page();
            }
        }

    }
}
