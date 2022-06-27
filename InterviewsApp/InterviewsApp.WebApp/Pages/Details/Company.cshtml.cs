using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InterviewsApp.WebApp.Pages.Details
{
    public class CompanyModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string CompanyId { get; set; }
        public void OnGet()
        {
        }
    }
}
