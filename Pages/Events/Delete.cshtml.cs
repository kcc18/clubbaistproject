using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BAIS3230Project.Domain;

namespace BAIS3230Project.Pages.Events
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Event EventDetail { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BCS DeleteManager = new();
            EventDetail = DeleteManager.FindEventByID(id.Value);

            if (EventDetail == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (EventDetail?.EventID == null)
            {
                return NotFound();
            }

            BCS DeleteManager = new();
            DeleteManager.RemoveEvent(EventDetail.EventID);

            return RedirectToPage("./Index");
        }
    }
}
