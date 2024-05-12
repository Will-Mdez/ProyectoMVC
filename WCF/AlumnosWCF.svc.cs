using Antlr.Runtime.Misc;
using DTOProyectoMVC;
using ProyectoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Helpers;

namespace ProyectoMVC.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "AlumnosWCF" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione AlumnosWCF.svc o AlumnosWCF.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class AlumnosWCF : IAlumnosWCF
    {
        ///necesito instanciar el contexto global
        private readonly ITSTEntities _context;

        //Necesito un constructor que inicialice 
        public AlumnosWCF()
        {
            _context = new ITSTEntities();
        }


        public string EliminarAlumno(int id)
        {
            string respuesta = "";
            try
            {
                Alumnos _alumnos = (from alm in _context.Alumnos
                                    where alm.ID_Alumno == id
                                    select alm).FirstOrDefault();
                Alumnos _alumno2 = _context.Alumnos.Where(a => a.ID_Alumno == id).FirstOrDefault();

                _context.Alumnos.Remove(_alumnos);
                _context.SaveChanges();
                return respuesta = "Alumno Eliminado";

            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public AlumnosDTO GetAlumnobyID(int id)
        {
            AlumnosDTO _resultado = new AlumnosDTO();

            _resultado = (from a in _context.Alumnos
                          where a.ID_Alumno == id
                          select new AlumnosDTO
                          {
                              ID_Alumno = (int)a.ID_Alumno,
                              Nombre = a.Nombre,
                              ApePat = a.ApePat,
                              ApeMat = a.ApeMat,
                              Matricula = a.Matricula,
                              Curp = a.Curp,
                              Edad = (int)a.Edad,
                              Email = a.Email,
                              Sexo = (bool)a.Sexo,
                              Foto_Url = a.Foto_Url,
                              Direccion = a.Direccion,
                              Activo = (bool)a.Activo,
                              Turno = a.Turno,
                              Semestre_ID = (int)a.Semestre_ID
                          }).FirstOrDefault();
            return _resultado;
        }

        public List<AlumnosDTO> GetAlumnos()
        {

            List<AlumnosDTO> _resultado = new List<AlumnosDTO>();

            _resultado = (from a in _context.Alumnos
                          select new AlumnosDTO
                          {
                              ID_Alumno = (int)a.ID_Alumno,
                              Nombre = a.Nombre,
                              ApePat = a.ApePat,
                              ApeMat = a.ApeMat,
                              Matricula = a.Matricula,
                              Curp = a.Curp,
                              Edad = (int)a.Edad,
                              Email = a.Email,
                              Sexo = (bool)a.Sexo,
                              Foto_Url = a.Foto_Url,
                              Direccion = a.Direccion,
                              Activo = (bool)a.Activo,
                              Turno = a.Turno,
                              Semestre_ID = (int)a.Semestre_ID
                          }).ToList();
            return _resultado;
        }

        public string insertAlumno(
                string Nombre,
                string ApePat,
                string ApeMat,
                string Matricula,
                string Curp,
                int Edad,
                string Email,
                bool Sexo,
                string Foto_Url,
                string Direccion,
                bool Activo,
                string Turno,
                int Semestre_ID)
        {
            string respuesta = "";
            try
            {
                Alumnos _alumno = new Alumnos()
                {
                    ID_Alumno = 0,
                    Nombre = Nombre,
                    ApePat = ApePat,
                    ApeMat = ApeMat,
                    Matricula = Matricula,
                    Curp = Curp,
                    Edad = Edad,
                    Email = Email,
                    Sexo = Sexo,
                    Foto_Url = Foto_Url,
                    Direccion = Direccion,
                    Activo = Activo,
                    Turno = Turno,
                    Semestre_ID = Semestre_ID
                };

                _context.Alumnos.Add(_alumno);
                _context.SaveChanges();
                return respuesta = "Alumno insertado con exito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

       

        public string updateAlumno(int ID_Alumno, string Nombre, string ApePat, string ApeMat, string Matricula, string Curp, int Edad, string Email, bool Sexo, string Foto_Url, string Direccion, bool Activo, string Turno, int Semestre_ID)
        {
            string respuesta = "";
            try
            {
                Alumnos _alumno = new Alumnos()
                {
                    ID_Alumno = ID_Alumno,
                    Nombre = Nombre,
                    ApePat = ApePat,
                    ApeMat = ApeMat,
                    Matricula = Matricula,
                    Curp = Curp,
                    Edad = Edad,
                    Email = Email,
                    Sexo = Sexo,
                    Foto_Url = Foto_Url,
                    Direccion = Direccion,
                    Activo = Activo,
                    Turno = Turno,
                    Semestre_ID = Semestre_ID
                };

                _context.Entry(_alumno).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return respuesta = "Alimno Actualizado con exito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }
    }
}
