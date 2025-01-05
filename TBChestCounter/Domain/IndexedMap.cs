using System;
using System.Collections.Generic;

namespace TBChestCounter.Domain
{
  public class IndexedMap<TKey, TValue>(IEqualityComparer<TKey> keyComparer)
  {
    private readonly List<TValue> _valuesByOrdinal = [default];
    private readonly Dictionary<TKey, TValue> _values = new(keyComparer);

    public TValue ByOrdinal(int ordinal, Func<TValue> notFound)
    {
      return ((ordinal >= 0) && (ordinal < _valuesByOrdinal.Count)) ?
        _valuesByOrdinal[ordinal] : notFound();
    }

    public TValue ByKey(TKey key, Func<TValue> notFound)
    {
      return _values.TryGetValue(key, out TValue value) ?
        value : notFound();        
    }

    public TValue Add(TKey key, Func<int, TValue> factory)
    {      
      TValue result = ByKey(key, notFound: () =>
      {
        int ordinal = _valuesByOrdinal.Count;
        TValue newValue = factory(ordinal);
        _valuesByOrdinal.Add(newValue);
        _values.Add(key, newValue);
        return newValue;
      });
      return result;
    }
  }
}
