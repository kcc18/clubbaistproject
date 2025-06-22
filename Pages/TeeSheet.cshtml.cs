using BAIS3230Project.Domain;
using BAIS3230Project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace BAIS3230Project.Pages
{
    public class TeeSheetModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public DateTime TeeSheetDate { get; set; } = DateTime.Today;

        [BindProperty(SupportsGet = true)]
        public string TypeFilter { get; set; }  // New filter property
        public List<TeeSheet> TeeSheetEntries { get; set; }

        public void OnGet()
        {
            BCS RequestDirector = new();
            TeeSheetEntries = RequestDirector.GetTeeSheetByDate(TeeSheetDate);

            // Filter by Type if selected
            //if (!string.IsNullOrEmpty(TypeFilter))
            //{
            //    TeeSheetEntries = TeeSheetEntries.Where(t => t.Type == TypeFilter).ToList();
            //}
        }
    }
}
