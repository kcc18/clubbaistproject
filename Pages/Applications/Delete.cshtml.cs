using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;

namespace BAIS3230Project.Pages.Applications
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Application Application { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BCS DeleteManager = new();
            Application = DeleteManager.FindApplicationByID(id.Value);

            if (Application == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Application?.ApplicationID == null)
            {
                return NotFound();
            }

            BCS DeleteManager = new();
            DeleteManager.RemoveApplication(Application.ApplicationID);

            return RedirectToPage("./Index");
        }

    }
}
