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
    public class DetailsModel : PageModel
    {

        public TeeTime TeeTime { get; set; } = default!;
        public void OnGet(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("TeeTimeID", "TeeTimeID is required.");
            }

            if (ModelState.IsValid)
            {
                BCS RequestDirector = new();
                TeeTime = RequestDirector.FindTeeTimebyID(id);
            }

        }
    }
}
