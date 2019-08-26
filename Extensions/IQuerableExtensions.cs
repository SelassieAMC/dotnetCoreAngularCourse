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
    }
}