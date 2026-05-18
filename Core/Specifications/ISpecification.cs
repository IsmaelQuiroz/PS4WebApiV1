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
        Expression<Func<T, bool>> MyCondition { get; }

        List<Expression<Func<T, object>>> MyIncludes { get; }

        Expression<Func<T, object>> MyOrderby { get; }

        Expression<Func<T, object>> MyOrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
