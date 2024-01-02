using Bulky.DataAccess.Repository.iRepository;
using Bulky.Models.Models;
using Bulky.Models.ViewModels;
using BulkyDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulkyweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWorkcs _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWorkcs unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            
            return View(objCompanyList);
        }
        public IActionResult Upsert(int? id)
        {
			IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString(),

			});
            
            if(id == null || id == 0) 
			    return View(new Company());

            Company companyobj = _unitOfWork.Company.Get(u=>u.Id == id);
            return View(companyobj);
        }
        [HttpPost]
        public IActionResult Upsert(Company companyobj)
        {
            if (ModelState.IsValid)
            {
                string WWWRootPath = _webHostEnvironment.WebRootPath;
                
                if(companyobj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyobj);
                }
                else
                {
					_unitOfWork.Company.update(companyobj);
				}
                _unitOfWork.Save();
                TempData["success"] = "Changes have been made successfully";
                return RedirectToAction("Index");
            }
            else
            {       
                return View(companyobj);
            }
		}
        #region apicalls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json( new {data = objCompanyList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(c => c.Id == id);
            
            if(companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error Occured while Deleting" });
            }        
            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Company deleted successfully" });
        }
        #endregion

    }
}
