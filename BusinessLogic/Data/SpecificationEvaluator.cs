using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SpecificationEvaluator<T> where T : BaseClass
    {
        public static IQueryable<T> MakeQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            if(spec.MyCondition != null)
            {
                inputQuery = inputQuery.Where(spec.MyCondition);
            }

            if(spec.MyOrderby != null)
            {
                inputQuery = inputQuery.OrderBy(spec.MyOrderby);
            }

            if(spec.MyOrderByDescending != null)
            {
                inputQuery = inputQuery.OrderByDescending(spec.MyOrderByDescending);
            }

            if (spec.IsPagingEnabled)
            {
                inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
            }

            inputQuery = spec.MyIncludes.Aggregate(inputQuery, (queryactual, include) => queryactual.Include(include));
            return inputQuery;
        }
    }
}
