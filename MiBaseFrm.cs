

using System.Windows.Forms;
using QFlashKit.code.lan;

namespace QFlashKit
{
  public class MiBaseFrm : Form, ILanguageSupport
  {
    private string lanid = "";

    public string LanID
    {
      get
      {
        return this.lanid;
      }
      set
      {
        this.lanid = value;
      }
    }

    public virtual void SetLanguage()
    {
    }
  }
}
