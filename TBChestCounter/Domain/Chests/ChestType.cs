namespace TBChestCounter.Domain.Chests
{
  public class ChestType(string name)
  {
    public string Name { get; } = name;

    public override string ToString() =>
      Name;
  }
}
