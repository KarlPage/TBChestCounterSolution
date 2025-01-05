namespace TBChestCounter.Domain.Clans
{
  public record struct ClanMemberId(int Value);

  public class ClanMember(ClanMemberId id, string name)
  {
    public static readonly ClanMember Empty = new(new(0), string.Empty);

    public ClanMemberId Id { get; } = id;
    public string Name { get; } = name;

    public override string ToString() =>
      $"{Id},{Name}";
  }
}
