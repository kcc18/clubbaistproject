using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;

namespace BAIS3230Project.Pages.TeeTimes
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
