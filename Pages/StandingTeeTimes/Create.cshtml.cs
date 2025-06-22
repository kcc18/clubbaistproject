using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;
using System.Data.Common;
using BAIS3230Project.TechnicalServices;
using Microsoft.AspNetCore.Identity;


namespace BAIS3230Project.Pages.StandingTeeTimes
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public StandingTeeTime StandingTeeTime { get; set; }
        public List<Member> MemberOptions { get; set; }
        public List<Employee> EmployeeOptions { get; set; }
        public string UserRole { get; set; }

        public string Message { get; set; }

        //public void OnGet()
        //{
        //    BCS RequestDirector = new BCS();

        //    // Fetch Members and Employees
        //    var members = RequestDirector.FindAllMembers();
        //    var employees = RequestDirector.FindAllEmployees();

        //    // Assign Members and Employees directly to ViewModel properties (no SelectListItem)
        //    MemberOptions = members;
        //    EmployeeOptions = employees;
        //}

        public void OnGet()
        {
            BCS RequestDirector = new();

            // get list of members and employees
            EmployeeOptions = RequestDirector.FindAllEmployees();
            MemberOptions = RequestDirector.FindAllMembers(); 

            // Get user role
            UserRole = User.IsInRole("Admin") ? "Admin" :
                       User.IsInRole("Clerk") ? "Clerk" :
                       User.IsInRole("ProShop") ? "ProShop" :
                       User.IsInRole("Gold") ? "Gold" :
                       User.IsInRole("Silver") ? "Silver" :
                       User.IsInRole("Bronze") ? "Bronze" : null;

            StandingTeeTime = new StandingTeeTime();

            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                var member = RequestDirector.FindMemberByEmail(email);

                if (member != null)
                {
                    StandingTeeTime.MemberID = member.MemberID;
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
            MemberOptions = RequestDirector.FindAllMembers();
            EmployeeOptions = RequestDirector.FindAllEmployees();

            // Check if the member is active
            string accountStatus = RequestDirector.FindMemberAccountStatus(StandingTeeTime.MemberID);

            Console.WriteLine(StandingTeeTime.Status);
            if (accountStatus != "Good")
            {
                // Return a validation message if the member is not active
                ModelState.AddModelError("", "Reservations can only be made by active members in good standing.");
                return Page(); // Returns the page with the error message
            }
            if (ModelState.IsValid)
            {
                //RequestDirector.CreateStandingTeeTime(StandingTeeTime);
                RequestDirector.ReoccurringStandingTeeTime(StandingTeeTime);
                Message = "Standing Tee Time created successfully";

                // Store a one-time success message in TempData
                TempData["StatusMessage"] = "Your standing tee time has been booked successfully!";


                if (User.IsInRole("Admin") || User.IsInRole("Clerk"))
                {
                    return RedirectToPage("./Index");
                }

                else if (User.IsInRole("Gold") || User.IsInRole("Silver") || User.IsInRole("Bronze"))
                {
                    return RedirectToPage("/StandingTeeTimes/Confirmation");
                }


                return RedirectToPage("./Index");           
            }
            else
            {
                Message = "Error. Could not create Standing Tee Time.";
                return Page();          
            }
        }
    }
}
