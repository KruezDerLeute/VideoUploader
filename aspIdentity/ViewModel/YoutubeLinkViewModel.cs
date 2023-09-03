using System.ComponentModel.DataAnnotations;

namespace aspIdentity.ViewModel
{
    public class YoutubeLinkViewModel
    {
        [Required(ErrorMessage = "Та нэр оруулна уу?")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Та Youtube-ийн холбоос оруулна уу?")]
        public string? Url { get; set; }

        [MaxLength(200, ErrorMessage = "Та 200-аас доошгүй тэмдэгт оруулна уу?")]
        public string? Description { get; set; }
    }
}
