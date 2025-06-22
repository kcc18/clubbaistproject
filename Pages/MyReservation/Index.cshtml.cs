using BAIS3230Project.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BAIS3230Project.Pages.MyReservation
{
    public class IndexModel : PageModel
    {
        public List<TeeTime> MyReservations { get; set; } = new List<TeeTime>();
        public TeeTime teeTime { get; set; }

        public void OnGet()
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);

            BCS RequestDirector = new();

            if (!string.IsNullOrEmpty(email))
            {
                // Call the helper method in the service layer to fetch the user's reservations
                MyReservations = RequestDirector.FindTeeTimebyEmail(email);
            }
        }
    }
}
