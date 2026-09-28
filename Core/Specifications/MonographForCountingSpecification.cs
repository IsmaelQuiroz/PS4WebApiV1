using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications;

    public class MonographForCountingSpecification : BaseSpecification<Monograph>
    {
        public MonographForCountingSpecification(MonographSpecificationParams monographParams)
            :base(x =>

                    (string.IsNullOrEmpty(monographParams.Search)
                        || (
                                x.Title.ToUpper().Contains(monographParams.Search.ToUpper()) ||
                                x.Keyword.ToUpper().Contains(monographParams.Search.ToUpper()) ||
                                x.Category.Name.ToUpper().Contains(monographParams.Search.ToUpper())
                           )
                    )
               )


        { }

    }

