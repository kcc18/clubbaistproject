using BAIS3230Project.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3230Project.Pages.Applications
{
    public class ApprovalIndexModel : PageModel
    {

        public List<Application> applicationList { get; set; }

        public string Message { get; set; }


        public void OnGet(int id)
        {
            BCS RequestDirector = new();
            applicationList = RequestDirector.FindAllApplications();
        }

    }
}
