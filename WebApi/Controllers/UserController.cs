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

    [HttpPost("register")]
    public async Task<ActionResult<UsuarioDto>> RegisterUser(RegisterUserDto registerUserDto)
    {
        var usuario = new Usuario
        {
            Email = registerUserDto.Email,
            UserName = registerUserDto.Username,
            Nombre = registerUserDto.Nombre,
            Apellido = registerUserDto.Apellido
        };

        var resultado = await _userManager.CreateAsync(usuario, registerUserDto.Password);
        
        if(!resultado.Succeeded)
        {
            return BadRequest(new CodeErrorResponse(401));
        }

        return new UsuarioDto
        {
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Token = "Este es el Token del usuario",
            Email = usuario.Email,
            Username = usuario.UserName
        };

    }
}
