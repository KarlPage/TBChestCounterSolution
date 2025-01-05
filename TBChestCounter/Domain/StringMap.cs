using System;
using System.Collections.Generic;

namespace TBChestCounter.Domain
{
  public class StringMap<TValue>
  {
    private readonly Dictionary<string, TValue> _items = new(StringComparer.OrdinalIgnoreCase);

    public TValue AddGet(string key, Func<TValue> factory)
    {
      if (!_items.TryGetValue(key, out TValue item))
      {
        item = factory();
        _items[key] = item;
      }
      return item;
    }

    public TValue Get(string key, Func<TValue> ifMissing) =>
      _items.TryGetValue(key, out TValue value) ? value : ifMissing(); 
  }
}
