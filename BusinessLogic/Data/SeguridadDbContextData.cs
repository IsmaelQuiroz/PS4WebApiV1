using BusinessLogic.Data.DataServices;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data;

public class SeguridadDbContextData
{

    public static async Task SeedUserAsync(UserManager<Usuario> userManager, IOptions<userSysConfig> options)
    {       
        if (!userManager.Users.Any())
        {
            var _config = options.Value;
            var usuario = new Usuario
            {
                Nombre = _config.Nombre,
                Apellido = _config.Apellido,
                UserName = _config.Username,
                Email = _config.Email
            };
            await userManager.CreateAsync(usuario, _config.Pwd);
        }
    }



    //await userManager.CreateAsync(usuario, "Iquirozs2026$_");

}
