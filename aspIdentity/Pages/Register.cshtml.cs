using aspIdentity.Data.Model;
using aspIdentity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspIdentity.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        [BindProperty]
        public Register Model { get; set; }
        public RegisterModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = Model.Email,
                    Email = Model.Email,
                };

                var result = await userManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
