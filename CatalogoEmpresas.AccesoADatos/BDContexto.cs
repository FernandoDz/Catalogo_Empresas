using CatalogoEmpresas.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoEmpresas.AccesoADatos
{
    public class BDContexto:DbContext
    {

        public DbSet<Contacto> Contactos{ get; set; }
        public DbSet<Empresa>Empresas{ get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data source = DESKTOP-5D189V7;
                   Initial Catalog = CatalogoEmpresasBD;
                  Integrated Security =True");
        }
    }
}
