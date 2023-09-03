using aspIdentity.Data;
using aspIdentity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Web;

namespace aspIdentity.Pages
{
    public class EditVideoModel : PageModel
    {
        private readonly AppDbContext context;

        public EditVideoModel(AppDbContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public VideoViewModel Model { get; set; }

        public string ID { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetDoSomething(string id) 
        {
            object videoRow = new object();
            try
            {
                var decodedJsonData = HttpUtility.UrlDecode(id);
                var idObject = JsonSerializer.Deserialize<Id>(decodedJsonData);

                string DeserializedId = idObject.id;
                this.ID = DeserializedId;
                ViewData["id"] = DeserializedId;

                videoRow = await context.Videos.FirstOrDefaultAsync(v => v.Id == DeserializedId);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return new JsonResult(videoRow);
        }

        public async Task<IActionResult> OnPostSaveChanges(string ID)
        {
            try
            {
                var video = await context.Videos.FirstOrDefaultAsync(v => v.Id == ID);
                video.Title = Model.Title;
                video.Description = Model.Description;
                video.ModifiedDate = DateTime.Now;

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return RedirectToPage("/VideoCollection") ;
        }
    }

    class Id
    {
        public string? id { get; set; }
    }
}
