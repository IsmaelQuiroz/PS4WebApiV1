using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class PS4DbContextData
    {
        public static async Task AsyncDataLoading(PS4DbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Product.Any())
                {
                    var producto = new Product
                    {
                        Name = "Planillas o Monografías",
                        Description = "Tipo de producto asociado a la herramienta de SearchMonographs",
                        Stock = 0
                    };
                    context.Add(producto);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
