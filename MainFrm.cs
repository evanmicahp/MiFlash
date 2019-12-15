using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using QFlashKit.code.data;
using QFlashKit.code.lan;
using QFlashKit.code.miControl;
using QFlashKit.code.module;
using QFlashKit.code.Utility;
using QFlashKit.form;
using Timer = System.Windows.Forms.Timer;

// TODO add localization

namespace QFlashKit
{
    public class MainFrm : MiBaseFrm
    {
        private Button _btnBrwDic;
        private Button _btnFlash;
        private Button _btnRefresh;
        private ColumnHeader _clnDevice;
        private ColumnHeader _clnID;
        private ColumnHeader _clnProgress;
        private ColumnHeader _clnResult;
        private ColumnHeader _clnStatus;
        private ColumnHeader _clnTime;
        private ListView _devicelist;
        private FolderBrowserDialog _fbdSelect;
        private Label _lblMD5;
        private MenuStrip _menuStrip1;
        private ToolStripMenuItem _miConfiguration;
        private ToolStripMenuItem _miFlashConfigurationToolStripMenuItem;
        private ToolStripMenuItem _miInstallDrivers;
        private RadioStripItem _rdoCleanAll;
        private RadioStripItem _rdoCleanAllAndLock;
        private RadioStripItem _rdoSaveUserData;
        private ToolStripStatusLabel _statusLblMsg;
        private StatusStrip _statusStrp;
        private ToolStripStatusLabel _statusTab;
        private Timer _timerUpdateStatus;
        private RichTextBox _txtLog;
        private TextBox _txtPath;
        private IContainer components;
        private ProcessFrm frm = new ProcessFrm();

