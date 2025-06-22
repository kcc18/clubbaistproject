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
    public class IndexModel : PageModel
    {
        public int TeeTimeID { get; set; }
        public DateTime TeeDate { get; set; }
        public TimeSpan ScheduledTeeTime { get; set; }
        public int MemberID { get; set; }
        public int NumberOfPlayers { get; set; }
        public string Phone { get; set; }
        public int NumberOfCarts { get; set; }
        public int EmployeeID { get; set; }

        public List<TeeTime> teeTimeList { get; set; }


        public void OnGet()
        {
            BCS RequestDirector = new();
            teeTimeList = RequestDirector.FindAllTeeTimes();
        }
    }
}
