using EPI_USE.Models;
using EPI_USE_Management_System.Models;
using EPI_USE_Web_Application.Data_Access_Layer;
using EPI_USE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EPI_USE_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //GET: EmployeeDAL
        //Creating an object of the EmployeeDAL
        public EmployeeDAL empDAL = new EmployeeDAL();

        //GET: ManagerDAL
        //Creating an object of the ManagerDAL
        public ManagerDAL manDAL = new ManagerDAL();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            var manList = new List<ManagerModel>();
            //DAL method which parses a list with all managers
            manList = manDAL.GetAllManagers().ToList();

            var empList = new List<EmployeeModel>();
            //DAL method which parses a list with all employees
            empList = empDAL.AllEmployees().ToList();

            //Creating a new list where only the employees with managers are added.
            var shortList = new List<EmployeeModel>();

            foreach (var emp in empList)
            {
                if (emp.ManagerNumber != "")
                {
                    shortList.Add(emp);
                }
            }

            //Loop and add the Parent Nodes.
            foreach (ManagerModel man in manList)
            {
                nodes.Add(new TreeViewNode { id = man.ManagerNumber.ToString(), parent = "#", text = man.ManagerName + " " + man.ManagerSurname + " - Manager" });
            }

            //Loop and add the Child Nodes.
            foreach (EmployeeModel emp in shortList)
            {
                nodes.Add(new TreeViewNode { id = emp.ManagerNumber.ToString() + "-" + emp.EmployeeNumber.ToString(), parent = emp.ManagerNumber.ToString(), text = emp.EmployeeName + " " + emp.EmployeeSurname+" - Employee" });
            }

            //Serialize to JSON string.
            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string PickedItem)
        {
            List<TreeViewNode> items = JsonConvert.DeserializeObject<List<TreeViewNode>>(PickedItem);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
