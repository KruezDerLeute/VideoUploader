using aspIdentity.Data;
using aspIdentity.Data.Model;
using aspIdentity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Web;

namespace aspIdentity.Pages
{
    public class AddLinkModel : PageModel
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment env;

        [BindProperty]
        public YoutubeLinkViewModel Model { get; set; }

        public string Message { get; set; }
        public AddLinkModel(AppDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostSaveVideo()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    static string GetYouTubeVideoId(string url)
                    {
                        var uri = new Uri(url);
                        var query = HttpUtility.ParseQueryString(uri.Query);
                        string videoId = query["v"];
                        return videoId;
                    }
                    
                    string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var NewVideoLink = new YoutubeLinks(context)
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = Model.Title,
                        YoutubeLink = Model.Url,
                        CreatedDate = DateTime.Now,
                        UserId = currentUserId,
                        YoutubeVidId = GetYouTubeVideoId(Model.Url),
                        Description = Model.Description,
                    };

                    await context.Youtubes.AddAsync(NewVideoLink);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    ViewData["Message"] = Message;
                }
            }
            return RedirectToPage("./VideoCollection");
        }
    }
}
