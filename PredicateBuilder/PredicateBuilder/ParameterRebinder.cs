// Decompiled with JetBrains decompiler
// Type: PredicateBuilder.ParameterRebinder
// Assembly: PredicateBuilder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BFECE19D-3C6B-4B2A-9BB0-07DC91526A17
// Assembly location: C:\Users\babula.pradhan\Downloads\predicatebuilder.1.0.0\lib\netstandard1.0\PredicateBuilder.dll

using System.Collections.Generic;
using System.Linq.Expressions;

namespace PredicateBuilder
{
  internal class ParameterRebinder : ExpressionVisitor
  {
    private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

    private ParameterRebinder(
      Dictionary<ParameterExpression, ParameterExpression> map)
    {
      this._map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
    }

    public static Expression ReplaceParameters(
      Dictionary<ParameterExpression, ParameterExpression> map,
      Expression exp)
    {
      return new ParameterRebinder(map).Visit(exp);
    }

    protected override Expression VisitParameter(ParameterExpression p)
    {
      ParameterExpression parameterExpression;
      if (this._map.TryGetValue(p, out parameterExpression))
        p = parameterExpression;
      return base.VisitParameter(p);
    }
  }
}
