using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Tesseract;
using TBChestCounter.Core;
using System.Diagnostics;
using TBChestCounter.OCR;

namespace TBChestCounter
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.ResizeRedraw, true);
    }

    protected override void OnLoad(EventArgs e)
    {      
    }

    private void captureToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ScreenSelection.Run(this);
    }
  }
}
