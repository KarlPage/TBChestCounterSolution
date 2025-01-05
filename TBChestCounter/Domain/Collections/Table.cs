using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBChestCounter.Domain.Collections
{
  public class Table<T1,T2,T3>
  {
    private readonly Dictionary<ValueTuple<T1, T2>, T3> _items = new();

    public Table<T1,T2,T3> Add(T1 v1, T2 v2, T3 v3)
    {
      ValueTuple<T1, T2> key = new(v1, v2);
      _items[key] = v3;
      return this;
    }

    public T3 Query(T1 v1, T2 v2)
    {
      ValueTuple<T1, T2> key = new(v1, v2);
      return _items[key];
    }
  }
}
