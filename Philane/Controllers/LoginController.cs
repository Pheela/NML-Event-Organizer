using Philane.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Philani.Controllers
{
    public class LoginController : Controller
    {
        private PhilaniEntities db = new PhilaniEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "surname")] Users user)
        {
            var username = db.Employees.FirstOrDefault(x => x.LastName.ToLower() == user.surname.ToLower());
            if (username!=null)
            {
                return RedirectToAction("Create", "EmployeesAnswer");
            }
            ViewBag.Error = "Please enter correct surname";
            return View(username);
        }

    }
}