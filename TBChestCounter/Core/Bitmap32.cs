using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TBChestCounter.Core
{
  public class Bitmap32
  {
    public static Bitmap ScreenCapture(Form1 mainForm, Rectangle selection) =>
      Capture(mainForm, () => selection);

    public static Bitmap ScreenCaptureUsingManualSelection(Form1 mainForm) =>
      Capture(mainForm, FormScreenSelection.Run);

    public static Bitmap Capture(
      Form1 mainForm,
      Func<Rectangle> createSelectionRectangle)
    {
      try
      {
        mainForm.Hide();
        Rectangle rcSelection = createSelectionRectangle();
        Bitmap bm = new(rcSelection.Width, rcSelection.Height, PixelFormat.Format32bppArgb);
        Point upperLeft = new(rcSelection.Left, rcSelection.Top);
        Size copySize = new(rcSelection.Width, rcSelection.Height);

        using (Graphics g = Graphics.FromImage(bm))
        {
          g.CopyFromScreen(upperLeft, new Point(0, 0), copySize);
        }
        return bm;
      }
      finally
      {
        mainForm.Show();
      }
    }
  }
}
