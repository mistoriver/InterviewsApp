using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterviewsApp.WebApp.Pages.Details
{
    public class InterviewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string InterviewId { get; set; }
        public void OnGet()
        {
        }
    }
}
