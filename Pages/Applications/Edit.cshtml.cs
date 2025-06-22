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

namespace BAIS3230Project.Pages.Applications
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Application Application { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            BCS requestDirector = new();
            Application = requestDirector.FindApplicationByID(id);

            if (Application == null)
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
            bool success = requestDirector.ModifyApplication(Application);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update application.");
                return Page();
            }

            return RedirectToPage("./Index");
        }

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var application =  await _context.Application.FirstOrDefaultAsync(m => m.ApplicationID == id);
        //    if (application == null)
        //    {
        //        return NotFound();
        //    }
        //    Application = application;
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

        //    _context.Attach(Application).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ApplicationExists(Application.ApplicationID))
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

        //private bool ApplicationExists(int id)
        //{
        //    return _context.Application.Any(e => e.ApplicationID == id);
        //}
    }
}
