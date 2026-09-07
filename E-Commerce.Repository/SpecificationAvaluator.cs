using E_Commerce.Core.Entities;
using E_Commerce.Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository
{
    public static class SpecificationAvaluator<Entity> where Entity : ModelEntity
    {
        // Products.Where(P => P.Id ==id).Include(P => P.Brand).Include(P => P.Category)
        public static IQueryable<Entity> GetQuery(IQueryable<Entity> Sequence,ISpecification<Entity> spec)
            {
            var query = Sequence;
            
            // query =  Products

            if(spec.Crateria is not  null)
               query = query.Where(spec.Crateria);
                // query =  Products.Where(P => P.Id ==id)

            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if(spec.OrderByDesc is not null) 
                query = query.OrderByDescending(spec.OrderByDesc);

                query = spec.Includes.Aggregate(query, (CurrentQuery, QueryExpression) => CurrentQuery.Include(QueryExpression));

            // query = Products.Where(P => P.Id ==id).Include(P => P.Brand).Include(P => P.Category)

            return query;
        }
    }
}
