using TBChestCounter.Domain;
using TBChestCounter.Domain.Chests;

namespace TBChestCounter.Commands
{
  class CommandLoadChestData : Command
  {
    static readonly string FileName = "ChestData.xml";

    protected override void OnRun()
    {
      ChestData chestData = XmlChestData.Load(FileName);
      AppState.SetChestData(chestData);
    }
  }
}
