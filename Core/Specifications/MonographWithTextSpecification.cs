using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications;

public class MonographWithTextSpecification : BaseSpecification<Monograph>
{
    public MonographWithTextSpecification(MonographSpecificationParams monographParams) 
        :base ( x =>
                     //(string.IsNullOrEmpty(monographParams.Search) || x.Title.To Upper().Contains(monographParams.Search.ToUpper())) ||
                     //(string.IsNullOrEmpty(monographParams.Search) || x.Keyword.ToUpper().Contains(monographParams.Search.ToUpper()) ) ||
                     //(string.IsNullOrEmpty(monographParams.Search) || x.Category.Name.ToUpper().Contains(monographParams.Search.ToUpper()) )        
                     (string.IsNullOrEmpty(monographParams.Search)
                        || (
                                x.Title.ToUpper().Contains(monographParams.Search.ToUpper()) ||
                                x.Keyword.ToUpper().Contains(monographParams.Search.ToUpper()) ||
                                x.Category.Name.ToUpper().Contains(monographParams.Search.ToUpper())
                           )
                    )
               )
    {
        MyAddInclude(p => p.Category);
        MyAddInclude(p => p.Product);
        MyApplyPaging(monographParams.PageSize * (monographParams.PageIndex - 1), monographParams.PageSize);

        //Add OrderBy switch
        if (!string.IsNullOrEmpty(monographParams.Sort))
        {
            switch (monographParams.Sort)
            {
                case "titleAsc":
                    MyAddOrderBy(p => p.Title);
                    break;
                case "titleDesc":
                    MyAddOrderByDescending(p => p.Title);
                    break;
                default:
                    MyAddOrderBy(p => p.Title);
                    break;
            }
        }
    }
}
