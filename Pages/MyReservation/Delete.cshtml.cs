using BAIS3230Project.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3230Project.Pages.MyReservation
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public TeeTime TeeTime { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BCS DeleteManager = new();
            TeeTime = DeleteManager.FindTeeTimebyID(id.Value);

            if (TeeTime == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (TeeTime?.TeeTimeID == null)
            {
                return NotFound();
            }

            BCS DeleteManager = new();
            DeleteManager.RemoveTeeTime(TeeTime.TeeTimeID);

            return RedirectToPage("./Index");
        }
    }
}
