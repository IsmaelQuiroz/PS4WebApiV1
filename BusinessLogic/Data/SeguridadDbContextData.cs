using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SeguridadDbContextData
    {
        public static async Task SeedUserAsync(UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    Nombre = "Ismael",
                    Apellido = "QS",
                    UserName = "iquirozs"
                };

                await userManager.CreateAsync(usuario, "Iquirozs2026$_");
            }
        } 
    }
}
