using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers;
public class UserController : BaseApiController
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly ITokenService _tokenService;

    public UserController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
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
            Token = _tokenService.CreateToken(usuario),
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UsuarioDto>> RegisterUser(RegisterUserDto registerUserDto)
    {
        //Validate username and email
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
            Token = _tokenService.CreateToken(usuario),
            Email = usuario.Email,
            Username = usuario.UserName
        };

    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UsuarioDto>> GetUser()
    {
        //Extract email Claim
        var email = HttpContext.User?.Claims?.FirstOrDefault( x => x.Type == ClaimTypes.Email)?.Value;

        //search for a user based on email
        var usuario = await _userManager.FindByEmailAsync(email);

        return new UsuarioDto
        {
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Email = usuario.Email,
            Username = usuario.UserName,
            Token = _tokenService.CreateToken(usuario)

        };
    }



}
