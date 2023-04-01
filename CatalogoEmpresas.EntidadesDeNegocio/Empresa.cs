using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoEmpresas.EntidadesDeNegocio
{
    public class Empresa
    {
        [Key]
    
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        [MaxLength(10,ErrorMessage ="El maximo de caracteres es de 10 caracteres  ")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Rubro es requerido")]
        [MaxLength(50, ErrorMessage = "El maximo de caracteres es de 50 caracteres  ")]
        public string Rubro { get; set; }

        [Required(ErrorMessage = "las categorias  es requerido")]
        [MaxLength(25, ErrorMessage = "El maximo de caracteres es de 25 caracteres  ")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        [MaxLength(25, ErrorMessage = "El maximo de caracteres es de 25 caracteres  ")]
        public string Departamento { get; set; }

        [Required(ErrorMessage ="el ContactoId es requerido")]
        [ForeignKey ("ContactoId")]
        [Display(Name ="Id del Contacto")]
        public int ContactoId { get; set; }


        public Contacto Contacto { get; set; }

        [NotMapped]
        public int top_aux { get; set; }

    }
}
