using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;

namespace BAIS3230Project.Pages.StandingTeeTimes
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public StandingTeeTime StandingTeeTime { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BCS DeleteManager = new();
            StandingTeeTime = DeleteManager.FindStandingTeeTimebyID(id.Value);

            if (StandingTeeTime == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (StandingTeeTime?.StandingTeeTimeID == null)
            {
                return NotFound();
            }

            BCS DeleteManager = new();
            DeleteManager.RemoveStandingTeeTime(StandingTeeTime.StandingTeeTimeID);

            return RedirectToPage("./Index");
        }
    }
}
