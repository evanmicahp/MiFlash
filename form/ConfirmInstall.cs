using System;
using System.ComponentModel;
using System.Windows.Forms;
using XiaoMiFlash.form;

namespace XiaoMiFlash.form
{
    public class ConfirmInstallFrm : Form
    {
        public ConfirmInstallFrm()
        {
            InitializeComponent();
        }
        private IContainer components = null;

        private void InitializeComponent()
        {
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            //
            // button1
            //
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            button1.Location = new System.Drawing.Point(164, 64);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(85, 29);
            button1.TabIndex = 0;
            button1.Text = "Install";
            button1.UseVisualStyleBackColor = true;
            //
            // button2
            //
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            button2.Location = new System.Drawing.Point(255, 64);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(85, 29);
            button2.TabIndex = 1;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += CloseWindow;
            //
            // label1
            //
            label1.Location = new System.Drawing.Point(12, 18);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(292, 33);
            label1.TabIndex = 2;
            label1.Text = "Install drivers?";
            //
            // ConfirmInstall
            //
            AcceptButton = button2;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button1;
            ClientSize = new System.Drawing.Size(352, 105);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "ConfirmInstall";
            Text = "Confirm install";
            ResumeLayout(false);
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}