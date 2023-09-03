using System.ComponentModel.DataAnnotations;

namespace aspIdentity.Data.Model
{
    public class VideoComment
    {
        [Key]
        public string Id { get; set; }
        public string? Data { get; set; }
        public DateTime PublishdedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        //ForeignKeys
        public string UserId { get; set; }

        public string VideoId { get; set; }


        //navigation properties
        public Video Video { get; set; }

        public AppUser User { get; set; }

    }
}
