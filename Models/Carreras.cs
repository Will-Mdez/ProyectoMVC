//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Carreras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carreras()
        {
            this.Semestres = new HashSet<Semestres>();
        }
    
        public int ID_Carrera { get; set; }
        public string Nombre { get; set; }
        public string Modalidad { get; set; }
        public Nullable<int> Duracion_Semestres { get; set; }
        public Nullable<int> Maestria_ID { get; set; }
    
        public virtual Maestrias Maestrias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Semestres> Semestres { get; set; }
    }
}
