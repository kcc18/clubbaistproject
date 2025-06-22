using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;

namespace BAIS3230Project.Pages.Events
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public int EventID { get; set; }

        [BindProperty]
        public string EventName { get; set; }

        [BindProperty]
        public DateOnly EventDate { get; set; }

        [BindProperty]
        public TimeSpan StartTime { get; set; }

        [BindProperty]
        public TimeSpan EndTime { get; set; }

        [BindProperty]
        public string EventType { get; set; }

        public Event EventDetail { get; set; }

        public string Message { get; set; }


        public void OnGet(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("EventID", "EventID is required.");
            }

            if (ModelState.IsValid)
            {
                BCS RequestDirector = new();
                EventDetail = RequestDirector.FindEventByID(id);
            }

        }
        public void OnPost() 
        {
        }

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var eventRename = await _context.Event.FirstOrDefaultAsync(m => m.EventID == id);
        //    if (eventRename == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        Event = eventRename;
        //    }
        //    return Page();
        //}
    }
}
