
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data;

public class SeguridadDbContext : IdentityDbContext<Usuario>
{
    public SeguridadDbContext(DbContextOptions<SeguridadDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

//Command for migrations --> Developer PowerShell
//root> dotnet  ef migrations add SeguridadInicio -p BusinessLogic -s WebApi -o Identity/Migrations -c SeguridadDbContext