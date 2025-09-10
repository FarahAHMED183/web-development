using Microsoft.EntityFrameworkCore;
using CRUD_Operation.Specifications.Base;

namespace CRUD_Operation.Specifications.Base;

public class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, IBaseSpecification<T> specification)
    {
        var query = inputQuery;

        // Apply criteria (Where clauses)
        if (specification.Criterias != null && specification.Criterias.Any())
        {
            foreach (var criteria in specification.Criterias)
            {
                query = query.Where(criteria);
            }
        }

        
        if (specification.Includes != null && specification.Includes.Any())
        {
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (specification.IncludeStrings != null && specification.IncludeStrings.Any())
        {
            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
        }

        if (specification.OrderByAsc != null)
        {
            query = query.OrderBy(specification.OrderByAsc);
        }
        else if (specification.OrderByDesc != null)
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        if (specification.IsPaginationEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }
}
