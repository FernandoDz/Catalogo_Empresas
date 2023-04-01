using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//namespace

using CatalogoEmpresas.EntidadesDeNegocio;
using CatalogoEmpresas.LogicaDeNegocio;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CatalogoEmpresas.InterfasGraficaMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UsuarioController : Controller
    {
        UsuarioBL usuarioBL = new UsuarioBL(); //instancia de acceso a la clase usuario
        RolBL rolBL = new RolBL();//instancia de acceso a la clase RolBL
        // GET: UsuarioController
        public async Task<IActionResult> Index(Usuario pUsuario = null)
        {
            if (pUsuario == null)
                pUsuario = new Usuario();
            if (pUsuario.top_aux == 0)
                pUsuario.top_aux = 10;
            else if (pUsuario.top_aux == -1)
                pUsuario.top_aux = 0;
            var usuarios = await usuarioBL.BuscarIncluirRolesAsync(pUsuario); ;
            ViewBag.Top = pUsuario.top_aux;
            ViewBag.Roles = await rolBL.ObtenerTodosAsync();
            return View(usuarios);
        }

        // accion que muestra un registro existente
        public async Task<IActionResult> Details(int id)
        {
            var usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = id });
            usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.RolId });
            return View(usuario);
        }

        // accion que muestra el formulario para crear un nuevo usuario
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await rolBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // accion que recibe los datos del formulario para enviarlos a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario pUsuario)
        {
            try
            {
                int result = await usuarioBL.CrearAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await rolBL.ObtenerTodosAsync();
                return View(pUsuario);
            }
        }

        // accion que muestra el formulario con los datos cargados  para editar 
        public async Task<IActionResult> Edit(Usuario pUsuario)
        {
          
            var taskObtenerTodosRoles = rolBL.ObtenerTodosAsync();
            var usuario = await usuarioBL.ObtenerPorIdAsync(pUsuario);
            ViewBag.Roles = await usuarioBL.ObtenerPorIdAsync(pUsuario);
            ViewBag.Error = "";
            return View(usuario);
        }

        //accion que recibe los datos modificados para enviarlos a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario pUsuario)
        {
            try
            {
                int result = await usuarioBL.ModificarAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await rolBL.ObtenerTodosAsync();
                return View(pUsuario);
            }
        }

        // opcion que muestra los datos del registro para confirmar los datos de eliminacion 
        public async Task<IActionResult> Delete(Usuario pUsuario)
        {
            var usuario = await usuarioBL.ObtenerPorIdAsync(pUsuario);
            usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.RolId });
            ViewBag.Error = "";
            return View(usuario);
        }

        // accion que recibe la confirmacion oara eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Usuario pUsuario)
        {
            try
            {
                int result = await usuarioBL.EliminarAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var usuario = await usuarioBL.ObtenerPorIdAsync(pUsuario);
                if (usuario == null)
                    usuario = new Usuario();
                if (usuario.Id > 0)
                    usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.RolId });
                return View(usuario);
            }
        }
        //accion que muestra la pagina de inicio de sesion 
        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = ReturnUrl;
            ViewBag.Error = "";
            return View();
        }

        // accion que recibe los datos de usuario para iniciar sesion
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuario pUsuario, string pReturnUrl = null)
        {
            try
            {
                var usuario = await usuarioBL.LoginAsync(pUsuario);
                if (usuario != null && usuario.Id > 0 && pUsuario.Login == usuario.Login)
                {
                    usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.RolId });
                     var claims = new[] { new Claim(ClaimTypes.Name, usuario.Login), new Claim(ClaimTypes.Role, usuario.Rol.Nombre ) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
                else
                    throw new Exception("Credenciales incorrectas");
                if (!string.IsNullOrWhiteSpace(pReturnUrl))
                    return Redirect(pReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Url = pReturnUrl;
                ViewBag.Error = ex.Message;
                return View(new Usuario { Login = pUsuario.Login });
            }
        }
        //accion que ejecuta el proceso de cierre de sesion 
        [AllowAnonymous]
        public async Task<IActionResult> CerrarSesion(string ReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuario");
        }
        // accion que muestra el formulario para cambiar la contraseña
        public async Task<IActionResult> CambiarPassword()
        {

            var usuarios = await usuarioBL.BuscarAsync(new Usuario { Login = User.Identity.Name, top_aux = 1 });
            var usuarioActual = usuarios.FirstOrDefault();
            ViewBag.Error = "";
            return View(usuarioActual);
        }

        // accion que recibe los cambios en la contraseña paara registrarlos en la bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarPassword(Usuario pUsuario, string pPasswordAnt)
        {
            try
            {
                int result = await usuarioBL.CambiarPasswordAsync(pUsuario, pPasswordAnt);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Usuario");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var usuarios = await usuarioBL.BuscarAsync(new Usuario { Login = User.Identity.Name, top_aux = 1 });
                var usuarioActual = usuarios.FirstOrDefault();
                return View(usuarioActual);
            }
        }
    }
}
