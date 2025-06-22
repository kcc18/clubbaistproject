using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BAIS3230Project.Data;
using BAIS3230Project.Domain;
using Microsoft.AspNetCore.Authorization;

namespace BAIS3230Project.Pages.Applications
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public List<Application> applicationList { get; set; }


        public void OnGet()
        {
            BCS RequestDirector = new();
            applicationList = RequestDirector.FindAllApplications();
        }
    }
}
