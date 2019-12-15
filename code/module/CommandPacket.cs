namespace QFlashKit.code.module
{
    public class CommandPacket
    {
        public CommandPacket()
        {
        }

        public CommandPacket(byte[] arr)
        {
            if (arr.Length < 48)
                return;
            for (var index = 0; index < arr.Length; ++index)
                if (index % 4 == 0)
                    switch (index)
                    {
                        case 12:
                            VersionCompatible = arr[index];
                            continue;
                        case 16:
                            CommandPacketLengthgth = arr[index];
                            continue;
                        case 20:
                            Mode = arr[index];
                            continue;
                        case 0:
                            Command = arr[index];
                            continue;
                        case 4:
                            Length = arr[index];
                            continue;
                        case 8:
                            VersionNumber = arr[index];
                            continue;
                        default:
                            continue;
                    }
        }

        public int Command { get; set; }

        public int Length { get; set; }

        public int VersionNumber { get; set; }

        public int VersionCompatible { get; set; }

        public int CommandPacketLengthgth { get; set; }

        public int Mode { get; set; }
    }
}