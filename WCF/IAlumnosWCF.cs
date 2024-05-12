using Antlr.Runtime.Misc;
using DTOProyectoMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Helpers;

namespace ProyectoMVC.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IAlumnosWCF" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IAlumnosWCF
    {
        [OperationContract]
        List<AlumnosDTO> GetAlumnos();

        [OperationContract]
        AlumnosDTO GetAlumnobyID(int id);

        [OperationContract]
        string insertAlumno(
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
                int Semestre_ID
            );

        [OperationContract]
        string updateAlumno(
            int ID_Alumno,
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
                int Semestre_ID);

        [OperationContract]
        string EliminarAlumno(int id);

    }
}
