using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Philane.Models;
using System.Net.Mail;
using System.IO;

namespace Philani.Controllers
{
    public class QuestionsController : Controller
    {
        private PhilaniEntities db = new PhilaniEntities();

        // GET: /Questions/
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        // GET: /Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Question1")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: /Questions/Edit/5
        public ActionResult Email(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            List<Employee> employees = db.Employees.Where(x=>x.Email!=null).ToList();

            foreach (var employee in employees)
            {
                string to = employee.Email;
                string name = employee.FistName;
                string from = "ziphiwop@gmail.com";
                string subject ="Please complete this questionnaire "+question.Question1;
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/email.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", employee.LastName);
                body = body.Replace("{FirstName}", employee.FistName);
                
                try
                {
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(from, "z!phiwo2")
                    };
                    using (var messages = new MailMessage(from, to)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml=true
                    })
                    {
                        smtp.Send(messages);
                    }
                    ViewBag.Message = "Email sent succesful";
                    return RedirectToAction("Index", db.Questions.ToList());
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error sending email";
                }
            }
            return RedirectToAction("Index", db.Questions.ToList());
        }

        // GET: /Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: /Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Question1")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }
    }
}
