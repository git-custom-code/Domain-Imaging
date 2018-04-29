namespace CustomCode.Domain.Imaging.Memory
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ColorChannelRowEnumerator<T> : IEnumerator<T>
        where T : struct, IComparable, IConvertible, IFormattable
    {
        public ColorChannelRowEnumerator(byte channelIndex, uint rowIndex, IImageMemoryBuffer buffer)
        {
            var start = (int)(channelIndex * buffer.SizePerChannel + rowIndex * buffer.SizePerAlignedRow);
            var length = (int)buffer.SizePerChannel;
            Memory = new Memory<byte>(buffer.AsArray(), start, length);
            RowLength = buffer.SizePerAlignedRow - buffer.Stride;
        }

        private Memory<byte> Memory { get; }

        private uint Index { get; set; }

        private uint RowLength { get; set; }

        public T Current { get; private set; }
        
        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        { }


        public bool MoveNext()
        {
            if (Index < RowLength)
            {
                var span = MemoryMarshal.Cast<byte, T>(Memory.Span);
                Current = span[(int)Index];
                ++Index;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            Index = 0;
            Current = default(T);
        }
    }
}