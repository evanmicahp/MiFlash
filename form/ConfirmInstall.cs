using System;
using System.Drawing;
using System.Windows.Forms;
using QFlashKit;

// TODO localize strings

namespace QFlashKit.form
{
    public class ConfirmInstallFrm : Form
    {
        private Button _button1;
        private Button _button2;
        private Label _label1;
        private MiInstaller _installer = new MiInstaller();

        public ConfirmInstallFrm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _button1 = new Button();
            _button2 = new Button();
            _label1 = new Label();
            SuspendLayout();
            //
            // _button1
            //
            _button1.DialogResult = DialogResult.OK;
            _button1.Location = new Point(164, 64);
            _button1.Name = "_button1";
            _button1.Size = new Size(85, 29);
            _button1.TabIndex = 0;
            _button1.Text = "Install";
            _button1.UseVisualStyleBackColor = true;
            _button1.Click += InstallDrivers;
            //
            // _button2
            //
            _button2.DialogResult = DialogResult.Cancel;
            _button2.Location = new Point(255, 64);
            _button2.Name = "_button2";
            _button2.Size = new Size(85, 29);
            _button2.TabIndex = 1;
            _button2.Text = "Cancel";
            _button2.UseVisualStyleBackColor = true;
            _button2.Click += CloseWindow;
            //
            // _label1
            //
            _label1.Location = new Point(12, 18);
            _label1.Name = "_label1";
            _label1.Size = new Size(292, 33);
            _label1.TabIndex = 2;
            _label1.Text = "Install drivers?";
            //
            // ConfirmInstall
            //
            AcceptButton = _button2;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _button1;
            ClientSize = new Size(352, 105);
            Controls.Add(_label1);
            Controls.Add(_button2);
            Controls.Add(_button1);
            Name = "ConfirmInstall";
            Text = "Confirm install";
            ResumeLayout(false);
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }

        private void InstallDrivers(object sender, EventArgs e)
        {
            _installer.CopyInstallDrivers();
            new DriverInstalledDialog().Show();
        }
    }
}