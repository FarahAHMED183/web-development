using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Operation.Specifications.Base
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; set; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; set; } = new List<string>();
        public Expression<Func<T, object>>? OrderByAsc { get; set; }
        public Expression<Func<T, object>>? OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criterias.Add(criteria);
        }

        public void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
        
        public void ApplyOrderByAsc(Expression<Func<T, object>> orderByAsc)
        {
            OrderByAsc = orderByAsc;
        }

        public void ApplyOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }

        public void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginationEnabled = true;
        }
    }
}
