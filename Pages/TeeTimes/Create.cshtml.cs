using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;
using System.Diagnostics.Metrics;
using System.Data;
using BAIS3230Project.TechnicalServices;

namespace BAIS3230Project.Pages.TeeTimes
{
    public class CreateModel : PageModel
    {

        [BindProperty]
        public TeeTime newTeeTime { get; set; }          
        public List<Member> MemberOptions { get; set; }
        public List<Employee> EmployeeOptions { get; set; }

        public string Message { get; set; }


        // for user role based tee time selection 
        public string UserRole { get; set; }
        public List<TimeSpan> AllowedTimes { get; set; }

        public void OnGet()
        {
            BCS RequestDirector = new();

            // Get user role
            UserRole = User.IsInRole("Admin") ? "Admin" :
                       User.IsInRole("Clerk") ? "Clerk" :
                       User.IsInRole("ProShop") ? "ProShop" :
                       User.IsInRole("Gold") ? "Gold" :
                       User.IsInRole("Silver") ? "Silver" :
                       User.IsInRole("Bronze") ? "Bronze" : null;

            newTeeTime = new TeeTime();

            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                var member = RequestDirector.FindMemberByEmail(email);

                if (member != null)
                {
                    newTeeTime.MemberID = member.MemberID;
                }
                else if (UserRole == "Admin" || UserRole == "Clerk")
                {
                    // Admins get to choose from the full list of members
                    MemberOptions = RequestDirector.FindAllMembers();
                }
                else
                {
                    // Handle the error case: user has no associated Member
                    Message = "No member profile found for your account. Please contact support.";
                }
            }
        }

        public IActionResult OnPost()
        {
            BCS RequestDirector = new();
            // Check if the member is active
            string accountStatus = RequestDirector.FindMemberAccountStatus(newTeeTime.MemberID);

            if (accountStatus != "Good")
            {
                // Return a validation message if the member is not active
                ModelState.AddModelError("", "Reservations can only be made by active members in good standing.");
                return Page(); // Returns the page with the error message
            }

            // Check if the date is blocked by an event
            bool isDateBlocked = RequestDirector.IsDateBlockedByEvent(newTeeTime.TeeDate);
            if (isDateBlocked)
            {
                // Add an error to the ModelState
                ModelState.AddModelError("", "Tee times are not available on this date due to a scheduled event.");

                // Return the same page so the error is shown
                return Page();
            }

            if (ModelState.IsValid)
            {
                //BCS RequestDirector = new();
                RequestDirector.CreateTeeTime(newTeeTime);
                Message = "Tee Time created successfully";

                // Store a one-time success message in TempData
                TempData["StatusMessage"] = "Your tee time has been booked successfully!";

                if (User.IsInRole("Admin") || User.IsInRole("Clerk"))
                {
                    return RedirectToPage("./Index");
                }

                else if (User.IsInRole("Gold") || User.IsInRole("Silver") || User.IsInRole("Bronze"))
                {
                    return RedirectToPage("/MyReservation/Index");
                }

                return RedirectToPage("./Index");

                //return Page();             
            }
            else
            {
                Message = "Error. Could not create Tee Time.";
                return Page();             
            }
        }

        public JsonResult OnGetAvailableTimes(DateTime date)
        {
            BCS RequestDirector = new();
            // Determine the role of the current user
            var role = User.IsInRole("Admin") ? "Admin" :
                       User.IsInRole("Clerk") ? "Clerk" :
                       User.IsInRole("ProShop") ? "ProShop" :
                       User.IsInRole("Gold") ? "Gold" :
                       User.IsInRole("Silver") ? "Silver" :
                       User.IsInRole("Bronze") ? "Bronze" : "Guest";

            var times = BCS.GetAllowedTimesForDay(date, role);

            return new JsonResult(times.Select(t => t.ToString(@"hh\:mm")));
        }
    }
}
