using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vegas.Core.Models;

namespace Vegas.Extensions
{
    public static class IQuerableExtensions 
    {
        public static IQueryable<T> ApplyOrdering<T> (this IQueryable<T> query, Dictionary<string, Expression<Func<T,Object>>> columnsMap, IQueryObject queryObj){
            if(String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy)){
                return query;
            }
            if(queryObj.IsSortAscending){
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            }else{
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
            }
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj){
            if(queryObj.Page <= 0)
                queryObj.Page = 1;
            if(queryObj.PageSize <= 0)
                queryObj.PageSize = 100;
            return query.Skip((queryObj.Page - 1) * (queryObj.Page)).Take(queryObj.PageSize);
        }
    }
}