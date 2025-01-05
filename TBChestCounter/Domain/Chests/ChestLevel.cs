using System.Data;

namespace TBChestCounter.Domain.Chests
{
  public readonly record struct ChestLevel(int Value)
  {
    public static readonly ChestLevel Any = new(0);

    public static implicit operator ChestLevel(int value) =>
      new(value);

    public bool IsAny => Equals(Any);

    public static ChestLevel Parse(string text)
    {
      if (text == null)
        return new(-1);
      if (text.Equals("any"))
        return Any;
      return new(int.Parse(text));
    }

    public override string ToString() =>
      IsAny ? "Any" : $"{Value}";
  }
}
