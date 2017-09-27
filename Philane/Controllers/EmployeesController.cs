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
    public class EmployeesController : Controller
    {
        private PhilaniEntities db = new PhilaniEntities();

        // GET: /Employees/
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

       
        // GET: /Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FistName,LastName,Email,DateCreated")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.DateCreated = DateTime.Now;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: /Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FistName,LastName,Email,DateCreated")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}
