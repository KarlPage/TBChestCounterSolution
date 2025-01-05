using System;
using System.Drawing;
using System.Windows.Forms;
using TBChestCounter.Properties;

namespace TBChestCounter.Core
{
  public partial class FormScreenSelection : Form
  {
    public static readonly Color TestColor = Color.Purple;

    public static Rectangle Run()
    {
      FormScreenSelection form = new();
      try
      {
        form.BackColor = Color.Green;
        form.TransparencyKey = TestColor;
        form.Opacity = 0.3;
        form.WindowState = FormWindowState.Maximized;
        form.ShowDialog();
      }
      finally
      {
        form.Dispose();
      }
      return form.SelectionRectangle;
    }

    private bool _isDragging = false;
    private Point _ptBegin;
    private Point _ptEnd;

    public FormScreenSelection()
    {
      InitializeComponent();
      Cursor = Cursors.Cross;
    }

    public Rectangle SelectionRectangle
    {
      get
      {
        int left = Math.Min(_ptBegin.X, _ptEnd.X);
        int top = Math.Min(_ptBegin.Y, _ptEnd.Y);
        int right = Math.Max(_ptBegin.X, _ptEnd.X);
        int bottom = Math.Max(_ptBegin.Y, _ptEnd.Y);
        int width = (right - left);
        int height = (bottom - top);

        return new(left, top, width, height);
      }
    }

    protected override void OnPaintBackground(PaintEventArgs e) { }

    protected override void OnPaint(PaintEventArgs e)
    {
      Rectangle rcSelection = SelectionRectangle;

      e.Graphics.Clear(BackColor);

      using Pen pen = new(TestColor);
      e.Graphics.DrawRectangle(Pens.White, rcSelection);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      _isDragging = e.Button == MouseButtons.Left;
      _ptBegin = e.Location;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;

      _isDragging = false;
      _ptEnd = e.Location;
      Close();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if ((e.Button != MouseButtons.Left) && !_isDragging)
        return;

      _ptEnd = e.Location;
      Invalidate();      
    }    
  }
}
