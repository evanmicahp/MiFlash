namespace QFlashKit.code.module
{
    public class Firehose
    {
        private static readonly string _reset_to_edl =
            "<?xml version=\"1.0\" ?><data><power value=\"reset_to_edl\"/></data>";

        private static readonly string _set_boot_partition =
            "<?xml version=\"1.0\" ?><data><setbootablestoragedrive value=\"1\"/></data>";

        private static readonly string _nop = "<?xml version=\"1.0\" ?><data><nop value=\"ping\"/></data>";

        private static readonly string _configure =
            "<?xml version=\"1.0\" ?><data><configure ZlpAwareHost=\"1\" MaxPayloadSizeToTargetInBytes=\"{0}\" MemoryName=\"{1}\" SkipStorageInit=\"{2}\"/></data>";

        public static string Reset_To_Edl => _reset_to_edl;

        public static string SetBootPartition => _set_boot_partition;

        public static string Nop => _nop;

        public static string Configure => _configure;

        public static int payload_size => 1048576;

        public static int MAX_PATCH_VALUE_LEN => 50;

        public static string FIREHOSE_PROGRAM =>
            "<?xml version=\"1.0\" ?><data><program SECTOR_SIZE_IN_BYTES=\"{0}\" num_partition_sectors=\"{1}\" start_sector=\"{2}\" physical_partition_number=\"{3}\"/></data>";

        public static string FIREHOSE_PATCH =>
            "<?xml version=\"1.0\" ?><data><patch SECTOR_SIZE_IN_BYTES=\"{0}\" byte_offset=\"{1}\" filename=\"DISK\" physical_partition_number=\"{2}\" size_in_bytes=\"{3}\" start_sector=\"{4}\" value=\"{5}\" what=\"Update\"/></data>";
    }
}