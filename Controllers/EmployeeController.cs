using EPI_USE_Web_Application.Data_Access_Layer;
using EPI_USE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EPI_USE_Web_Application.Controllers
{
    public class EmployeeController : Controller
    {
        // Creation of employee data access layer object
        EmployeeDAL empDAL = new EmployeeDAL();

        //Get all Employees
        public ViewResult Index(string SearchTerm)
        {
            var empList = new List<EmployeeModel>();
            //Calling the DAL method which parses a list with all employees
            empList = empDAL.AllEmployees().ToList();

            //Declaration for filter.
            ViewData["CurrentFilter"] = SearchTerm;

            //If the searchString contains a value 
            if (!String.IsNullOrEmpty(SearchTerm))
            {
                empList = empList.Where(e => e.EmployeeNumber.Contains(SearchTerm) ||
                e.EmployeeSalary.ToString().Contains(SearchTerm) || e.EmployeePosition.Contains(SearchTerm) || e.BirthDate.Equals(SearchTerm) ||
                e.EmployeeSurname.Contains(SearchTerm) || e.EmployeeName.Contains(SearchTerm)).ToList();
            }

            //Return the list of employees
            return View(empList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // return view 
            return View();
        }

        // Error handling when creating employee
        public IActionResult CreateError()
        {
            // return view 
            return View();
        }

        // Error handling when updating employee
        public IActionResult UpdateError()
        {
            // return view 
            return View();
        }

        // Create or insert new employee 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] EmployeeModel ObjEmp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //DAL method to create a new employee.
                    empDAL.AddEmployee(ObjEmp);
                }
                catch (Exception e)
                {
                    return RedirectToAction("CreateError");
                }

                //When employee is created successfully, the useris redirected to Employee List Page (Index Page)
                return RedirectToAction("Index");
            }
            return View(ObjEmp);
        }

        // get employee by id details 
        [Route("Employee/Details/{empID}")]
        [HttpGet]
        public IActionResult Details(string empID)
        {
            if (empID == null)
            {
                return NotFound();
            }
            EmployeeModel emp = empDAL.GetEmployeeByID(empID);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);  // return view 
        }

        //Edit Employee Details 
        [Route("Employee/Update/{empID}")]
        public IActionResult Update(string empID)
        {
            if (empID == null)
            {
                return NotFound();
            }
            EmployeeModel emp = empDAL.GetEmployeeByID(empID);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp); // return view 
        }

        //update employee 
        [Route("Employee/Update/{empID}")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(string empID, [Bind] EmployeeModel ObjEmp2)
        {
            if (empID == null)
            {
                return NotFound();
            }

            // Checks if model requirements are met
            if (ModelState.IsValid)
            {
                try
                {
                    empDAL.UpdateEmployees(ObjEmp2);
                }
                catch (Exception e)
                {
                    return RedirectToAction("UpdateError");
                }
                return RedirectToAction("Index");
            }
            return View(empDAL);
        }

        //Get Delete View 
        [Route("Employee/Delete/{EmpID}")]
        [HttpGet]
        public IActionResult Delete(string EmpID)
        {
            if (EmpID == null)
            {
                return NotFound();
            }
            EmployeeModel emp = empDAL.GetEmployeeByID(EmpID);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);  // return view 
        }

        // perform the delete 
        [Route("Employee/Delete/{EmpID}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCust(string EmpID)
        {
            empDAL.DeleteEmployee(EmpID);
            return RedirectToAction("Index"); // redirects user to index page
        }
    }
}
