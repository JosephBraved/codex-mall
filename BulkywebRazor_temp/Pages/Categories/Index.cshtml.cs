using BulkywebRazor_temp.Data;
using BulkywebRazor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkywebRazor_temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList;
        public IndexModel(ApplicationDbContext db) {
        _db = db;
        }
        public void OnGet()
        {
             CategoryList = _db.Categories.ToList();
        }
    }
}
