using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TBChestCounter
{
  public static class SelectionBoxModule
  {    
    private static readonly Boxes _selectionBoxes = new();
    private static Control _control = null;
    private static Box _selectedBox = null;
    private static HitTestResult _lastHit = HitTestResult.None;
    private static Manipulate _manipulate = new();

    public static int SelectionPointSize { get; set; } = 6;

    public static void Init(Control owner)
    {
      _control = owner;
      _control.Paint += OwnerPaint;
      _control.MouseMove += OwnerMouseMove;
      _control.MouseDown += OwnerMouseDown;
    }

    public static Box SelectedBox
    {
      get => _selectedBox;
      set { _selectedBox = value; }
    }

    public static void AddSelectionBox(Box box)
    {
      _selectionBoxes.Add(box);
    }

    public static void Render(Graphics g)
    {
      _selectionBoxes.Render(g);
    }

    private static void OwnerMouseDown(object sender, MouseEventArgs e)
    {
      if (_manipulate.IsValid)
      {
        _manipulate.MouseDown(e);
        return;
      }

      HitTestResult hr = _selectionBoxes.HitTest(e.Location);
      if (HitTestResult.IsEqual(_lastHit, hr))
        return;

      if (!hr.IsNone)
      {
        hr.Box.Select(true);
        _lastHit = hr;
        _control.Invalidate();
      }
    }

    private static void OwnerMouseMove(object sender, MouseEventArgs e)
    {
      if (_manipulate.IsValid)
      {
        _manipulate.MouseMove(e);
        return;
      }
      HitTestResult hr = _selectionBoxes.HitTest(e.Location);
      if (HitTestResult.IsEqual(_lastHit, hr))
        return;
      _lastHit = hr;
      SetCursor(hr.Ordinal);
      Trace.WriteLine(hr);
    }

    private static void OwnerPaint(object sender, PaintEventArgs e)
    {
      Render(e.Graphics);
    }

    public enum SelectionPointDirection : sbyte
    {
      None = -1,
      Left,
      Right,
      TopLeft,
      TopMid,
      TopRight,
      BottomLeft,
      BottomMid,
      BottomRight,

      Box
    }

    public class HitTestResult
    {
      public static HitTestResult None = new() { Ordinal = SelectionPointDirection.None };
      public bool IsNone => Equals(None);

      public Box Box;
      public SelectionPointDirection Ordinal;

      public static bool IsEqual(HitTestResult hr1, HitTestResult hr2) =>
        hr1.Ordinal == hr2.Ordinal && hr1.Box == hr2.Box;

      public override string ToString() =>
        $"Box({Box?.Bounds}, Ordinal({Ordinal}))";
    }        

    class Manipulate
    {
      private Box _box;

      public bool IsValid => _box != null;

      public void SetActive(Box box)
      {
        _box = box;
      }

      public void MouseDown(MouseEventArgs e)
      {
      }

      public void MouseMove(MouseEventArgs e)
      {
      }
    }

    class BoxPoints
    {
      private readonly Point[] _points = new Point[8];

      public Point Get(SelectionPointDirection ord)
      {
        return _points[(int)ord];
      }

      public void Set(SelectionPointDirection ord, Point pt)
      {
        _points[(int)ord] = pt;
      }

      public void Update(Rectangle bounds)
      {
        Set(SelectionPointDirection.Left, new Point(bounds.Left, bounds.Top + (bounds.Height/2)));
        Set(SelectionPointDirection.Right, new Point(bounds.Right, bounds.Top + (bounds.Height / 2)));
        Set(SelectionPointDirection.TopLeft, new(bounds.Left, bounds.Top));
        Set(SelectionPointDirection.TopMid, new(bounds.Left+(bounds.Width/2), bounds.Top));
        Set(SelectionPointDirection.TopRight, new(bounds.Right, bounds.Top));
        Set(SelectionPointDirection.BottomLeft, new(bounds.Left, bounds.Bottom));
        Set(SelectionPointDirection.BottomMid, new(bounds.Left + (bounds.Width / 2), bounds.Bottom));
        Set(SelectionPointDirection.BottomRight, new(bounds.Right, bounds.Bottom));
      }

      public SelectionPointDirection HitTest(Point ptTest)
      {
        int extents = SelectionPointSize * 2;
        for (int i=0; i<_points.Length; i++)
        {
          Point pt = _points[i];
          Rectangle rcTest = new(
            pt.X - SelectionPointSize,
            pt.Y - SelectionPointSize,
            extents,
            extents);
          if (rcTest.Contains(ptTest))
            return (SelectionPointDirection)i;          
        }
        return SelectionPointDirection.None;
      }

      public void Render(Graphics g)
      {
        int extents = SelectionPointSize * 2;
        foreach (Point pt in _points)
        {
          Rectangle rcDraw = new(pt.X - SelectionPointSize, pt.Y - SelectionPointSize, extents, extents);
          g.FillRectangle(Brushes.Green, rcDraw);
        }
      }
    }

    public class Box
    {
      private bool _selected = false;
      private Rectangle _bounds;
      private readonly BoxPoints _boxPoints = new();

      public Rectangle Bounds => _bounds;

      internal Box(Rectangle bounds)
      {
        _bounds = bounds;
        Update(bounds);
      }

      public void Select(bool select)
      {
        _selected = select;
        _manipulate.SetActive(this);
      }      

      internal HitTestResult HitTest(Point loc)
      {
        int extents = SelectionPointSize * 2;
        Rectangle rcBox = Bounds;
        Rectangle rcTest = new(
          rcBox.Left - SelectionPointSize,
          rcBox.Top - SelectionPointSize,
          rcBox.Width+extents,
          rcBox.Height+extents);

        if (rcTest.Contains(loc))
        {
          SelectionPointDirection ordinal = _boxPoints.HitTest(loc);
          if (ordinal == SelectionPointDirection.None)
            ordinal = SelectionPointDirection.Box;
          return new() { Box = this, Ordinal = ordinal };
        }
        return HitTestResult.None;
      }

      internal void Update(Rectangle bounds)
      {
        _boxPoints.Update(bounds);
      }

      internal void Render(Graphics g)
      {
        g.DrawRectangle(Pens.Green, _bounds);
        if(_selected)
          _boxPoints.Render(g);
      }
    }

    class Boxes
    {
      private readonly List<Box> _items = [];

      public void Add(Box box)
      {
        _items.Add(box);
      }

      public HitTestResult HitTest(Point loc)
      {
        HitTestResult hr = HitTestResult.None;

        foreach (Box box in _items)
        {
          hr = box.HitTest(loc);
          if (!hr.IsNone)
            return hr;
        }
        return hr;
      }

      internal void Render(Graphics g)
      {
        foreach (Box box in _items)
        {
          box.Render(g);
        }
      }
    }

    static void SetCursor(SelectionPointDirection dir)
    {
      _control.Cursor = dir switch
      {
        SelectionPointDirection.Left => Cursors.SizeWE,
        SelectionPointDirection.Right => Cursors.SizeWE,
        SelectionPointDirection.TopLeft => Cursors.SizeNWSE,
        SelectionPointDirection.TopMid => Cursors.SizeNS,
        SelectionPointDirection.TopRight => Cursors.SizeNESW,
        SelectionPointDirection.BottomLeft => Cursors.SizeNESW,
        SelectionPointDirection.BottomMid => Cursors.SizeNS,
        SelectionPointDirection.BottomRight => Cursors.SizeNWSE,
        SelectionPointDirection.Box => Cursors.SizeAll,
        _ => Cursors.Default,
      };
    }
  }
}
