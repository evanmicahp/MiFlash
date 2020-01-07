using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QFlashKit;

namespace QFlashKit.form
{
    public class DriverFrm : Form
    {
        private ListBox _driverListBox = new ListBox();
        private IContainer component;
        private GroupBox _driversBox;
        private Button _installBtn;
        private static MiInstaller _installer;
        private List<String> _driverName = new List<string>();

        public DriverFrm()
        {
            InitializeComponent();
            _installer.EventInstall += DriverFrm_Load;
        }

        private void InitializeComponent()
        {
            _driversBox = new GroupBox();
            _installBtn = new Button();
            SuspendLayout();
            _driversBox.Text = "Install Drivers";
            _driversBox.FlatStyle = FlatStyle.Flat;
            _driversBox.Controls.Add(_installBtn);
            Controls.Add(_driversBox);
            _installer = new MiInstaller();
            component = _installer.Container;
            //
            // driversBox
            //
            _driversBox.Controls.Add(_installBtn);
            _driversBox.Location = new Point(32, 26);
            _driversBox.Name = "_driversBox";
            _driversBox.Size = new Size(417, 266);
            _driversBox.TabIndex = 0;
            _driversBox.TabStop = false;
            _driversBox.Text = "Install Drivers";
            //
            // installBtn
            //
            _installBtn.Location = new Point(332, 233);
            _installBtn.Name = "_installBtn";
            _installBtn.Size = new Size(80, 27);
            _installBtn.TabIndex = 1;
            _installBtn.Text = "Install";
            _installBtn.UseVisualStyleBackColor = true;
            _installBtn.Click += ConfirmInstall;
            //
            // driverNames
            //
            _driverListBox.DataSource = _driverName; // TODO fix exception
            //
            // Form1
            //
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(489, 326);
            Controls.Add(_driversBox);
            Name = "Form1";
            Text = "Install Drivers";
            Load += DriverFrm_Load;
            _driversBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private void ConfirmInstall(object sender, EventArgs e)
        {
            new ConfirmInstallFrm().Show();
        }

        private void DriverFrm_Load(object sender, EventArgs e)
        {
            _driverName = _installer.GetDriverNames();
        }
    }
}