using EPI_USE_Web_Application.Data_Access_Layer;
using EPI_USE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EPI_USE_Web_Application.Controllers
{
    public class ManagerController : Controller
    {
        ManagerDAL manDAL = new ManagerDAL();

        //Get all Managers 
        public IActionResult Index()
        {
            List<ManagerModel> Manlist = new List<ManagerModel>();
            Manlist = manDAL.GetAllManagers().ToList();
            return View(Manlist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // return view 
            return View();
        }

        // Error view when deleting a manager due to foreign key constraint
        public IActionResult DeleteError()
        {
            // return view 
            return View();
        }

        // Error view when deleting a manager due to foreign key constraint
        public IActionResult CreationError()
        {
            // return view 
            return View();
        }

        // Create or insert new manager
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] ManagerModel ObjMan)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //DAL method to create a new manager.
                    manDAL.AddManager(ObjMan);
                }
                catch (Exception e)
                {
                     return View("CreationError");
                }

                //When manager is created successfully, the user is redirected to Manager List Page (Index Page)
                return RedirectToAction("Index");
            }
            return View(ObjMan);
        }

        // get manager by id details 
        [Route("Manager/Details/{manID}")]
        [HttpGet]
        public IActionResult Details(string manID)
        {
            if (manID == null)
            {
                return NotFound();
            }
            ManagerModel man = manDAL.GetManagerByID(manID);
            if (man == null)
            {
                return NotFound();
            }
            return View(man);  // return view 
        }

        //Edit Manager Details 
        [Route("Manager/Update/{manID}")]
        public IActionResult Update(string manID)
        {
            if (manID == null)
            {
                return NotFound();
            }
            ManagerModel man = manDAL.GetManagerByID(manID);
            if (man == null)
            {
                return NotFound();
            }
            return View(man); // return view 
        }

        //update manager 
        [Route("Manager/Update/{manID}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(string manID, [Bind] ManagerModel ObjMan2)
        {
            if (manID == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                manDAL.UpdateManagers(ObjMan2);
                return RedirectToAction("Index");
            }
            return View(manDAL);  // return view
        }

        //Get Delete View 
        [Route("Manager/Delete/{ManID}")]
        [HttpGet]
        public IActionResult Delete(string ManID)
        {
            if (ManID == null)
            {
                return NotFound();
            }
            ManagerModel man = manDAL.GetManagerByID(ManID);
            if (man == null)
            {
                return NotFound();
            }
            return View(man);  // return view
        }

        // perform the delete 
        [Route("Manager/Delete/{ManID}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCust(string ManID)
        {
            try
            {
                manDAL.DeleteManager(ManID);
            }
            catch (Exception e)
            {
                return RedirectToAction("DeleteError"); // redirects user to error page
            }
            return RedirectToAction("Index"); // redirects user to index page
        }
    }
}
