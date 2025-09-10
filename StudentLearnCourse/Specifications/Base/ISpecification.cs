using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Operation.Specifications.Base
{
    public interface IBaseSpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public List<string> IncludeStrings { get; set; }
        public Expression<Func<T, object>>? OrderByAsc { get; set; }
        public Expression<Func<T, object>>? OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
