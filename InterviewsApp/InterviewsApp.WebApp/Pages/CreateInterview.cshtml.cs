using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterviewsApp.WebApp.Pages
{
    public class CreateInterviewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string CreatedPosition { get; set; }
        public void OnGet()
        {
        }
    }
}
