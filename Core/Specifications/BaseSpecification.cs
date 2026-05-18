using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() { }

        public BaseSpecification(Expression<Func<T, bool>> condition)
        {
            MyCondition = condition;
        }

        public Expression<Func<T, bool>> MyCondition { get; } //Proviene de la Interface

        public List<Expression<Func<T, object>>> MyIncludes => new List<Expression<Func<T, object>>>();

        protected void MyAddInclude(Expression<Func<T, object>> includeExpression)
        => MyIncludes.Add(includeExpression);

        public Expression<Func<T, object>> MyOrderby{ get; private set; }

        protected void MyAddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            MyOrderby = orderByExpression;
        }

        public Expression<Func<T, object>> MyOrderByDescending { get; private set; }

        protected void MyAddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            MyOrderByDescending = orderByDescExpression;
        }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }


        protected void MyApplyPaging (int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }


    }
}
