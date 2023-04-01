using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatalogoEmpresas.AccesoADatos;
using CatalogoEmpresas.EntidadesDeNegocio;


namespace CatalogoEmpresas.LogicaDeNegocio
{
    public class ContactoBL
    {
        public async Task<int>CrearAsync(Contacto pContacto)
        {
            return await ContactoDAL.CraerAsync(pContacto);

        }

        public async Task<int> ModificarAsync(Contacto pContacto)
        {
            return await ContactoDAL.ModificarAsync(pContacto);

        }
        public async Task<int> EliminarAsync(Contacto pContacto)
        {
            return await ContactoDAL.EliminarAsync(pContacto);

        }
        public async Task<Contacto> OptenerporId(Contacto pContacto)
        {
            return await ContactoDAL.OptenerporId(pContacto);
        }
        public async Task<List<Contacto>> ObtenerTodosAsync()
        {
            return await ContactoDAL.ObtenertodosAsync();
        }
        public async Task<List<Contacto>>BuscarAsnync(Contacto pContacto)
        {
            return await ContactoDAL.BuscarAsnync(pContacto);
        }
    }
}
