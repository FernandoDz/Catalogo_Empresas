using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoEmpresas.EntidadesDeNegocio
{
    public  class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        [MaxLength(30 , ErrorMessage ="El largo maximo es 30 caracteres")]
        public string Nombre { get; set; }

        [NotMapped]
        public int top_aux { get; set; } //propiedad auxiliar

        //propiedad de navegacion 
        public List<Usuario> Usuarios { get; set; }

    }
}
