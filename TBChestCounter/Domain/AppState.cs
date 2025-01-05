using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBChestCounter.Domain.Chests;

namespace TBChestCounter.Domain
{
  public static class AppState
  {
    public static ChestData ChestData { get; private set; }

    public static void SetChestData(ChestData data)
    {
      ChestData = data;
    }
  }
}
