using System.Drawing;
using System.Windows.Forms;

namespace TBChestCounter.OCR
{
  public class ScreenSelection
  {
    public static void Run(Form mainForm)
    {
      using (FormScreenBoxSelection formSelection = new())
      {
        try
        {
          mainForm.Hide();          
          formSelection.BackColor = Color.Black;
          formSelection.TransparencyKey = Color.White;
          formSelection.Opacity = .3;
          formSelection.ShowDialog();
        }
        finally
        {
          mainForm.Show();
        }
      }
    }
  }
}
