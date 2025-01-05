using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace TBChestCounter.Domain.Chests
{
  public class ChestData
  {
    private readonly Dictionary<string, ChestEntry> _entries = new(StringComparer.OrdinalIgnoreCase);

    public ChestData(params ChestEntry[] chestTypes)
    {
      foreach (ChestEntry entry in chestTypes)
        _entries.Add(entry.Type, entry);
    }

    public ChestEntry Find(string type)
    {
      if (_entries.TryGetValue(type, out ChestEntry entry))
        return entry;
      return ChestEntry.None;
    }

    public Chest FindChest(string type, ChestLevel level)
    {
      var result = Find(type);
      if (result.IsNone)
        return Chest.None;

      ChestPoints cp = result.Find(level);
      if (cp.IsNone)
        return Chest.None;

      return new Chest(type, level, cp.Amount);
    }
  }
}