        public MainFrm()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            CheckAdmin(IsRunAsAdmin());
            components = new Container();
            _txtPath = new TextBox();
            _btnBrwDic = new Button();
            _fbdSelect = new FolderBrowserDialog();
            _btnRefresh = new Button();
            _btnFlash = new Button();
            _devicelist = new ListView();
            _clnID = new ColumnHeader();
            _clnDevice = new ColumnHeader();
            _clnProgress = new ColumnHeader();
            _clnTime = new ColumnHeader();
            _clnStatus = new ColumnHeader();
            _clnResult = new ColumnHeader();
            _txtLog = new RichTextBox();
            _timerUpdateStatus = new Timer(components);
            _statusStrp = new StatusStrip();
            _statusLblMsg = new ToolStripStatusLabel();
            _statusTab = new ToolStripStatusLabel();
            _rdoCleanAll = new RadioStripItem();
            _rdoSaveUserData = new RadioStripItem();
            _rdoCleanAllAndLock = new RadioStripItem();
            _lblMD5 = new Label();
            _menuStrip1 = new MenuStrip();
            _miConfiguration = new ToolStripMenuItem();
            _miFlashConfigurationToolStripMenuItem = new ToolStripMenuItem();
            _miInstallDrivers = new ToolStripMenuItem();
            _statusStrp.SuspendLayout();
            _menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtPath
            // 
            _txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                               | AnchorStyles.Left
                                               | AnchorStyles.Right;
            _txtPath.Location = new Point(98, 33);
            _txtPath.Name = "_txtPath";
            _txtPath.Size = new Size(668, 20);
            _txtPath.TabIndex = 0;
            // 
            // btnBrwDic
            // 
            _btnBrwDic.Location = new Point(21, 31);
            _btnBrwDic.Name = "_btnBrwDic";
            _btnBrwDic.Size = new Size(75, 23);
            _btnBrwDic.TabIndex = 1;
            _btnBrwDic.Text = "Location";
            _btnBrwDic.UseVisualStyleBackColor = true;
            _btnBrwDic.Click += btnBrwDic_Click;
            // 
            // fbdSelect
            // 
            _fbdSelect.Description = "Fastboot ROM";
            // 
            // btnRefresh
            // 
            _btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnRefresh.Location = new Point(831, 29);
            _btnRefresh.Name = "_btnRefresh";
            _btnRefresh.Size = new Size(75, 23);
            _btnRefresh.TabIndex = 2;
            _btnRefresh.Text = "Refresh";
            _btnRefresh.UseVisualStyleBackColor = true;
            _btnRefresh.Click += btnRefresh_Click;
            // 
            // btnFlash
            // 
            _btnFlash.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnFlash.Location = new Point(951, 28);
            _btnFlash.Name = "_btnFlash";
            _btnFlash.Size = new Size(75, 23);
            _btnFlash.TabIndex = 3;
            _btnFlash.Text = "flash";
            _btnFlash.UseVisualStyleBackColor = true;
            _btnFlash.Click += btnFlash_Click;
            // 
            // devicelist
            // 
            _devicelist.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                  | AnchorStyles.Left
                                                  | AnchorStyles.Right;
            _devicelist.Columns.AddRange(new[]
            {
                _clnID,
                _clnDevice,
                _clnProgress,
                _clnTime,
                _clnStatus,
                _clnResult
            });
            _devicelist.GridLines = true;
            _devicelist.Location = new Point(21, 86);
            _devicelist.Name = "_devicelist";
            _devicelist.Size = new Size(1005, 316);
            _devicelist.TabIndex = 4;
            _devicelist.UseCompatibleStateImageBehavior = false;
            _devicelist.View = View.Details;
            _devicelist.ColumnWidthChanging += devicelist_ColumnWidthChanging;
            // 
            // clnID
            // 
            _clnID.Text = "ID";
            // 
            // clnDevice
            // 
            _clnDevice.Text = "Kenzo";
            _clnDevice.Width = 90;
            // 
            // clnProgress
            // 
            _clnProgress.Text = "Progres";
            _clnProgress.Width = 107;
            // 
            // clnTime
            // 
            _clnTime.Text = "Waktu";
            // 
            // clnStatus
            // 
            _clnStatus.Text = "Status";
            _clnStatus.Width = 500;
            // 
            // clnResult
            // 
            _clnResult.Text = "Hasil";
            _clnResult.Width = 126;
            // 
            // txtLog
            // 
            _txtLog.Location = new Point(37, 421);
            _txtLog.Name = "_txtLog";
            _txtLog.Size = new Size(949, 65);
            _txtLog.TabIndex = 6;
            _txtLog.Text = "";
            _txtLog.Visible = false;
            // 
            // timer_updateStatus
            // 
            _timerUpdateStatus.Interval = 1000;
            _timerUpdateStatus.Tick += timer_updateStatus_Tick;
            // 
            // statusStrp
            // 
            _statusStrp.Items.AddRange(new ToolStripItem[]
            {
                _statusLblMsg,
                _statusTab,
                _rdoCleanAll,
                _rdoSaveUserData,
                _rdoCleanAllAndLock
            });
            _statusStrp.Location = new Point(0, 422);
            _statusStrp.Name = "_statusStrp";
            _statusStrp.Size = new Size(1094, 22);
            _statusStrp.TabIndex = 7;
            _statusStrp.Text = "statusStrip1";
            // 
            // statusLblMsg
            // 
            _statusLblMsg.Name = "_statusLblMsg";
            _statusLblMsg.Size = new Size(0, 17);
            // 
            // statusTab
            // 
            _statusTab.Name = "_statusTab";
            _statusTab.Size = new Size(708, 17);
            _statusTab.Spring = true;
            // 
            // rdoCleanAll
            // 
            _rdoCleanAll.IsChecked = false;
            _rdoCleanAll.Name = "_rdoCleanAll";
            _rdoCleanAll.Size = new Size(98, 20);
            _rdoCleanAll.Text = "Hapus Semua";
            // 
            // rdoSaveUserData
            // 
            _rdoSaveUserData.IsChecked = true;
            _rdoSaveUserData.Name = "_rdoSaveUserData";
            _rdoSaveUserData.Size = new Size(118, 20);
            _rdoSaveUserData.Text = "Simpan Data User";
            // 
            // rdoCleanAllAndLock
            // 
            _rdoCleanAllAndLock.IsChecked = false;
            _rdoCleanAllAndLock.Name = "_rdoCleanAllAndLock";
            _rdoCleanAllAndLock.Size = new Size(155, 20);
            _rdoCleanAllAndLock.Text = "Hapus Semua Dan Kunci";
            // 
            // lblMD5
            // 
            _lblMD5.AutoSize = true;
            _lblMD5.Location = new Point(96, 68);
            _lblMD5.Name = "_lblMD5";
            _lblMD5.Size = new Size(0, 13);
            _lblMD5.TabIndex = 8;
            // 
            // menuStrip1
            // 
            _menuStrip1.BackColor = SystemColors.ControlLight;
            _menuStrip1.GripStyle = ToolStripGripStyle.Visible;
            _menuStrip1.Items.AddRange(new ToolStripItem[]
            {
                _miConfiguration
            });
            _menuStrip1.Location = new Point(0, 0);
            _menuStrip1.Name = "_menuStrip1";
            _menuStrip1.RenderMode = ToolStripRenderMode.System;
            _menuStrip1.Size = new Size(1094, 24);
            _menuStrip1.TabIndex = 9;
            _menuStrip1.Text = "menuStrip1";
            // 
            // miConfiguration
            // 
            _miConfiguration.DropDownItems.AddRange(new ToolStripItem[]
            {
                _miFlashConfigurationToolStripMenuItem, _miInstallDrivers
            });
            _miConfiguration.Name = "_miConfiguration";
            _miConfiguration.Size = new Size(93, 40);
            _miConfiguration.Text = "Configuration";
            // 
            // miFlashConfigurationToolStripMenuItem
            // 
            _miFlashConfigurationToolStripMenuItem.Name = "_miFlashConfigurationToolStripMenuItem";
            _miFlashConfigurationToolStripMenuItem.Size = new Size(178, 22);
            _miFlashConfigurationToolStripMenuItem.Text = "Configure MiFlash";
            _miFlashConfigurationToolStripMenuItem.Click += miFlashConfigurationToolStripMenuItem_Click;
            //
            // miInstallDriver
            //
            _miInstallDrivers.Name = "_miInstallDrivers";
            _miInstallDrivers.Size = new Size(178, 22);
            _miInstallDrivers.Text = "Install Drivers";
            _miInstallDrivers.Click += miInstallDrivers_Click;
            //
            // MainFrm
            // 
            ClientSize = new Size(1094, 444);
            Controls.Add(_lblMD5);
            Controls.Add(_statusStrp);
            Controls.Add(_menuStrip1);
            Controls.Add(_txtLog);
            Controls.Add(_devicelist);
            Controls.Add(_btnFlash);
            Controls.Add(_btnRefresh);
            Controls.Add(_btnBrwDic);
            Controls.Add(_txtPath);
            MainMenuStrip = _menuStrip1;
            Name = "MainFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QFlashKit (Beta)";
            FormClosing += MainFrm_FormClosing;
            FormClosed += MainFrm_FormClosed;
            Load += MainFrm_Load;
            _statusStrp.ResumeLayout(false);
            _statusStrp.PerformLayout();
            _menuStrip1.ResumeLayout(false);
            _menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private bool IsRunAsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void CheckAdmin(bool admin)
        {
            if (!admin) Log.w("Not running as admin!");
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            SetLanguage();
            _txtPath.Text = MiAppConfig.Get("swPath");
        }

