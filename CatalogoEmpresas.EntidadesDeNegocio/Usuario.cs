using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoEmpresas.EntidadesDeNegocio
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Rol")]
        [Required(ErrorMessage = "El Rol es obligatorio")]
        [Display(Name = "Rol")]
        public int RolId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Login es obligatorio")]
        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "El Password es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password debe estar entre 6 a 50 caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El estatus es obligatorio")]
        public byte Estatus { get; set; }

        [Display(Name = "Fecha registro")]
        public DateTime FechaRegistro { get; set; }

        //propiedad de navegación
        public Rol Rol { get; set; }

        //propiedades auxiliares
        [NotMapped]
        public int top_aux { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirmar el password")]
        [StringLength(50, ErrorMessage = "Password debe estar entre 6 a 50 caracteres", MinimumLength = 6)]
        //nueva notacion: manejar una propiedad contraseña
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password y confirmar password deben de ser iguales")]
        [Display(Name = "Confirmar password")]
        public string confirmPassword_aux { get; set; }

    }
    public enum Estatus_Usuario
    {
        ACTIVO =1,
        INACTIV0=2,
    }
}
