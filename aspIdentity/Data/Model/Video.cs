using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspIdentity.Data.Model
{
    public class Video
    {
        [Key]
        public string? Id { get; set; }

        [Required()]
        [MaxLength(70)]
        public string? Title { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string? FilePath { get; set; }

        [NotMapped]
        [Required]
        public IFormFile? VideoFile { get; set; }

        //ForeignKey 
        public string? UserId { get; set; }

        //navigation property
        public AppUser? User { get; set; }

        public ICollection<VideoComment> Comments { get; set; }

    }
}
