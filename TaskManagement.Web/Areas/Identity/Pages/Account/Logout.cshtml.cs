using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagment.Domain;

namespace TaskManagement.Web.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public readonly SignInManager<UserEntity> _signInManager;
        public LogoutModel(SignInManager<UserEntity> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {   
            await _signInManager.SignOutAsync();
            return RedirectToPage("./Login");
        }
            public void OnGet()
        {
        }
    }
}
