using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace aspIdentity.Data.Model
{
    public class AppUser : IdentityUser
    {
        public ICollection<Video> Videos { get; set; }

        public ICollection<YoutubeLinks> YoutubeLinks { get; set; }

        public ICollection<VideoComment> Comments { get; set; }

        public ICollection<YoutubeLinkComment> YTComments { get; set; }
    }
}
