using aspIdentity.Data;
using aspIdentity.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace aspIdentity.Pages
{
    [Authorize]
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly AppDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        IEnumerable<YoutubeLinks> _links;
        IEnumerable<Video> _videos;
        object _user;

        public string Message = "";
        public PrivacyModel(ILogger<PrivacyModel> logger, AppDbContext DbContext, UserManager<AppUser> userManager)
        {
            _logger = logger;
            dbContext = DbContext;
            this.userManager = userManager;
        }



        public void OnGet()
        {
            //string Email = HttpContext.User.Identity.Name;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetSelectVideos()
        {
            try
            {
                var CurrentUser = userManager.GetUserId(User).ToString();

                _videos = await dbContext.Videos
                    .Include(v => v.User)
                    .Where(v => v.UserId == CurrentUser)
                    .ToListAsync();

                _links = await dbContext.Youtubes
                    .Include(v => v.User)
                    .Where(y => y.UserId == CurrentUser)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            var videoResult = _videos.Select(v => new
            {
                v.Id,
                v.Title,
                v.Description,
                v.CreatedDate,
                v.ModifiedDate,
                v.FilePath,
                v.User.UserName
            }).ToList();

            var linkResults = _links.Select(y => new
            {
                y.Id,
                y.Title,
                y.Description,
                y.CreatedDate,
                y.ModifiedDate,
                y.YoutubeLink,
                y.YoutubeVidId,
                y.User.UserName
            }).ToList();

            var result = new 
            {
                Videos = videoResult,
                Links = linkResults
            };

            return new JsonResult(result);
        }

        public IActionResult OnGetAddVideo()
        {
            return Redirect("/AddVideo");
        }

        public IActionResult OnGetVideoPlayer()
        {
            return RedirectToPage("/VideoPlayer");
        }
    }
}