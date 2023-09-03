using System.ComponentModel.DataAnnotations;

namespace aspIdentity.Data.Model
{
    public class YoutubeLinkComment
    {
        [Key]
        public string? Id { get; set; }
        public string? Data { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        //ForiegnKeys
        public string? UserId { get; set; }
        public string? YoutubeLinkEntityId { get; set; }

        //Navigation properties
        public AppUser User { get; set; }
        public YoutubeLinks YoutubeLink { get; set; }
    }
}
