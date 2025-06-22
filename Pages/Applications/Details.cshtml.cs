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
    public class DetailsModel : PageModel
    {
        public Application Application { get; set; }

        public string Message { get; set; }


        public void OnGet(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("AppliationID", "ApplicationID is required.");
            }

            if (ModelState.IsValid)
            {
                BCS RequestDirector = new();
                Application = RequestDirector.FindApplicationByID(id);
            }

        }

    }
}
