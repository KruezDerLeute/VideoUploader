using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace aspIdentity.Data.Model
{
    public class YoutubeLinks
    {
        [NotMapped]
        private readonly AppDbContext dbContext;

        [NotMapped]
        private Uri url;

        [NotMapped]
        private NameValueCollection query;

        public YoutubeLinks(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Key]
        public string Id { get; set; }

        public string YoutubeVidId { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string? Description { get; set; }

        [Required]
        public string YoutubeLink { get; set; }

        //ForeignKey
        public string UserId { get; set; }

        //Navigation property
        public AppUser User { get; set; }

        public ICollection<YoutubeLinkComment> YTComments { get; set; }
    }
}
