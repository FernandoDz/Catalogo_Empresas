using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CatalogoEmpresas.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace CatalogoEmpresas.AccesoADatos
{
    public class ContactoDAL
    {
        public static async Task<int> CraerAsync(Contacto pContacto) 
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pContacto);
                result=await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Contacto pcontacto)
        {
            int result = 0;
            using (var bdContexto=new BDContexto())
            {
                var contacto=await bdContexto.Contactos.FirstOrDefaultAsync(c => c.Id==pcontacto.Id);//metodo modificar
                contacto.Nombre=pcontacto.Nombre;
                contacto.Email=pcontacto.Email;
                contacto.Telefono = pcontacto.Telefono;
                contacto.Movil=pcontacto.Movil;

                bdContexto.Update(contacto);
                result = await bdContexto.SaveChangesAsync();

            }
            return result;
        }
        public static async Task<int> EliminarAsync(Contacto pContacto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var contacto = await bdContexto.Contactos.FirstOrDefaultAsync(c => c.Id == pContacto.Id);//metodo eliminar
                bdContexto.Contactos.Remove(contacto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Contacto> OptenerporId(Contacto pContacto)
        {
            var contacto = new Contacto();
            using (var bdContexto = new BDContexto())
            {
                contacto= await bdContexto.Contactos.FirstOrDefaultAsync(c => c.Id == pContacto.Id);
            }
            return contacto;
        }
        public static async Task<List<Contacto>> ObtenertodosAsync()
        {
            var contacto = new List<Contacto>();
            using(var bdContexto = new BDContexto())
            {
                contacto=await bdContexto.Contactos.ToListAsync();
            }
            return contacto;
        } 
        internal static IQueryable<Contacto>QuerySelect(IQueryable<Contacto> pQuery,Contacto pContacto)
        {
            if(pContacto.Id>0)
                pQuery=pQuery.Where(c => c.Id == pContacto.Id);

            if (!string.IsNullOrEmpty(pContacto.Nombre))
                pQuery = pQuery.Where(c => c.Nombre.Contains(pContacto.Nombre));

           

            if (!string.IsNullOrEmpty(pContacto.Telefono))
                pQuery=pQuery.Where(c => c.Telefono.Contains(pContacto.Telefono));

            if (!string.IsNullOrEmpty(pContacto.Movil))
                pQuery = pQuery.Where(c => c.Movil.Contains(pContacto.Movil));

            if (!string.IsNullOrEmpty(pContacto.Email))
                pQuery = pQuery.Where(c => c.Email.Contains(pContacto.Email));
            pQuery = pQuery.OrderByDescending(c => c.Id).AsQueryable();

            if (pContacto.top_aux > 0)
                pQuery = pQuery.Take(pContacto.top_aux).AsQueryable();
            return pQuery;

        }
        public static async Task<List<Contacto>> BuscarAsnync(Contacto pContacto)
        {
            var contacto = new List<Contacto>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Contactos.AsQueryable();
                select = QuerySelect(select, pContacto);
                contacto = await select.ToListAsync();


            }
            return contacto;
        }
    }
}
