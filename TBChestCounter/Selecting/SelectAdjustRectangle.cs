using System;
using System.Drawing;
using System.Windows.Forms;

namespace TBChestCounter.Selecting
{
  class SelectAdjustRectangle : ISelectRectangle
  {
    public Rectangle Bounds { get; private set; }

    public SelectAdjustRectangle(Rectangle initialBounds)
    {
      Bounds = initialBounds;
    }

    public event EventHandler<SelectRectangleChangedEventArgs> Changed;
    public event EventHandler Completed;

    public void MouseDown(MouseEventArgs e)
    {
    }

    public void MouseMove(MouseEventArgs e)
    {
    }

    public void MouseUp(MouseEventArgs e)
    {
    }
  }
}
