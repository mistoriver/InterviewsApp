using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterviewsApp.WebApp.Pages.Create
{
    public class PositionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string CreatedCompany { get; set; }
        public void OnGet()
        {
        }
    }
}
