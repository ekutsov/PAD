using System.Linq.Expressions;
using PAD.Finance.Domain.Enums;

namespace PAD.Finance.Core.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TSource> OrderByDirection<TSource>(this IQueryable<TSource> query, string sortColumn, SortDirection sortDirection)
    {
        if (string.IsNullOrWhiteSpace(sortColumn) || sortDirection == SortDirection.None) sortColumn = "Id";

        ParameterExpression parameter = Expression.Parameter(typeof(TSource), "item");

        MemberExpression member = Expression.Property(parameter, sortColumn);

        LambdaExpression expression = Expression.Lambda(member, parameter);

        string method = sortDirection == SortDirection.Descending ? "OrderByDescending" : "OrderBy";

        Type[] types = new Type[] { query.ElementType, expression.Body.Type };

        MethodCallExpression mce = Expression.Call(typeof(Queryable), method, types, query.Expression, expression);

        return query.Provider.CreateQuery<TSource>(mce);
    }
}