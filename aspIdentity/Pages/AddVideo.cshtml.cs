using aspIdentity.Data;
using aspIdentity.Data.Model;
using aspIdentity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace aspIdentity.Pages
{
    public class AddVideoModel : PageModel
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment web;
        public AddVideoModel(AppDbContext context, IWebHostEnvironment web)
        {
            this.context = context;
            this.web = web;
        }

        [BindProperty]
        public VideoViewModel Model { get; set; }
        public string Message { get; set; }

        [HttpPost]
        public async Task<IActionResult> OnPostSaveVideo()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (context)
                    {
                        string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        string UniqueFileName = Guid.NewGuid().ToString() + "_" + Model.VideoFile.FileName;
                        string VideoFolderPath = Path.Combine(web.WebRootPath, "Videos");
                        string filePath = Path.Combine(VideoFolderPath, UniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Model.VideoFile.CopyToAsync(stream);
                            var NewVideo = new Video
                            {
                                Id = Guid.NewGuid().ToString(),
                                Description = Model.Description,
                                FilePath = Path.Combine("Videos", UniqueFileName),
                                CreatedDate = DateTime.Now,
                                Title = Model.Title,
                                UserId = currentUserId,
                            };

                            await context.Videos.AddAsync(NewVideo);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception ex) 
                {
                    Message = ex.Message;
                    ViewData["Message"] = Message;
                }
            }
            else
            {
                return Page();
            }
            return RedirectToPage("./VideoCollection");
        }
    }
}
