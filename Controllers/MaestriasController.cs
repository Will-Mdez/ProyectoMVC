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
    public class MaestriasController : Controller
    {
        private ITSTEntities db = new ITSTEntities();

        // GET: Maestrias
        public ActionResult Index()
        {
            return View(db.Maestrias.ToList());
        }

        // GET: Maestrias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maestrias maestrias = db.Maestrias.Find(id);
            if (maestrias == null)
            {
                return HttpNotFound();
            }
            return View(maestrias);
        }

        // GET: Maestrias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maestrias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Maestria,Nombre,Modalidad,Duracion_Semestres")] Maestrias maestrias)
        {
            if (ModelState.IsValid)
            {
                db.Maestrias.Add(maestrias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maestrias);
        }

        // GET: Maestrias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maestrias maestrias = db.Maestrias.Find(id);
            if (maestrias == null)
            {
                return HttpNotFound();
            }
            return View(maestrias);
        }

        // POST: Maestrias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Maestria,Nombre,Modalidad,Duracion_Semestres")] Maestrias maestrias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maestrias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maestrias);
        }

        // GET: Maestrias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maestrias maestrias = db.Maestrias.Find(id);
            if (maestrias == null)
            {
                return HttpNotFound();
            }
            return View(maestrias);
        }

        // POST: Maestrias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maestrias maestrias = db.Maestrias.Find(id);
            db.Maestrias.Remove(maestrias);
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
