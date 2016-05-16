using AutoMapper;
using System;
using System.Linq.Expressions;

namespace GrantRequests.Common
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
        public static IMappingExpression<TSource, TDestination> IgnoreAllNotSimple<TSource, TDestination>
    (this IMappingExpression<TSource, TDestination> map)
        {
           var properties = typeof(TDestination).GetProperties();
            foreach (var item in properties)
            {
                if (item.PropertyType.IsClass)
                {                   
                }
            }
            return map;
        }
    }
}