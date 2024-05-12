using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoMVC.Models;
using static ProyectoMVC.Models.Enum;


namespace ProyectoMVC.Controllers
{
    public class AlumnosController : Controller
    {
        private ITSTEntities db = new ITSTEntities();

        // GET: Alumnos
        public ActionResult Index()
        {
            var alumnos = db.Alumnos.Include(a => a.Semestres);
            return View(alumnos.ToList());
        }

        // GET: Alumnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumnos alumnos = db.Alumnos.Find(id);
            if (alumnos == null)
            {
                return HttpNotFound();
            }
            return View(alumnos);
        }

        // GET: Alumnos/Create
        public ActionResult Create()
        {
            ViewBag.Semestre_ID = new SelectList(db.Semestres, "ID_Semestre", "ID_Semestre");
            return View();
        }

        // POST: Alumnos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Alumno,Nombre,ApePat,ApeMat,Matricula,Curp,Edad,Email,Sexo,Foto_Url,Direccion,Activo,Turno,Semestre_ID")] Alumnos alumnos)
        {
            if (ModelState.IsValid)
            {
                db.Alumnos.Add(alumnos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Semestre_ID = new SelectList(db.Semestres, "ID_Semestre", "ID_Semestre", alumnos.Semestre_ID);
            SweetAlert("Nuevo Alumno", "Has ingresado a un Nuevo Alumno", NotificationType.success);

            return View(alumnos);
        }

        // GET: Alumnos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumnos alumnos = db.Alumnos.Find(id);
            if (alumnos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Semestre_ID = new SelectList(db.Semestres, "ID_Semestre", "ID_Semestre", alumnos.Semestre_ID);
            return View(alumnos);
        }

        // POST: Alumnos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Alumno,Nombre,ApePat,ApeMat,Matricula,Curp,Edad,Email,Sexo,Foto_Url,Direccion,Activo,Turno,Semestre_ID")] Alumnos alumnos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumnos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Semestre_ID = new SelectList(db.Semestres, "ID_Semestre", "ID_Semestre", alumnos.Semestre_ID);
            SweetAlert("Actualizado", "Has Actualizado a un Alumno", NotificationType.success);
            return View(alumnos);
        }

        // GET: Alumnos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumnos alumnos = db.Alumnos.Find(id);
            if (alumnos == null)
            {
                return HttpNotFound();
            }

            return View(alumnos);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alumnos alumnos = db.Alumnos.Find(id);
            var calificaciones = db.Calificaciones.Where(c => c.Alumno_ID == id);
            if (calificaciones != null)
            {
                // Eliminar las calificaciones
                foreach (var calificacion in calificaciones)
                {
                    db.Calificaciones.Remove(calificacion);
                }
            }

            db.SaveChanges();
            alumnos = db.Alumnos.Find(id);
            // Guardar los cambios en la base de datos
            db.Alumnos.Remove(alumnos);
            db.SaveChanges();
            SweetAlert("Eliminado", "Has Eliminado al Alumno", NotificationType.success);
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

        #region Sweet Alert
        private void SweetAlert(string title, string msg, NotificationType nt)
        {
            var script = "<script languaje='javascript'>" +
                "Swal.fire({" +
                "title: '" + title + "'," +
                "text:'" + msg + "'," +
                "icon: '" + nt + "'" +
                "});"
                + "</script>";
            TempData["alert"] = script;
        }

        private void SweetAlert_eliminar(int id)
        {
            var script = "<script languaje='javascript'>" +
                  "Swal.fire({" +
                  "title: 'Estas Seguro?',  " +
                  "text: 'Estas a punto de Eliminar el Camion " + id.ToString() + "',  " +
                  "icon: 'info'," +
                  "showDenylButton: true," +
                  "showCancelButton: true," +
                  "confirmButtonText: 'Eliminar'," +
                  "denyButtonText: 'Cancelar'" +
                  "}).then((result) => {" +
                  "if (result.isConfirmed) {  " +
                  "window.location.href='/Camiones/Eliminar_Camion/" + "id" + "';" +
                  "}else if(result.isDenied){" +
                  "Swal.fire('Se ha cancelado la operacion','','info');" +
                  "}" +
                  "});" +
                  "</script>";
            //TempData funciona como un ViewBag, pasando unfo del controlador a cualquier parte del codigo
            TempData["alert"] = script;
        }
        #endregion
    }
}
