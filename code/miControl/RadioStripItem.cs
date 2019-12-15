using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace QFlashKit.code.miControl
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip |
                                       ToolStripItemDesignerAvailability.ContextMenuStrip |
                                       ToolStripItemDesignerAvailability.StatusStrip)]
    public class RadioStripItem : ToolStripControlHost
    {
        private readonly RadioButton radio;

        public RadioStripItem()
            : base(new RadioButton())
        {
            radio = Control as RadioButton;
        }

        public bool IsChecked
        {
            get => radio.Checked;
            set => radio.Checked = value;
        }
    }
}