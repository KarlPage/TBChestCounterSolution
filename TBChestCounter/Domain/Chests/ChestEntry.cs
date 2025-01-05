namespace TBChestCounter.Domain.Chests
{
  public class ChestEntry(string type, params ChestPoints[] points)
  {
    public static readonly ChestEntry None = new(string.Empty);

    public bool IsNone => Equals(None);
    public string Type { get; } = type;
    public ChestPoints[] Points { get; } = points;

    public ChestPoints Find(ChestLevel level)
    {
      int test = level.Value;
      for (int i=0; i<Points.Length; i++)
      {
        ChestPoints cur = Points[i];
        if (cur.Level == test)
          return cur;
      }
      return ChestPoints.None;
    }
  }
}
