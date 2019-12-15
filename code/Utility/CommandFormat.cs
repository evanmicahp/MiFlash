using System;
using System.Runtime.InteropServices;

namespace QFlashKit.code.Utility
{
    public class CommandFormat
    {
        public static byte[] StructToBytes(object structObj)
        {
            var length = 48;
            var destination = new byte[length];
            var num = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(structObj, num, false);
            Marshal.Copy(num, destination, 0, length);
            Marshal.FreeHGlobal(num);
            for (var index = 20; index < destination.Length; ++index)
                destination[index] = 0;
            return destination;
        }

        public static byte[] StructToBytes(object structObj, int length)
        {
            var length1 = length;
            var destination = new byte[length1];
            var num = Marshal.AllocHGlobal(length1);
            Marshal.StructureToPtr(structObj, num, false);
            Marshal.Copy(num, destination, 0, length1);
            Marshal.FreeHGlobal(num);
            for (var index = 20; index < destination.Length; ++index)
                destination[index] = 0;
            return destination;
        }

        public static object BytesToStuct(byte[] bytes, Type type)
        {
            var num1 = Marshal.SizeOf(type);
            if (num1 > bytes.Length)
                return null;
            var num2 = Marshal.AllocHGlobal(num1);
            Marshal.Copy(bytes, 0, num2, num1);
            var structure = Marshal.PtrToStructure(num2, type);
            Marshal.FreeHGlobal(num2);
            return structure;
        }
    }
}