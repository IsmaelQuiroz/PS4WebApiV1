using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class MonographWithCategorySpecification : BaseSpecification<Monograph>
    {
        public MonographWithCategorySpecification(MonographSpecificationParams monographParams)
        :base( x => 
                (string.IsNullOrEmpty(monographParams.Search) || ( x.Title.Contains(monographParams.Search) || x.Keyword.Contains(monographParams.Keyword) )) &&
                (!monographParams.CategoryId.HasValue || x.CategoryId == monographParams.CategoryId )
             )
        
        {
            MyAddInclude(x => x.Category);

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


        public MonographWithCategorySpecification(int id) : base ( x => x.Id == id)
        {
            MyAddInclude(p => p.Category);
        }
    }
}
