using BulkywebRazor_temp.Data;
using BulkywebRazor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkywebRazor_temp.Pages.Categories
{
    public class SageModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category SingleCategory { get; set; }
        public SageModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
       public IActionResult OnPost() { 
        _db.Categories.Add(SingleCategory);
        _db.SaveChanges();
            return RedirectToPage("index");

       }
    }
}
