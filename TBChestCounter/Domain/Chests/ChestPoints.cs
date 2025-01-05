namespace TBChestCounter.Domain.Chests
{
  public struct ChestPoints(ChestLevel level, int amount)
  {
    public static readonly ChestPoints None = new(ChestLevel.Any, 0);

    public readonly bool IsNone => Equals(None);
    public ChestLevel Level { get; } = level;
    public int Amount { get; set; } = amount;
    public readonly override string ToString() =>
      $"{Level}={Amount}";
  }
}
