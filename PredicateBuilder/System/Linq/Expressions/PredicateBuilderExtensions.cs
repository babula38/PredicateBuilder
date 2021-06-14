// Decompiled with JetBrains decompiler
// Type: System.Linq.Expressions.PredicateBuilderExtensions
// Assembly: PredicateBuilder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BFECE19D-3C6B-4B2A-9BB0-07DC91526A17
// Assembly location: C:\Users\babula.pradhan\Downloads\predicatebuilder.1.0.0\lib\netstandard1.0\PredicateBuilder.dll

using PredicateBuilder;
using System.Collections.Generic;

namespace System.Linq.Expressions
{
  public static class PredicateBuilderExtensions
  {
    public static Expression<Func<T, bool>> And<T>(
      this Expression<Func<T, bool>> first,
      Expression<Func<T, bool>> second)
    {
      return first.Compose<Func<T, bool>>(second, new Func<Expression, Expression, Expression>(Expression.And));
    }

    public static Expression<Func<T, bool>> Or<T>(
      this Expression<Func<T, bool>> first,
      Expression<Func<T, bool>> second)
    {
      return first.Compose<Func<T, bool>>(second, new Func<Expression, Expression, Expression>(Expression.Or));
    }

    private static Expression<T> Compose<T>(
      this Expression<T> first,
      Expression<T> second,
      Func<Expression, Expression, Expression> merge)
    {
      Expression expression = ParameterRebinder.ReplaceParameters(first.Parameters.Select((f, i) => new
      {
        f = f,
        s = second.Parameters[i]
      }).ToDictionary(p => p.s, p => p.f), second.Body);
      return Expression.Lambda<T>(merge(first.Body, expression), (IEnumerable<ParameterExpression>) first.Parameters);
    }
  }
}
