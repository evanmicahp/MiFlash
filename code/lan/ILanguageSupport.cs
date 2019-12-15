namespace QFlashKit.code.lan
{
    public interface ILanguageSupport
    {
        string LanID { get; set; }

        void SetLanguage();
    }
}