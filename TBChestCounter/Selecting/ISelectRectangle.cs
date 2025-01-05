using System;
using System.Drawing;
using System.Windows.Forms;

namespace TBChestCounter.Selecting
{
  public class SelectRectangleChangedEventArgs(Rectangle rcOld, Rectangle rcNew) : EventArgs
  {
    public Rectangle OldBounds { get; } = rcOld;
    public Rectangle NewBounds { get; } = rcNew;
  }

  public interface ISelectRectangle
  {
    event EventHandler<SelectRectangleChangedEventArgs> Changed;
    event EventHandler Completed;

    Rectangle Bounds { get; }

    public void MouseDown(MouseEventArgs e);
    public void MouseMove(MouseEventArgs e);
    public void MouseUp(MouseEventArgs e);
  }
}
