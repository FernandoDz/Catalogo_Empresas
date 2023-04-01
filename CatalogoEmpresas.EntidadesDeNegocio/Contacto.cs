using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CatalogoEmpresas.EntidadesDeNegocio
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El largo maximo son 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [MaxLength(100, ErrorMessage = "El largo maximo son 50 caracteres")]
        public string Email { get; set; }

        [MaxLength(15, ErrorMessage = "El largo maximo son 15 caracteres")]
        [MinLength(8, ErrorMessage = " el largo minimo son 8 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "el campo es obligatorio")]
        [MaxLength(15, ErrorMessage = "El largo maximo son 15 caracteres")]
        [MinLength(8, ErrorMessage = " el largo minimo son 8 caracteres")]
        public string Movil { get; set; }


        public List<Empresa> Empresas { get; set; }

        [NotMapped]
        public int top_aux { get; set; }//propiedad de auxiliar que sirve para
                                        //especificar el numero de
                                        //registros que queremos 
    }
    //el mio
}
