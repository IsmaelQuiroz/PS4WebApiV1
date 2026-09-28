using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> MyCondition { get; } //Define el filtro principal de la consulta (el equivalente al Where de LINQ o SQL).

        List<Expression<Func<T, object>>> MyIncludes { get; } //Una lista de relaciones que deben cargarse ávidamente (Eager Loading) para evitar el problema de consultas N+1 (el equivalente a Include en EF).

        Expression<Func<T, object>> MyOrderby { get; }

        Expression<Func<T, object>> MyOrderByDescending { get; }

        int Take { get; } //La cantidad máxima de registros que deseas obtener (el límite de la página).
        int Skip { get; } //La cantidad de registros que debes saltarte antes de empezar a contar (el desfase de la página).
        bool IsPagingEnabled { get; } //Una bandera para indicarle al evaluador de la consulta si debe o no aplicar la paginación (Skip y Take).
    }
}
