using System;
using System.Drawing;
using System.Windows.Forms;

namespace QFlashKit.form
{
    public class DriverInstalledDialog : Form
    {
        private Button _okbutton;
        private Label _label1;
        public bool _done;

        public DriverInstalledDialog()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            _okbutton = new Button();
            _label1 = new Label();
            SuspendLayout();

            _okbutton.DialogResult = DialogResult.OK;
            _okbutton.Location = new Point(164, 64);
            _okbutton.Name = "_okbutton";
            _okbutton.Size = new Size(85, 29);
            _okbutton.TabIndex = 0;
            _okbutton.Text = "Okay";
            _okbutton.UseVisualStyleBackColor = true;
            _okbutton.Click += FrmClose;

            _label1.Location = new Point(12, 18);
            _label1.Name = "_label1";
            _label1.Size = new Size(292, 33);
            _label1.TabIndex = 2;
            _label1.Text = "Drivers installed.";

            AcceptButton = _okbutton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(352, 105);
            Controls.Add(_label1);
            Controls.Add(_okbutton);
            Name = "DriverInstalledDialog";
            Text = "Drivers Installed";
        }

        private void FrmClose(object sender, EventArgs e)
        {
            _done = true;
            Close();
        }
    }
}