using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace aspIdentity.ViewModel
{
    public class VideoViewModel
    {
        public string id { get; set; }

        [Required(ErrorMessage = "Та бичлэгэнд нэр өгнө үү?")]
        [MaxLength(250, ErrorMessage = "Гарчигний тэмдэгтүүдийн урт 70-аас доошгүй байна")]
        public string? Title { get; set; }

        [MaxLength(250, ErrorMessage = "Тайлбарны тэмдэгтүүдийн урт 250-аас доошгүй байна")]
        public string? Description { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Файл сонгоно уу")]
        public IFormFile? VideoFile { get; set; }
    }
}
