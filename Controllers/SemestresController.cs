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
    public class SemestresController : Controller
    {
        private ITSTEntities db = new ITSTEntities();

        // GET: Semestres
        public ActionResult Index()
        {
            var semestres = db.Semestres.Include(s => s.Carreras);
            return View(semestres.ToList());
        }

        // GET: Semestres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semestres semestres = db.Semestres.Find(id);
            if (semestres == null)
            {
                return HttpNotFound();
            }
            return View(semestres);
        }

        // GET: Semestres/Create
        public ActionResult Create()
        {
            ViewBag.Carrera_ID = new SelectList(db.Carreras, "ID_Carrera", "Nombre");
            return View();
        }

        // POST: Semestres/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Semestre,Numero,Carrera_ID,FechaI,FechaF")] Semestres semestres)
        {
            if (ModelState.IsValid)
            {
                db.Semestres.Add(semestres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Carrera_ID = new SelectList(db.Carreras, "ID_Carrera", "Nombre", semestres.Carrera_ID);
            return View(semestres);
        }

        // GET: Semestres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semestres semestres = db.Semestres.Find(id);
            if (semestres == null)
            {
                return HttpNotFound();
            }
            ViewBag.Carrera_ID = new SelectList(db.Carreras, "ID_Carrera", "Nombre", semestres.Carrera_ID);
            return View(semestres);
        }

        // POST: Semestres/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Semestre,Numero,Carrera_ID,FechaI,FechaF")] Semestres semestres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semestres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Carrera_ID = new SelectList(db.Carreras, "ID_Carrera", "Nombre", semestres.Carrera_ID);
            return View(semestres);
        }

        // GET: Semestres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semestres semestres = db.Semestres.Find(id);
            if (semestres == null)
            {
                return HttpNotFound();
            }
            return View(semestres);
        }

        // POST: Semestres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Semestres semestres = db.Semestres.Find(id);
            db.Semestres.Remove(semestres);
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
