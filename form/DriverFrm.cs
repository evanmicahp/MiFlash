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
        private MiInstaller _installer = new MiInstaller();
        private List<String> _driverName = new List<string>();

        public DriverFrm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _driverName = _installer.GetDriverNames();
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
            _driversBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private void ConfirmInstall(object sender, EventArgs e)
        {
            // new ConfirmInstallFrm().Show();
            var confirmResult =
                MessageBox.Show("Install Drivers?", "Confirm Driver Install", MessageBoxButtons.OKCancel);
            if (confirmResult == DialogResult.OK)
            {
                _installer.CopyInstallDrivers();
                var confirmedBox = MessageBox.Show("Drivers Installed.", "Drivers Installed.", MessageBoxButtons.OK);
            }
        }
    }
}