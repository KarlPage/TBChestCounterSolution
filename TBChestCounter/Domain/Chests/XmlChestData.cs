using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TBChestCounter.Domain.Chests
{
  public class XmlChestData
  {
    public static ChestData Load(string fileName)
    {
      XDocument doc = XDocument.Load(fileName);
      var chests = XChests(doc.Descendants("chest"));
      return new(chests);
    }

    static ChestEntry XChest(XElement xChest)
    {
      string type = (string)xChest.Attribute("type");
      var points = xChest
        .Descendants("points")
        .Select(ParseChestPoints)
        .ToArray();
      return new(type, points);
    }

    static ChestEntry[] XChests(IEnumerable<XElement> xChests)
    {
      List<ChestEntry> chestEntries = [];

      foreach (XElement xChest in xChests)
      {
        ChestEntry entry = XChest(xChest);
        chestEntries.Add(entry);
      }
      return chestEntries.ToArray();
    }

    static string Attribute(XElement elem, string name)
    {
      XAttribute attr = elem.Attribute(name);
      return (null == attr) ? string.Empty : attr.Value;
    }

    static ChestPoints ParseChestPoints(XElement xPoints)
    {
      string strLevel = Attribute(xPoints, "level");
      ChestLevel level = ChestLevel.Parse(strLevel);

      string strAmount = Attribute(xPoints, "amount");
      if (string.IsNullOrEmpty(strAmount))
        return new(-1, -1);
      int amount = int.Parse(strAmount);
      return new(level, amount);
    }
  }
}
