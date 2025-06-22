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
    public class IndexModel : PageModel
    {

        public int StandingTeeTimeID { get; set; }
        public int MemberID { get; set; }
        public DateTime RequestedStartDate { get; set; }
        public DateTime RequestedEndDate { get; set; }
        public TimeSpan RequestedTeeTime { get; set; }
        public int PriorityNumber { get; set; }
        public TimeSpan ApprovedTeeTime { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OccurenceDate { get; set; }
        public string Status { get; set; }
        public List<StandingTeeTime> standingTeeTimeList { get; set; }


        public void OnGet()
        {
            BCS RequestDirector = new();
            standingTeeTimeList = RequestDirector.FindAllStandingTeeTimes();
        }
    }
}
