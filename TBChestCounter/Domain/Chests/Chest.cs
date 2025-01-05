namespace TBChestCounter.Domain.Chests
{
  public class Chest(string type, ChestLevel level, int points)
  {
    public static readonly Chest None = new(string.Empty, ChestLevel.Any, 0);

    public bool IsValid => !Equals(None);
    public string Type { get; } = type;
    public ChestLevel Level { get; } = level;
    public int Points { get; } = points;
  }
}
