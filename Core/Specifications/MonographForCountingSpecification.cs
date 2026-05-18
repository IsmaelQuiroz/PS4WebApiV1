using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class MonographForCountingSpecification : BaseSpecification<Monograph>
    {
        public MonographForCountingSpecification(MonographSpecificationParams monographParams)
            :base(x =>

                    //Where  ( @Search ISNULL OR (Keyword like '%@Search%' or Title like '%Search%')) AND
                    (string.IsNullOrEmpty(monographParams.Search) ||  (x.Title.Contains(monographParams.Search) || x.Keyword.Contains(monographParams.Keyword))) &&
                   
                   // (MarcaId ISNULL OR MarcaId = @MarcaId)
                   (!monographParams.CategoryId.HasValue || x.CategoryId == monographParams.CategoryId)     
            
                 )
        { }

    }
}
