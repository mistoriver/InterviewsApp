using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterviewsApp.WebApp.Pages.Create
{
    public class InterviewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string CreatedPosition { get; set; }
        public void OnGet()
        {
        }
    }
}
