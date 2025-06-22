using BAIS3230Project.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3230Project.Pages.MyReservation
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public TeeTime TeeTime { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            BCS requestDirector = new();
            TeeTime = requestDirector.FindTeeTimebyID(id);

            if (TeeTime == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            BCS requestDirector = new();
            bool success = requestDirector.ModifyTeeTime(TeeTime);
            Message = "Successfuly updated the tee time.";

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update Tee Time.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
