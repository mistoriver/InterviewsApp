using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterviewsApp.WebApp.Pages
{
    public class CreatePositionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string CreatedCompany { get; set; }
        public void OnGet()
        {
        }
    }
}
