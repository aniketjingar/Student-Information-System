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
    public class TeachingAssignmentsController : Controller
    {
        private Entities db = new Entities();

        // GET: TeachingAssignments
        public ActionResult Index()
        {
            var teachingAssignments = db.TeachingAssignments.Include(t => t.Cours).Include(t => t.Instructor).Include(t => t.Term);
            return View(teachingAssignments.ToList());
        }

        // GET: TeachingAssignments/Details/5
        public ActionResult Details(int? id, string id1, int? id2)
        {
            if (id == null || id1.Equals(null) || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeachingAssignment teachingAssignment = db.TeachingAssignments.Find(id,id1,id2);
            if (teachingAssignment == null)
            {
                return HttpNotFound();
            }
            return View(teachingAssignment);
        }

        // GET: TeachingAssignments/Create
        public ActionResult Create()
        {
            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Course_Name");
            ViewBag.Instructor_ID = new SelectList(db.Instructors, "Instructor_ID", "Instructor_First_Name");
            ViewBag.Term_ID = new SelectList(db.Terms, "Term_ID", "Term_Name");
            return View();
        }

        // POST: TeachingAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Instructor_ID,Course_ID,Term_ID")] TeachingAssignment teachingAssignment)
        {
            if (ModelState.IsValid)
            {
                db.TeachingAssignments.Add(teachingAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Course_Name", teachingAssignment.Course_ID);
            ViewBag.Instructor_ID = new SelectList(db.Instructors, "Instructor_ID", "Instructor_First_Name", teachingAssignment.Instructor_ID);
            ViewBag.Term_ID = new SelectList(db.Terms, "Term_ID", "Term_Name", teachingAssignment.Term_ID);
            return View(teachingAssignment);
        }

        // GET: TeachingAssignments/Edit/5
        public ActionResult Edit(int? id, string id1, int? id2)
        {
            if (id == null || id1.Equals(null) || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeachingAssignment teachingAssignment = db.TeachingAssignments.Find(id,id1,id2);
            if (teachingAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Course_Name", teachingAssignment.Course_ID);
            ViewBag.Instructor_ID = new SelectList(db.Instructors, "Instructor_ID", "Instructor_First_Name", teachingAssignment.Instructor_ID);
            ViewBag.Term_ID = new SelectList(db.Terms, "Term_ID", "Term_Name", teachingAssignment.Term_ID);
            return View(teachingAssignment);
        }

        // POST: TeachingAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Instructor_ID,Course_ID,Term_ID")] TeachingAssignment teachingAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teachingAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Course_ID = new SelectList(db.Courses, "Course_ID", "Course_Name", teachingAssignment.Course_ID);
            ViewBag.Instructor_ID = new SelectList(db.Instructors, "Instructor_ID", "Instructor_First_Name", teachingAssignment.Instructor_ID);
            ViewBag.Term_ID = new SelectList(db.Terms, "Term_ID", "Term_Name", teachingAssignment.Term_ID);
            return View(teachingAssignment);
        }

        // GET: TeachingAssignments/Delete/5
        public ActionResult Delete(int? id, string id1, int? id2)
        {
            if (id == null || id1.Equals(null) || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeachingAssignment teachingAssignment = db.TeachingAssignments.Find(id,id1,id2);
            if (teachingAssignment == null)
            {
                return HttpNotFound();
            }
            return View(teachingAssignment);
        }

        // POST: TeachingAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, string id1, int? id2)
        {
            TeachingAssignment teachingAssignment = db.TeachingAssignments.Find(id,id1,id2);
            db.TeachingAssignments.Remove(teachingAssignment);
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
