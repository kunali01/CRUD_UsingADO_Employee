// Corrected EmployeeController.cs
using CRUD_UsingADO.Models;
using CrudUsingADO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_UsingADO.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration configuration;
        EmployeeCrud db;

        public EmployeeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new EmployeeCrud(this.configuration);
        }

        public IActionResult Index()
        {
            var response = db.GetEmployees();
            return View(response);
        }

        public IActionResult Details(int id)
        {
            var res = db.GetEmployeeById(id);
            return View(); // Retain this or modify as per your requirement
        }

        // Renamed to avoid conflict
        public ActionResult ViewDetails(int id)
        {
            var employee = db.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public ActionResult Create(Employee emp)
        {
            try
            {
                int response = db.AddEmployee(emp);
                if (response >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMsg = "Something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var employee = db.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            var result = db.UpdateEmployee(emp);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ErrorMsg = "Something went wrong.";
            return View(emp);
        }

        public ActionResult Delete(int id)
        {
            var employee = db.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = db.DeleteEmployee(id);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ErrorMsg = "Something went wrong.";
            return View();
        }
    }
}
