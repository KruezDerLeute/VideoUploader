using aspIdentity.Data;
using aspIdentity.Data.Model;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace aspIdentity.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        object vidObj;

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
        public string Message { get; set; }
        public async Task<IActionResult> OnGetSelectVideos()
        {
            try
            {
                var linksTask = await _context.Youtubes
                    .Include(y => y.User)
                    .ToListAsync();

                var videosTask = await _context.Videos
                    .Include(v => v.User)
                    .ToListAsync();

                var videoResult = videosTask.Select(v => new
                {
                    v.Id,
                    v.Title,
                    v.Description,
                    v.CreatedDate,
                    v.ModifiedDate,
                    v.FilePath,
                    v.User.UserName
                }).ToList();

                var linkResults = linksTask.Select(y => new
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

                var vidObj = new 
                {
                    _link = linkResults,
                    _video = videoResult,    
                };

                this.vidObj = vidObj;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            
            return new JsonResult(this.vidObj);
        }
    }
}