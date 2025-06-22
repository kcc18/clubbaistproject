using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;

namespace BAIS3230Project.Pages.Events
{
    public class CreateModel : PageModel
    {

        [BindProperty]
        public Event newEvent { get; set; }          
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                BCS RequestDirector = new();
                RequestDirector.CreateEvent(newEvent);
                Message = "Program created successfully";
                return RedirectToPage("./Index");              }
            else
            {
                Message = "Error. Could not create program.";
                return Page();             
            }
        }

    }
}
