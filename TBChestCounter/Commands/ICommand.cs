using System;

namespace TBChestCounter.Commands
{
  public interface ICommand
  {
    void Run();
  }

  public abstract class Command : ICommand
  {
    public void Run()
    {
      try
      {
        OnRun();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    protected abstract void OnRun();
  }
}
