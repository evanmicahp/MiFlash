using System.Windows.Forms;
using QFlashKit.code.lan;

namespace QFlashKit
{
    public class MiBaseFrm : Form, ILanguageSupport
    {
        public string LanID { get; set; } = "";

        public virtual void SetLanguage()
        {
        }
    }
}