using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using S1G5_SISAPP.Models;

namespace S1G5_SISAPP.Controllers
{
    public class RegistrationsController : Controller
    {
        private Entities db = new Entities();

        // GET: Registrations
        public ActionResult Index()
        {
            var registrations = db.Registrations.Include(r => r.Cours).Include(r => r.Student).Include(r => r.Term);
            return View(registrations.ToList());
        }

        // GET: Registrations/Details/5
        public ActionResult Details(int? id, int? id1, string id2)
        {
            if (id == null || id1 == null || id2.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id,id1,id2);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Course_Name");
            ViewBag.Student_ID = new SelectList(db.Students, "Student_ID", "Student_First_Name");
            ViewBag.Term_ID = new SelectList(db.Terms, "Term_ID", "Term_Name");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Student_ID,Term_ID,Course_ID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Course_Name", registration.Course_ID);
            ViewBag.Student_ID = new SelectList(db.Students, "Student_ID", "Student_First_Name", registration.Student_ID);
            ViewBag.Term_ID = new SelectList(db.Terms, "Term_ID", "Term_Name", registration.Term_ID);
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public ActionResult Edit(int? id, int? id1, string id2)
        {
            if (id == null || id1 == null || id2.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id,id1,id2);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Course_Name", registration.Course_ID);
            ViewBag.Student_ID = new SelectList(db.Students, "Student_ID", "Student_First_Name", registration.Student_ID);
            ViewBag.Term_ID = new SelectList(db.Terms, "Term_ID", "Term_Name", registration.Term_ID);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Student_ID,Term_ID,Course_ID")] Registration registration)
        {

            if (ModelState.IsValid)
            {
                //get the composite keys and the parameters
                int SID;
                int.TryParse(Request["Student_ID"],out SID);
                int TID;
                int.TryParse(Request["Term_ID"], out TID);
                string CID = (Request["Course_ID"]).ToString();
                Console.WriteLine(SID + TID + CID);
                

                //using where clause to find the record 
                var services = db.Registrations.Where(a => a.Student_ID == SID)
                                           .Where(a => a.Term_ID == TID)
                                           .Where(a => a.Course_ID == CID);

                foreach (var s in services)
                {
                    db.Registrations.Remove(s);
                }

                db.Registrations.Add(registration);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Index");
                }
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Course_Name", registration.Course_ID);
            ViewBag.Student_ID = new SelectList(db.Students, "Student_ID", "Student_First_Name", registration.Student_ID);
            ViewBag.Term_ID = new SelectList(db.Terms, "Term_ID", "Term_Name", registration.Term_ID);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(int? id, int? id1, string id2)
        {
            if (id == null || id1 == null || id2.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id,id1,id2);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? id1, string id2)
        {
            Registration registration = db.Registrations.Find(id,id1,id2);
            db.Registrations.Remove(registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
