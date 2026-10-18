using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic;
public class MonographService : IMonographService
{
    private readonly IMonographRepository _monographRepository;

    public MonographService(IMonographRepository monographRepository)
    {
        _monographRepository = monographRepository;
    }

    public async Task<(int statusCode, string message)> CreateMonographAsync(Monograph monograph)
    {
        var titleExists = await _monographRepository.ExistTitleAsync(monograph.Title, 0);
        var codeExists = await _monographRepository.ExistCodeAsync(monograph.Code,0);
        
        if (titleExists)
        {
            return (404, "Ya existe el Título en la Base de Datos");
        }
        else if (codeExists)
        {
            return (404, "El Código que desea agregar, Ya Existe en la Base de Datos");
        }

        return (await _monographRepository.Add(monograph), "Monografía agregada con éxito");
    }

    public async Task<(int statusCode, string message)> UpdateMonographAsync(Monograph monograph)
    {
        var existTile = await _monographRepository.ExistTitleAsync(monograph.Title, monograph.Id);
        var codeExist = await _monographRepository.ExistCodeAsync(monograph.Code, monograph.Id);

        if (existTile)
        {
            return (404, "Ya existe el Título en la Base de Datos");
        }
        else if (codeExist)
        {
            return (404, "El Código que desea agregar, Ya Existe en la Base de Datos");
        }

        return (await _monographRepository.Update(monograph), "Monografía actualizada con éxito");
    }

   
}
