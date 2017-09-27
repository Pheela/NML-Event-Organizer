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
    public class EmployeesAnswerController : Controller
    {
        private PhilaniEntities db = new PhilaniEntities();

        // GET: /EmployeesAnswer/
        public ActionResult Index()
        {
            var employeesanswers = db.EmployeesAnswers.Include(e => e.Employee).Include(e => e.Question);
            return View(employeesanswers.ToList());
        }
        // GET: /EmployeesAnswer/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FistName");
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Question1");
            return View();
        }
        // POST: /EmployeesAnswer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,EmployeeId,QuestionId,Yes,No")] EmployeesAnswer employeesanswer)
        {
            if (ModelState.IsValid)
            {
                employeesanswer.EmployeeId = 1;
                db.EmployeesAnswers.Add(employeesanswer);
                db.SaveChanges();
                ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Question1", employeesanswer.QuestionId);
                ViewBag.Success = "Thank you, response successfully submitted!";
                return View(employeesanswer);
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FistName", employeesanswer.EmployeeId);
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Question1", employeesanswer.QuestionId);
            return View(employeesanswer);
        }

        // GET: /EmployeesAnswer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeesAnswer employeesanswer = db.EmployeesAnswers.Find(id);
            if (employeesanswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FistName", employeesanswer.EmployeeId);
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Question1", employeesanswer.QuestionId);
            return View(employeesanswer);
        }

        // POST: /EmployeesAnswer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,EmployeeId,QuestionId,Yes,No")] EmployeesAnswer employeesanswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeesanswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FistName", employeesanswer.EmployeeId);
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Question1", employeesanswer.QuestionId);
            return View(employeesanswer);
        }
    }
}
