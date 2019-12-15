using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QFlashKit.form
{
    public class DriverFrm : Form
    {
        private IContainer component;
        private GroupBox driversBox;
        private Button installBtn;
        private MiInstaller installer;

        public DriverFrm()
        {
            InitializeComponent();
            installer.EventInstall += DriverFrm_Load;
        }

        private void InitializeComponent()
        {
            driversBox = new GroupBox();
            installBtn = new Button();
            SuspendLayout();
            driversBox.Text = "Install Drivers";
            driversBox.FlatStyle = FlatStyle.Flat;
            driversBox.Controls.Add(installBtn);
            Controls.Add(driversBox);
            installer = new MiInstaller();
            component = installer.Container;
            //
            // driversBox
            //
            driversBox.Controls.Add(installBtn);
            driversBox.Location = new Point(32, 26);
            driversBox.Name = "driversBox";
            driversBox.Size = new Size(417, 266);
            driversBox.TabIndex = 0;
            driversBox.TabStop = false;
            driversBox.Text = "Install Drivers";
            //
            // installBtn
            //
            installBtn.Location = new Point(332, 233);
            installBtn.Name = "installBtn";
            installBtn.Size = new Size(80, 27);
            installBtn.TabIndex = 1;
            installBtn.Text = "Install";
            installBtn.UseVisualStyleBackColor = true;
            installBtn.Click += ConfirmInstall;
            //
            // Form1
            //
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(489, 326);
            Controls.Add(driversBox);
            Name = "Form1";
            Text = "Install Drivers";
            Load += DriverFrm_Load;
            driversBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void ConfirmInstall(object sender, EventArgs e)
        {
            new ConfirmInstallFrm().Show();
        }

        private void event_DriverInstall(object sender, EventArgs e)
        {
            installer.CopyInstallDrivers();
        }

        private void DriverFrm_Load(object sender, EventArgs e)
        {
        }
    }
}