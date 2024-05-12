using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoMVC.Models;

namespace ProyectoMVC.Controllers
{
    public class CalificacionesController : Controller
    {
        private ITSTEntities db = new ITSTEntities();

        // GET: Calificaciones
        public ActionResult Index()
        {
            var calificaciones = db.Calificaciones.Include(c => c.Alumnos).Include(c => c.Materias);
            return View(calificaciones.ToList());
        }

        // GET: Calificaciones
        public ActionResult CalificacionesAlumno(int id)
        {

            var calificaciones = db.Calificaciones
                                .Where(c => c.Alumno_ID == id)
                                .Include(c => c.Alumnos)
                                .Include(c => c.Materias)
                                .ToList();

            return View(calificaciones);
        }

        // GET: Calificaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificaciones calificaciones = db.Calificaciones.Find(id);
            if (calificaciones == null)
            {
                return HttpNotFound();
            }
            return View(calificaciones);
        }

        // GET: Calificaciones/Create
        public ActionResult Create()
        {
            ViewBag.Alumno_ID = new SelectList(db.Alumnos, "ID_Alumno", "Nombre");
            ViewBag.Materia_ID = new SelectList(db.Materias, "ID_Materia", "Nombre");
            return View();
        }

        // POST: Calificaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Calificacion,Calificacion,Fecha,Alumno_ID,Materia_ID")] Calificaciones calificaciones)
        {
            if (ModelState.IsValid)
            {
                db.Calificaciones.Add(calificaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Alumno_ID = new SelectList(db.Alumnos, "ID_Alumno", "Nombre", calificaciones.Alumno_ID);
            ViewBag.Materia_ID = new SelectList(db.Materias, "ID_Materia", "Nombre", calificaciones.Materia_ID);
            return View(calificaciones);
        }

        // GET: Calificaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificaciones calificaciones = db.Calificaciones.Find(id);
            if (calificaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Alumno_ID = new SelectList(db.Alumnos, "ID_Alumno", "Nombre", calificaciones.Alumno_ID);
            ViewBag.Materia_ID = new SelectList(db.Materias, "ID_Materia", "Nombre", calificaciones.Materia_ID);
            return View(calificaciones);
        }

        // POST: Calificaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Calificacion,Calificacion,Fecha,Alumno_ID,Materia_ID")] Calificaciones calificaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calificaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Alumno_ID = new SelectList(db.Alumnos, "ID_Alumno", "Nombre", calificaciones.Alumno_ID);
            ViewBag.Materia_ID = new SelectList(db.Materias, "ID_Materia", "Nombre", calificaciones.Materia_ID);
            return View(calificaciones);
        }

        // GET: Calificaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificaciones calificaciones = db.Calificaciones.Find(id);
            if (calificaciones == null)
            {
                return HttpNotFound();
            }
            return View(calificaciones);
        }

        // POST: Calificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calificaciones calificaciones = db.Calificaciones.Find(id);
            db.Calificaciones.Remove(calificaciones);
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
