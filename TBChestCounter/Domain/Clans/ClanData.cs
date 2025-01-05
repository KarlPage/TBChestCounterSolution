using System;

namespace TBChestCounter.Domain.Clans
{
  public class ClanData
  {
    private readonly IndexedMap<string, ClanMember> _members = new(StringComparer.OrdinalIgnoreCase);

    public Clan Clan { get; }
    
    public ClanMember MemberByOrdinal(int ordinal) =>
      _members.ByOrdinal(ordinal, () => ClanMember.Empty);

    public ClanMember MemberByName(string name) =>
      _members.ByKey(name, () => ClanMember.Empty);

    public ClanData(Clan clan)
    {
      this.Clan = clan;
    }

    public ClanMember AddMember(string name)
    {
      return _members.Add(name, id => new ClanMember(new(id), name));
    }
  }
}
