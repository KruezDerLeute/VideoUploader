using aspIdentity.Data;
using aspIdentity.Data.Model;
using aspIdentity.Migrations;
using Google.Apis.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;

namespace aspIdentity.Pages
{
    [Authorize]
    public class VideoPlayerModel : PageModel
    {
        private readonly AppDbContext context;

        public VideoPlayerModel(AppDbContext contest)
        {
            this.context = contest;
        }

        public string? id { get; set; }
        public string? youtubeLink { get; set; }

        public string Message { get; set; }
        private Video vid;
        private YoutubeLinks youtubeLinkObj;

        public IEnumerable<VideoComment> videoComments { get; set; }
        public IEnumerable<YoutubeLinkComment> youtubeLinkComments { get; set; }

        public void OnGet()
        {

        }

        [NonHandler]
        static string GetYouTubeVideoId(string url)
        {
            var uri = new Uri(url);
            var videoId = uri.Segments.Last();
            return videoId;
        }

        [NonHandler]
        public bool CheckYoutubeLinkOrNot()
        {
            bool isYoutubeLink = true;
            return isYoutubeLink;
        }

        public IActionResult OnGetDelete(string data)
        {
            string youtubeVidId = "";
            bool isYoutubeLink = false;
            try
            {
                //decoding json data
                var decodedJsonData = HttpUtility.UrlDecode(data);
                // Deserializing Json data to dotnet object
                var videoOrLinkObj = JsonSerializer.Deserialize<Data>(decodedJsonData);

                string id = videoOrLinkObj.id;
                isYoutubeLink = videoOrLinkObj.isYoutubeLink;
                if (isYoutubeLink is true)
                {
                    youtubeVidId = GetYouTubeVideoId(videoOrLinkObj.youtubeLink);
                }

                if (youtubeVidId != "")
                {
                    var deletingVid = context.Youtubes.FirstOrDefault(y => y.Id == id || y.YoutubeVidId == youtubeVidId);
                    context.Remove<YoutubeLinks>(deletingVid);
                    context.SaveChanges();
                }
                else if (youtubeVidId == "")
                {
                    var deletingVid = context.Videos.FirstOrDefault(v => v.Id == id);
                    context.Remove<Video>(deletingVid);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return RedirectToPage("/VideoCollection");
        }

        public async Task<IActionResult> OnGetLoadVideo(string data)
        {
            string youtubeLink = "";
            string youtubeVidId = "";

            var MediaAndComment = new VideoAndComments();
            try
            {
                //decoding json data
                var decodedJsonData = HttpUtility.UrlDecode(data);
                // Deserializing Json data to dotnet object
                var videoOrLinkObj = JsonSerializer.Deserialize<VideoOrLinkVideo>(decodedJsonData); 

                //assigning values of the deserialized object
                string id = videoOrLinkObj.id;
                youtubeLink = videoOrLinkObj.youtubeLink;

                this.youtubeLink = youtubeLink;
                this.id = id;

                if (youtubeLink != null)
                {
                    youtubeVidId = GetYouTubeVideoId(youtubeLink);
                }

                //checking if the data is for video or YT link or not
                //and retrieving the video based on the data's id or youtube id
                if (youtubeLink != null)
                {
                    youtubeLinkObj = await context.Youtubes.FirstOrDefaultAsync(y => y.YoutubeVidId == youtubeVidId);

                    this.youtubeLinkComments = context.YoutubeLinkComments
                        .Include(c => c.User)
                        .Where(c => c.YoutubeLinkEntityId == id);

                    var youtubeObj = new 
                    {
                        Id = youtubeLinkObj.Id,
                        LinkId = youtubeLinkObj.YoutubeVidId,
                        YTLink = youtubeLink,
                        Description = youtubeLinkObj.Description,
                        Title = youtubeLinkObj.Title,
                    };

                    var YTCommentObj = youtubeLinkComments.Select(c => new
                    {
                        Id = c.Id,
                        Data = c.Data,
                        AuthorName = c.User.UserName,
                        VideoId = c.YoutubeLinkEntityId,
                    });
                    MediaAndComment.Comments = YTCommentObj;
                    MediaAndComment.Video = youtubeObj;
                    //comment = await context.YoutubeLinkComments.Where();
                }
                else if (youtubeLink == null)
                {
                    vid = await context.Videos.FirstOrDefaultAsync(v => v.Id == id);

                    videoComments = context.VideoComments
                        .Include(c => c.User)
                        .Where(c => c.VideoId == id);
                    var newVideoObj = new object();
                    if (vid != null)
                    {
                        newVideoObj = new
                        {
                            Id = vid.Id,
                            Title = vid.Title,
                            Description = vid.Description,
                            CreatedDate = vid.CreatedDate,
                            ModifiedDate = vid.ModifiedDate,
                            FilePath = vid.FilePath,
                        };
                    }
                    

                    var newCommentObj = videoComments.Select(v => new
                    {
                        Id = v.Id,
                        Data = v.Data,
                        PublishedDate = v.PublishdedDate,
                        ModifiedDate = v.ModifiedDate,
                        CommentAuthorId = v.UserId,
                        VideoId = v.VideoId,
                        AuthorName = v.User.UserName
                    }) ; 

                    MediaAndComment.Comments = newCommentObj;
                    MediaAndComment.Video = newVideoObj;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            var jsonResult = new JsonResult(MediaAndComment);

            return jsonResult;
        }

        public IActionResult OnGetRedirectToEditVideo()
        {
            return RedirectToPage("/EditVideo");
        }

        public IActionResult OnGetRedirectToEditLink()
        {
            return RedirectToPage("/EditLink");
        }

        public async Task<IActionResult> OnGetAddComment (string commentInput, string idContainer, bool isYoutubeLink, string data)
        {
            if (isYoutubeLink != true)
            {
                using (context)
                {
                    var commentObj = new VideoComment
                    {
                        Id = Guid.NewGuid().ToString(),
                        Data = commentInput,
                        ModifiedDate = DateTime.Now,
                        PublishdedDate = DateTime.Now,
                        VideoId = idContainer,
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    };
                    await context.VideoComments.AddAsync(commentObj);
                    await context.SaveChangesAsync();
                }
            }
            else if (isYoutubeLink == true)
            {
                using (context)
                {
                    var YTcommentObj = new YoutubeLinkComment()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Data = commentInput,
                        PublishedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        YoutubeLinkEntityId = idContainer,
                    };
                    await context.YoutubeLinkComments.AddAsync(YTcommentObj);
                    await context.SaveChangesAsync();
                }
            }
            return Page();
        }
    }

    class Data
    {
        public string? id { get; set; }
        public bool isYoutubeLink { get; set; }
        public string youtubeLink { get; set; }
    }

    class VideoOrLinkVideo
    {
        public string? id { get; set; }
        public string? youtubeLink { get; set; }
    }

    class VideoAndComments
    {
        public object? Video { get; set; }
        public object? Comments { get; set; }
    }
}
