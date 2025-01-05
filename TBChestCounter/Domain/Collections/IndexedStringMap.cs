using System;
using System.Collections.Generic;

namespace TBChestCounter.Domain.Collections
{
  public class IndexedStringMap
  {
    public struct Result(string key, int ordinal, bool isSome)
    {
      public static readonly Result None = new(string.Empty, 0, false); 
      public static Result Some(string key, int ordinal) => new(key, ordinal, true);

      public bool IsSome { get; } = isSome;
      public string Key { get; } = key;
      public int Ordinal { get; } = ordinal;

      public T Match<T>(Func<Result, T> some, Func<T> none) =>
        IsSome ? some(this) : none();
    }

    private readonly List<string> _byOrdinal = [];
    private readonly Dictionary<string, int> _byName = new(StringComparer.OrdinalIgnoreCase);

    public bool IsValidOrdinal(int ordinal) =>
      ordinal >= 0 && ordinal <= _byOrdinal.Count;

    public Result ByOrdinal(int ordinal) =>
      IsValidOrdinal(ordinal) ? Result.Some(_byOrdinal[ordinal], ordinal) : Result.None;

    public Result ByName(string key)
    {
      return _byName.TryGetValue(key, out int ordinal) ?
        Result.Some(key, ordinal) : Result.None;
    }

    public Result AddGet(string key)
    {
      if (!_byName.TryGetValue(key, out int ordinal))
      {
        ordinal = _byOrdinal.Count;
        _byName[key] = ordinal;
      }
      return Result.Some(key, ordinal);
    }
  }
}
