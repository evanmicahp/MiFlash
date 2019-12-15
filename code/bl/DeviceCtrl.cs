namespace QFlashKit.code.bl
{
    public abstract class DeviceCtrl
    {
        public string deviceName = "";
        public string flashScript;
        public string swPath = "D:\\SW\\A1\\FDL153I\\images\\";

        public abstract void flash();

        public abstract string[] getDevice();
    }
}