using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatalogoEmpresas.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace CatalogoEmpresas.AccesoADatos
{
    public class EmpresaDAL
    {

        public static async Task<int>CrearAsync(Empresa pEmpresa)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pEmpresa);
                result = await bdContexto.SaveChangesAsync();

            }
            return result;
        }


        public static async Task<int> ModificarAsync(Empresa pEmpresa)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var empresa = await bdContexto.Empresas.FirstOrDefaultAsync(
                    e => e.Id == pEmpresa.Id);
                empresa.Nombre = pEmpresa.Nombre;
                empresa.Rubro = pEmpresa.Rubro;
                empresa.Categoria = pEmpresa.Categoria;
                empresa.Departamento = pEmpresa.Departamento;
                empresa.ContactoId = pEmpresa.ContactoId;

                bdContexto.Update(empresa);
                result = await bdContexto.SaveChangesAsync();


            }
            return result;

        }
        public static async Task<int> EliminarAsync(Empresa pEmpresa)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var empresa = await bdContexto. Empresas.FirstOrDefaultAsync( e => e.Id == pEmpresa.Id);//metodo eliminar
                bdContexto.Empresas.Remove(empresa);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Empresa> ObtenerPorIdAsync(Empresa pEmpresa)
        {
            var empresa = new Empresa();
            using(var bdContexto = new BDContexto())
            {
                empresa = await bdContexto.Empresas.FirstOrDefaultAsync(
                    e => e.Id == pEmpresa.Id);

            }
            return empresa;
        }
        public static async Task<List<Empresa>> ObtenerTodosAsync()
        {
            var empresa=new List<Empresa>();
            using(var bdContexto =new BDContexto())
            {
                empresa = await bdContexto.Empresas.ToListAsync();
            }
            return empresa;
        }
        internal static IQueryable<Empresa>QuerySelect( IQueryable<Empresa> pQuary, Empresa pEmpresa)
        {
            if (pEmpresa.Id > 0)
                pQuary = pQuary.Where(e => e.Id== pEmpresa.Id);

           if (!string.IsNullOrWhiteSpace(pEmpresa.Rubro))
                pQuary = pQuary.Where(e => e.Rubro == pEmpresa.Rubro);

            if (!string.IsNullOrWhiteSpace(pEmpresa.Categoria))
                pQuary = pQuary.Where(e => e.Categoria == pEmpresa.Categoria);

            if (!string.IsNullOrWhiteSpace(pEmpresa.Departamento))
                pQuary = pQuary.Where(e => e.Departamento == pEmpresa.Departamento);

            pQuary = pQuary.OrderByDescending(e => e.Id).AsQueryable();
            if (pEmpresa.top_aux>0)
                pQuary=pQuary.Take(pEmpresa.top_aux).AsQueryable();
            return pQuary;

        }
        public static async Task<List<Empresa>> BuscarAsync(Empresa pEmpresa)
        {
            var empresas = new List<Empresa>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Empresas.AsQueryable();
                select = QuerySelect(select, pEmpresa);
                empresas = await select.ToListAsync();

            }
            return empresas;

        }


        public static async Task<List<Empresa>> BuscarIncluirContactoAsync(Empresa pEmpresa)
        {
            var empresas = new List<Empresa>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Empresas.AsQueryable();
                select = QuerySelect(select, pEmpresa).Include(e => e.Contacto).AsQueryable();
                empresas = await select.ToListAsync();

            }
            return empresas;

        }


    }
}
