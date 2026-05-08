using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public static class DbContextExtensions
    {
        public static async Task<bool> HasPendingMigrtionsAsync(this DbContext context)
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            return pendingMigrations.Any();
        }
    }
}