        private void btnBrwDic_Click(object sender, EventArgs e)
        {
            if (_fbdSelect.ShowDialog() != DialogResult.OK)
                return;
            _txtPath.Text = _fbdSelect.SelectedPath;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                var command = string.Format("pushd \"{0}\"&&prompt $$&&set PATH=\"{1}\";%PATH%&&\"{2}\" -s {3}&&popd",
                    "F:\\Dev\\mi\\MiFlash\\bin\\Debug\\images", Script.AndroidPath, "flash.bat", "djhgjhgvjh");
                Log.w("Command:" + command);
                _btnRefresh.Enabled = false;
                _btnRefresh.Cursor = Cursors.WaitCursor;
                var device = UsbDevice.GetDevice();
                var items = _devicelist.Items;
                foreach (var str in FlashingDevice.flashDeviceList.Where(d => d.IsDone.Value).Select(d => d.Name)
                    .ToList())
                {
                    foreach (var flashDevice in FlashingDevice.flashDeviceList)
                        if (flashDevice.Name == str)
                        {
                            FlashingDevice.flashDeviceList.Remove(flashDevice);
                            break;
                        }

                    foreach (ListViewItem listViewItem in items)
                        if (listViewItem.SubItems[1].Text == str)
                        {
                            items.Remove(listViewItem);
                            break;
                        }

                    foreach (Control control in (ArrangedElementCollection) _devicelist.Controls)
                        if (control.Name == str + "progressbar")
                        {
                            _devicelist.Controls.Remove(control);
                            break;
                        }
                }

                using (var enumerator = device.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var d = enumerator.Current;
                        if (FlashingDevice.flashDeviceList.Where(fd => fd.Name == d.Name).Select(fd => fd.Name)
                                .Count() == 0)
                        {
                            var num1 = _devicelist.Items.Count + 1;
                            var listViewItem = new ListViewItem(new string[6]
                                {num1.ToString(), d.Name, "", "0s", "", ""});
                            _devicelist.Items.Add(listViewItem);
                            d.ID = num1;
                            d.Progress = 0.0f;
                            d.IsDone = new bool?();
                            FlashingDevice.flashDeviceList.Add(d);
                            var num2 = 0.0f;
                            var rectangle = new Rectangle();
                            var progressBar = new ProgressBar();
                            rectangle = listViewItem.SubItems[2].Bounds;
                            rectangle.Width = _devicelist.Columns[2].Width;
                            progressBar.Parent = _devicelist;
                            progressBar.SetBounds(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                            progressBar.Value = (int) num2;
                            progressBar.Visible = true;
                            progressBar.Name = d.Name + "progressbar";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.w(ex.Message);
                var num = (int) MessageBox.Show(ex.Message);
            }
            finally
            {
                _btnRefresh.Enabled = true;
                _btnRefresh.Cursor = Cursors.Default;
            }
        }

        private void btnFlash_Click(object sender, EventArgs e)
        {
            var str = "";
            if (_rdoCleanAll.IsChecked)
                str = FlashType.CleanAll;
            else if (_rdoSaveUserData.IsChecked)
                str = FlashType.SaveUserData;
            else if (_rdoCleanAllAndLock.IsChecked)
                str = FlashType.CleanAllAndLock;
            _timerUpdateStatus.Enabled = true;
            try
            {
                foreach (var flashDevice in FlashingDevice.flashDeviceList)
                    if (!flashDevice.IsDone.HasValue || flashDevice.IsDone.Value)
                    {
                        flashDevice.StartTime = DateTime.Now;
                        flashDevice.Status = "flashing";
                        flashDevice.Progress = 0.0f;
                        flashDevice.IsDone = false;
                        flashDevice.IsUpdate = true;
                        // fungsi untuk flash

                        var deviceCtrl = flashDevice.DeviceCtrl;
                        deviceCtrl.deviceName = flashDevice.Name;
                        deviceCtrl.swPath = _txtPath.Text.Trim();
                        deviceCtrl.flashScript = str;
                        new Thread(deviceCtrl.flash)
                        {
                            IsBackground = true
                        }.Start();
                    }
            }
            catch (Exception ex)
            {
                var num = (int) MessageBox.Show(ex.Message);
            }
        }

        private void timer_updateStatus_Tick(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in _devicelist.Items)
            {
                listViewItem.UseItemStyleForSubItems = false;
                foreach (var flashDevice in FlashingDevice.flashDeviceList)
                    //jika device ingin update (isupdate) = true dan nama device sesuai dengan yang ada di table
                    if (flashDevice.IsUpdate && String.Equals(flashDevice.Name.ToLower(), listViewItem.SubItems[1].Text.ToLower(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        //
                        listViewItem.SubItems[2].Text = flashDevice.Progress * 100.0 + "%";
                        foreach (Control control in (ArrangedElementCollection) _devicelist.Controls)
                            if (control.Name == flashDevice.Name + "progressbar")
                            {
                                var progressBar = (ProgressBar) control;
                                if (progressBar.Value == (int) (flashDevice.Progress * 100.0))
                                {
                                    if ((int) (flashDevice.Progress * 100.0) < 100)
                                        flashDevice.Progress += 3f / 1000f;
                                    progressBar.Value = (int) (flashDevice.Progress * 100.0);
                                }
                                else
                                {
                                    progressBar.Value = (int) (flashDevice.Progress * 100.0);
                                }
                            }

                        if (flashDevice.StartTime > DateTime.MinValue)
                        {
                            var timeSpan = DateTime.Now.Subtract(flashDevice.StartTime);
                            listViewItem.SubItems[3].Text = string.Format("{0}s", timeSpan.TotalSeconds.ToString());
                        }

                        listViewItem.SubItems[4].Text = flashDevice.Status;
                        listViewItem.SubItems[5].Text = flashDevice.Result;
                        if (flashDevice.IsDone.HasValue && flashDevice.IsDone.Value ||
                            flashDevice.Status == "flash done")
                        {
                            flashDevice.IsUpdate = false;
                            flashDevice.IsDone = true;
                            listViewItem.SubItems[5].BackColor = Color.LightGreen;
                        }

                        if (flashDevice.IsDone.HasValue && flashDevice.IsDone.Value ||
                            flashDevice.Result.ToLower() == "success")
                        {
                            flashDevice.IsUpdate = false;
                            flashDevice.IsDone = true;
                            listViewItem.SubItems[5].BackColor = Color.LightGreen;
                            break;
                        }

                        if (flashDevice.Result.ToLower().IndexOf("error") < 0)
                            if (flashDevice.Result.ToLower().IndexOf("fail") < 0)
                                break;
                        flashDevice.IsUpdate = false;
                        flashDevice.IsDone = true;
                        listViewItem.SubItems[5].BackColor = Color.Red;
                        break;
                    }
            }

            if (FlashingDevice.flashDeviceList.Count != 0)
                return;
            _timerUpdateStatus.Enabled = false;
        }

        private void devicelist_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            var newWidth = e.NewWidth;
            foreach (Control control in (ArrangedElementCollection) _devicelist.Controls)
                if (control.Name.IndexOf("progressbar") >= 0)
                {
                    var progressBar = (ProgressBar) control;
                    var bounds = progressBar.Bounds;
                    bounds.Width = _devicelist.Columns[2].Width;
                    progressBar.SetBounds(_devicelist.Items[0].SubItems[2].Bounds.X, bounds.Y, bounds.Width,
                        bounds.Height);
                }
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MiAppConfig.SetValue("swPath", _txtPath.Text);
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Dispose();
        }

        public override void SetLanguage()
        {
            base.SetLanguage();
            if (CultureInfo.InstalledUICulture.Name.ToLower().IndexOf("zh") >= 0)
                LanID = LanguageType.eng;
            else
                LanID = LanguageType.eng;
            var languageProvider = new LanguageProvider(LanID);
            _btnBrwDic.Text = languageProvider.GetLanguage("MainFrm.btnBrwDic");
            _btnRefresh.Text = languageProvider.GetLanguage("MainFrm.btnRefresh");
            _btnFlash.Text = languageProvider.GetLanguage("MainFrm.btnFlash");
            _devicelist.Columns[0].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln0");
            _devicelist.Columns[1].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln1");
            _devicelist.Columns[2].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln2");
            _devicelist.Columns[3].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln3");
            _devicelist.Columns[4].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln4");
            _devicelist.Columns[5].Text = languageProvider.GetLanguage("MainFrm.devicelist.cln5");
            _rdoCleanAll.Text = languageProvider.GetLanguage("MainFrm.rdoCleanAll");
            _rdoSaveUserData.Text = languageProvider.GetLanguage("MainFrm.rdoSaveUserData");
            _rdoCleanAllAndLock.Text = languageProvider.GetLanguage("MainFrm.rdoCleanAllAndLock");
        }

        private void miFlashConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConfigurationFrm().Show();
        }

        private void miInstallDrivers_Click(object sender, EventArgs e)
        {
            new DriverFrm().Show();
        }
    }
}