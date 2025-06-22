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
    public class IndexModel : PageModel
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

        public List<Event> eventList { get; set; }


        public void OnGet()
        {
            BCS RequestDirector = new();
            eventList = RequestDirector.FindAllEvents();
        }
    }
}
