using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using SneakerStoreAPI.Data;
using SneakerStoreAPI.Repositories;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SneakerStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepo;

        //public APIController(IUsuarioRepository usuarioRepo)
        //{
        //    _usuarioRepo = usuarioRepo;
        //}

        //[HttpGet]
        //[Route("Usuarios/usuarios")]
        //public async Task<IActionResult> GetUsuariosAsync()
        //{
        //    var result = await _usuarioRepo.GetUsuariosAsync();
        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("usuario")]
        //public async Task<IActionResult> GetTodoUsuarioByIdAsync(int idUsuario)
        //{
        //    var result = await _usuarioRepo.GetUsuarioByIdAsync (idUsuario);
        //    return Ok(result);
        //}


        //[HttpGet]
        //[Route("usuarioscontador")]
        //public async Task<IActionResult> GetTodosAndCountAsync()
        //{
        //    var result = await _usuarioRepo.GetUsuariosEContadorAsync();
        //    return Ok(result);
        //}


        //[HttpPost]
        //[Route("criarusuario")]
        //public async Task<IActionResult> SaveAsync(Usuario novoUsuario)
        //{
        //    var result = await _usuarioRepo.SaveAsync(novoUsuario);
        //    return Ok(result);
        //}


        //[HttpPost]
        //[Route("atualizastatus")]
        //public async Task<IActionResult> UpdateTodoUsernameAsync(Usuario atualizaUsuario)
        //{
        //    var result = await _usuarioRepo.UpdateUsuarioAsync(atualizaUsuario);
        //    return Ok(result);
        //}


        //[HttpDelete]
        //[Route("deletausuario")]
        //public async Task<IActionResult> DeleteAsync(int idUsuario)
        //{
        //    var result = await _usuarioRepo.DeleteAsync(idUsuario);
        //    return Ok(result);
        //}    

    }
}
