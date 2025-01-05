using System;
using System.Diagnostics;
using System.Windows.Forms;
using TBChestCounter.Domain.Chests;
using TBChestCounter.Domain.Clans;

namespace TBChestCounter
{
  internal static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());
    }

    static ClanData Test()
    {
      Clan clan = new(new(1), "BSL");
      ClanData data = new(clan);
      data.AddMember("Karlos");
      data.AddMember("Karlos Maximus");
      data.AddMember("Adoragas");
      data.AddMember("XCion");
      data.AddMember("Trojan Horse");
      data.AddMember("XCion");
      return data;
    }

    static void PopulateChestData()
    {
      ChestData data = CreateChestData();
      int count = 0;

      Stopwatch sw = Stopwatch.StartNew();

      for (int i = 0; i < 10000; i++)
      {
        var result = data.FindChest("avrena", ChestLevel.Any);
        if (result.IsValid)
          count++;
        result = data.FindChest("citadel", new(20));
        if (result.IsValid)
          count++;
      }
      sw.Stop();
      var time = sw.Elapsed;
      Trace.WriteLine(time.TotalMilliseconds);
    }

    static ChestData CreateChestData()
    {      
      var arena = new ChestEntry("Arena", new ChestPoints(ChestLevel.Any, 10));
      var citadel = new ChestEntry("Citadel",
        new(10, 100),
        new(15, 150),
        new(20, 200),
        new(25, 250),
        new(30, 300),
        new(35, 250));

      var crypts = new ChestEntry("Crypts",
        new(5, 1),
        new(10, 2),
        new(15, 3),
        new(20, 4),
        new(25, 5),
        new(30, 6),
        new(35, 7));
      return new(arena, citadel, crypts);
    }
  }
}

// Level 5 crypt
// Level 15 epic crypt
// level 25 rare crypt
// level 10 Citadel

