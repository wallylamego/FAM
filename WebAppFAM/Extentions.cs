using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebAppFAM
{
    public static class ExtensionMethods
    {
        public static string UppercaseSpecifiedNumbers(this string value, int NoOfChar)
        {
            // Uppercase the first letter in the string.
            if (value.Length > 0)
            {
                if (NoOfChar > 0)
                {
                    char[] array = value.ToCharArray();
                    for (int i = 0; i < NoOfChar; i++)
                    {
                        array[i] = char.ToUpper(array[i]);
                    }

                    return new string(array);
                }

            }
            return value;

        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string memberName, bool asc = true)
        {
          ParameterExpression[] typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };
            System.Reflection.PropertyInfo pi = typeof(T).GetProperty(memberName);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    asc ? "OrderBy" : "OrderByDescending",
                    new Type[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
                );
        }
    }
}