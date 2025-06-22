//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using BAIS3230Project.Data;
//using BAIS3230Project.Domain;

//namespace BAIS3230Project.Pages.Events
//{
//    public class EditModel : PageModel
//    {

//        public async Task<IActionResult> OnGetAsync(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var eventRename = await _context.Event.FirstOrDefaultAsync(m => m.EventID == id);
//            if (eventRename == null)
//            {
//                return NotFound();
//            }
//            Event = eventRename;
//            return Page();
//        }

//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more information, see https://aka.ms/RazorPagesCRUD.
//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            _context.Attach(Event).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!EventExists(Event.EventID))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return RedirectToPage("./Index");
//        }

//        private bool EventExists(int id)
//        {
//            return _context.Event.Any(e => e.EventID == id);
//        }
//    }
//}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BAIS3230Project.Domain;

namespace BAIS3230Project.Pages.Events
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Event EventDetail { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            BCS requestDirector = new();
            EventDetail = requestDirector.FindEventByID(id);

            if (EventDetail == null)
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
            bool success = requestDirector.ModifyEvent(EventDetail);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update event.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
