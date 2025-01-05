using System.Drawing;
using System.Windows.Forms;
using TBChestCounter.Selecting;

namespace TBChestCounter.OCR
{
  public partial class FormScreenBoxSelection : Form
  {
    private SelectRectangle _selection = new();

    public FormScreenBoxSelection()
    {
      InitializeComponent();
      KeyPreview = true;
      _selection.Changed += SelectionChanged;
      _selection.Completed += (s, e) => Close();
    }

    void SelectionChanged(object sender, SelectRectangleChangedEventArgs e)
    {
      using Graphics g = CreateGraphics();
      g.DrawRectangle(Pens.Black, e.OldBounds);
      g.DrawRectangle(Pens.White, e.NewBounds);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.DrawRectangle(Pens.White, _selection.Bounds);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        Close();
      base.OnKeyDown(e);
    }

    protected override void OnMouseDown(MouseEventArgs e) =>
      _selection.MouseDown(e);

    protected override void OnMouseUp(MouseEventArgs e) =>
      _selection.MouseUp(e);

    protected override void OnMouseMove(MouseEventArgs e) =>
      _selection.MouseMove(e);
  }
}
