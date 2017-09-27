using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Philane.Models;

namespace Philani.Controllers
{
    public class HomeController : Controller
    {
        private PhilaniEntities db = new PhilaniEntities();

        // GET: /EmployeesAnswer/
        public ActionResult Index()
        {
            var employeesanswers = db.EmployeesAnswers.Include(e => e.Employee).Include(e => e.Question);
            return View(employeesanswers.ToList());
        }
    }
}