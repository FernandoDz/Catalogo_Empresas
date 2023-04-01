using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CatalogoEmpresas.LogicaDeNegocio;
using CatalogoEmpresas.EntidadesDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CatalogoEmpresas.InterfasGraficaMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ContactoController : Controller
    {
        // GET: ContactoController
        ContactoBL contactoBL = new ContactoBL(); //INSTANCIA DE ACCESO A DATOS/

        // GET: ContactoController
        //MUESTRA LA PAGINA PRINCIPAL DE REGISTROS
        public async Task<IActionResult> Index(Contacto pContacto = null) //no es necesario enviarlo//
        {
            if (pContacto == null)
                pContacto = new Contacto();
            if (pContacto.top_aux == 0)
                pContacto.top_aux = 10;
            else if (pContacto.top_aux == -1) //si el valor es -1 se regresa a 1//
                pContacto.top_aux = 0;
            var Contacto = await contactoBL.BuscarAsnync(pContacto);
            ViewBag.Top = pContacto.top_aux; //variable llamada top y se inserta a view bag//
            return View(Contacto);
        }

        // accion que muestra el detalle de un registro//
        ///mostrar informacion
        public async Task<IActionResult> Details(int id)
        {
            //objetos con nombre//                              //objeto sin nombre//
            var contacto = await contactoBL.OptenerporId(new Contacto { Id = id });
            return View(contacto);
        }

        //accion que muestra el formulario para agregar un contacto nuevo

        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // accion que recibe los datos del formulario para enviarlos a la base de datos//
        //guardar informacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contacto pContacto)
        {
            //manejo de excepciones//
            //si llegase a ocurrir algun error en el funcionamiento//
            try
            {
                int result = await contactoBL.CrearAsync(pContacto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pContacto);
            }
        }

        // aaccion que muestra los datos del registro cargados en el formulario para editarlos
        public async Task<IActionResult> Edit(Contacto pContacto)
        {
            var contacto = await contactoBL.OptenerporId(pContacto);
            ViewBag.Error = "";
            return View(contacto);
        }

        // acccion que recibe datos modificados
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Contacto pContacto)
        {

            try
            {

                int result = await contactoBL.ModificarAsync(pContacto);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pContacto);
            }




        }



        // accion que muestra pagina para confirmar la eliminacion
        public async Task<IActionResult> Delete(Contacto pContacto)
        {
            ViewBag.Error = "";
            var contacto = await contactoBL.OptenerporId(pContacto);
            return View();
        }

        // accion que recibe la accion de eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Contacto pContacto)
        {
            try
            {
                int result = await contactoBL.EliminarAsync(pContacto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pContacto);
            }
        }
    }
}
