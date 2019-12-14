using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;
using QFlashKit.code.Utility;
using QFlashKit;

namespace QFlashKit.form
{
    public class DriverFrm : Form
    {
        private MiInstaller installer;
        private IContainer component;
        private GroupBox driversBox;
        private Button installBtn;

        public DriverFrm()
        {
            InitializeComponent();
            installer.eventInstall += DriverFrm_Load;
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
            driversBox.Controls.Add(this.installBtn);
            driversBox.Location = new System.Drawing.Point(32, 26);
            driversBox.Name = "driversBox";
            driversBox.Size = new System.Drawing.Size(417, 266);
            driversBox.TabIndex = 0;
            driversBox.TabStop = false;
            driversBox.Text = "Install Drivers";
            //
            // installBtn
            //
            installBtn.Location = new System.Drawing.Point(332, 233);
            installBtn.Name = "installBtn";
            installBtn.Size = new System.Drawing.Size(80, 27);
            installBtn.TabIndex = 1;
            installBtn.Text = "Install";
            installBtn.UseVisualStyleBackColor = true;
            installBtn.Click += new EventHandler(ConfirmInstall);
            //
            // Form1
            //
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            ClientSize = new System.Drawing.Size(489, 326);
            Controls.Add(this.driversBox);
            Name = "Form1";
            Text = "Install Drivers";
            Load += new System.EventHandler(this.DriverFrm_Load);
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