using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterviewsApp.WebApp.Pages.Details
{
    public class PositionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string PositionId { get; set; }
        public void OnGet()
        {
        }
    }
}
