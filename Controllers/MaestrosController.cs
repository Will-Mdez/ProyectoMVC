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
    public class MaestrosController : Controller
    {
        private ITSTEntities db = new ITSTEntities();

        // GET: Maestros
        public ActionResult Index()
        {
            var maestros = db.Maestros.Include(m => m.Materias);
            return View(maestros.ToList());
        }

        // GET: Maestros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maestros maestros = db.Maestros.Find(id);
            if (maestros == null)
            {
                return HttpNotFound();
            }
            return View(maestros);
        }

        // GET: Maestros/Create
        public ActionResult Create()
        {
            ViewBag.Materia_ID = new SelectList(db.Materias, "ID_Materia", "Nombre");
            return View();
        }

        // POST: Maestros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Maestro,Nombre,ApePat,ApeMat,Matricula,Curp,Edad,Email,Sexo,Foto_Url,Direccion,Materia_ID,Activo")] Maestros maestros)
        {
            if (ModelState.IsValid)
            {
                db.Maestros.Add(maestros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Materia_ID = new SelectList(db.Materias, "ID_Materia", "Nombre", maestros.Materia_ID);
            return View(maestros);
        }

        // GET: Maestros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maestros maestros = db.Maestros.Find(id);
            if (maestros == null)
            {
                return HttpNotFound();
            }
            ViewBag.Materia_ID = new SelectList(db.Materias, "ID_Materia", "Nombre", maestros.Materia_ID);
            return View(maestros);
        }

        // POST: Maestros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Maestro,Nombre,ApePat,ApeMat,Matricula,Curp,Edad,Email,Sexo,Foto_Url,Direccion,Materia_ID,Activo")] Maestros maestros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maestros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Materia_ID = new SelectList(db.Materias, "ID_Materia", "Nombre", maestros.Materia_ID);
            return View(maestros);
        }

        // GET: Maestros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maestros maestros = db.Maestros.Find(id);
            if (maestros == null)
            {
                return HttpNotFound();
            }
            return View(maestros);
        }

        // POST: Maestros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maestros maestros = db.Maestros.Find(id);
            db.Maestros.Remove(maestros);
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
