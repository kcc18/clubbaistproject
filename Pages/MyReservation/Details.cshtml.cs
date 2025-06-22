using BAIS3230Project.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3230Project.Pages.MyReservation
{
    public class DetailsModel : PageModel
    {
        public TeeTime TeeTime { get; set; } = default!;
        public void OnGet(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("TeeTimeID", "TeeTimeID is required.");
            }

            if (ModelState.IsValid)
            {
                BCS RequestDirector = new();
                TeeTime = RequestDirector.FindTeeTimebyID(id);
            }

        }
    }
}
