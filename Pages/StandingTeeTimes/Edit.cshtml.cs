using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;

namespace BAIS3230Project.Pages.StandingTeeTimes
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public StandingTeeTime StandingTeeTime { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            BCS requestDirector = new();
            StandingTeeTime = requestDirector.FindStandingTeeTimebyID(id);

            if (StandingTeeTime == null)
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
            bool success = requestDirector.ModifyStandingTeeTime(StandingTeeTime);
            Message = "Successfuly updated the standing tee time.";

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update Standing Tee Time.");
                return Page();
            }

            return RedirectToPage("./Index");
        }

        //[BindProperty]
        //public StandingTeeTime StandingTeeTime { get; set; } = default!;

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var standingteetime = await _context.StandingTeeTime.FirstOrDefaultAsync(m => m.StandingTeeTimeID == id);
        //    if (standingteetime == null)
        //    {
        //        return NotFound();
        //    }
        //    StandingTeeTime = standingteetime;
        //    return Page();
        //}

        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more information, see https://aka.ms/RazorPagesCRUD.
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Attach(StandingTeeTime).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StandingTeeTimeExists(StandingTeeTime.StandingTeeTimeID))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("./Index");
        //}

        //private bool StandingTeeTimeExists(int id)
        //{
        //    return _context.StandingTeeTime.Any(e => e.StandingTeeTimeID == id);
        //}
    }
}
