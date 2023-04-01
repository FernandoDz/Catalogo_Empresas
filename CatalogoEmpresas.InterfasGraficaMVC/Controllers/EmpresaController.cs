using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CatalogoEmpresas.EntidadesDeNegocio;
using CatalogoEmpresas.LogicaDeNegocio;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace CatalogoEmpresas.InterfasGraficaMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class EmpresaController : Controller
    {
        EmpresasBl empresasBl = new EmpresasBl();
        ContactoBL ContactoBL = new ContactoBL();
        // GET: EmpresaController
        public async Task<IActionResult> Index(Empresa pEmpresa= null)
        {
            if (pEmpresa == null)
                pEmpresa=new Empresa();
            if (pEmpresa.top_aux == 0)
                pEmpresa.top_aux = 10;
            else if (pEmpresa.top_aux == 1)
                pEmpresa.top_aux = 0;

            var empresas = await empresasBl.BuscarIncluirContactoAsync(pEmpresa);
            ViewBag.Contactos=await ContactoBL.ObtenerTodosAsync();
            return View();
        }

        // Accion que muetra los detalles de un registro
        public async Task<IActionResult> Details(int id)
        {
            var empresa = await empresasBl.ObtenerPorIdAsync(new Empresa { Id = id });
            empresa.Contacto = await ContactoBL.OptenerporId(new Contacto { Id = empresa.Contacto.Id });

            return View();
        }

        // GET: EmpresaController/Create
        public async Task <IActionResult> Create()
        {
            ViewBag.Contactos = await ContactoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // accion que recibe los datos del formulario y los envia a la bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(Empresa pEmpresa)
        {
            try
            {
                int result = await empresasBl.CrearAsync(pEmpresa);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Contactos = await ContactoBL.ObtenerTodosAsync();
                return View(pEmpresa);
            }
        }

        // accion que muestra los datos del registri cargados en el formulario
        public async Task<IActionResult> Edit(Empresa pEmpresa)
        {
            var empresa = await empresasBl.ObtenerPorIdAsync(pEmpresa);
            ViewBag.Contacto = await ContactoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View(empresa);
        }

        // accion que recibe los datos modificados para enviarlos a la base de datos 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Edit(int id, Empresa pEmpresa)
        {
            try
            {
                int result = await empresasBl.ModificarAsync(pEmpresa);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Contactos = await ContactoBL.ObtenerTodosAsync();
                return View(pEmpresa);
            }
        }

        // GET: EmpresaController/Delete/5
        public async Task<IActionResult> Delete(Empresa pEmpresa)
        {
            var empresa = await empresasBl.ObtenerPorIdAsync(pEmpresa);
            empresa.Contacto = await ContactoBL.OptenerporId(new Contacto { Id=pEmpresa.ContactoId});
            return View(empresa);
        }

        // POST: EmpresaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(int id, Empresa pEmpresa)
        {
            try
            {
                int result = await empresasBl.EliminarAsync(pEmpresa);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error=ex.Message;
                var empresa = await empresasBl.ObtenerPorIdAsync(pEmpresa);
                if (empresa == null)
                    empresa = new Empresa();
                if(empresa.Id>0) 
                    empresa.Contacto=await ContactoBL.OptenerporId(new Contacto{ Id=pEmpresa.ContactoId});  

                return View(empresa);
            }
        }
    }
}
