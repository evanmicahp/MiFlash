using System;
using QFlashKit.code.bl;

namespace QFlashKit.code.module
{
    public class Device
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public float Progress { get; set; }

        public DateTime StartTime { get; set; }

        public float Elapse { get; set; }

        public string Status { get; set; } = "";

        public string Result { get; set; } = "";

        public bool? IsDone { get; set; }

        public bool IsUpdate { get; set; }

        public DeviceCtrl DeviceCtrl { get; set; }
    }
}