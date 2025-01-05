using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBChestCounter.Domain.Clans
{
  public record struct ClanId(int Value);

  public class Clan(ClanId id, string name)
  {
    public ClanId Id { get; } = id;
    public string Name { get; } = name;
  }
}
