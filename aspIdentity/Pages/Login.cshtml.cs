using aspIdentity.Data.Model;
using aspIdentity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspIdentity.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> signInManager;

        public LoginModel(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty]
        public Login Model { get; set; }
        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(Model.Email, Model.Password, Model.RememberMe, false);
                if (result.Succeeded)
                {
                    ViewData["CurrentUser"] = HttpContext.User.Identity.Name;
                    if (returnUrl == null)
                    {
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                ModelState.AddModelError("", "Username or password is incorrect");
                ViewData["Message"] = "Username or password is incorrect";
            }
            return Page();
        }
    }
}
