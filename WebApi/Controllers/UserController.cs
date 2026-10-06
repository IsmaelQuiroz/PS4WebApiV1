using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers;
public class UserController : BaseApiController
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

    public UserController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
    {
        var usuario = await _userManager.FindByEmailAsync(loginDto.Email);

        if(usuario == null)
        {
            return Unauthorized(new CodeErrorResponse(401));
        }

        var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, loginDto.Password, false);
        if (!resultado.Succeeded)
        {
            return Unauthorized(new CodeErrorResponse(401));
        }
        return new UsuarioDto
        {
            Email = usuario.Email,
            Username = usuario.UserName,
            Token = "This is the user Token",
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido
        };
    }
}
