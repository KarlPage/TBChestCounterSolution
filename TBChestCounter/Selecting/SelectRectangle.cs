using System;
using System.Drawing;
using System.Windows.Forms;

namespace TBChestCounter.Selecting
{
  public class SelectRectangle : ISelectRectangle
  {
    private int left;
    private int top;
    private int right;
    private int bottom;

    public event EventHandler<SelectRectangleChangedEventArgs> Changed = delegate { };
    public event EventHandler Completed = delegate { };

    public Rectangle Bounds { get; private set; }

    public void MouseDown(MouseEventArgs e) =>
      HandleMouse(MouseCode.Down, e);

    public void MouseMove(MouseEventArgs e) =>
      HandleMouse(MouseCode.Move, e);

    public void MouseUp(MouseEventArgs e) =>
      HandleMouse(MouseCode.Up, e);

    void RaiseChanged()
    {
      Rectangle rcOld = Bounds;
      Normalise();
      Rectangle rcNew = Bounds;
      Changed(this, new(rcOld, rcNew));
    }

    void HandleMouse(MouseCode code, MouseEventArgs e)
    {
      switch (code)
      {
        case MouseCode.Down:
          left = e.X;
          top = e.Y;
          break;

        case MouseCode.Move:
          if (e.Button == MouseButtons.Left)
          {
            right = e.X;
            bottom = e.Y;
            RaiseChanged();
          }
          break;

        case MouseCode.Up:
          right = e.X;
          bottom = e.Y;
          RaiseChanged();
          Completed(this, new());
          break;
      }
    }

    void Normalise()
    {
      int l = Math.Min(left, right);
      int t = Math.Min(top, bottom);
      int r = Math.Max(left, right);
      int b = Math.Max(top, bottom);
      Bounds = new(l, t, r - l, b - t);
    }
  }
}
