using System.Runtime.InteropServices;

namespace MagikaNet;

public sealed class MagikaClient : IDisposable
{
    private IntPtr _handle;

    public MagikaClient()
    {
        _handle = NativeMagika.NewSession();
        if (_handle == IntPtr.Zero) throw new InvalidOperationException("Failed to init Magika.");
    }

    public string DetectPath(string path)
    {
        var sPtr = NativeMagika.DetectPathJson(_handle, path);
        if (sPtr == IntPtr.Zero) throw new InvalidOperationException("Detection failed.");
        try
        {
            var result = Marshal.PtrToStringUTF8(sPtr)!;
            return result;
        }
        finally
        {
            NativeMagika.StringFree(sPtr);
        }
    }

    public void Dispose()
    {
        if (_handle != IntPtr.Zero)
        {
            NativeMagika.Free(_handle);
            _handle = IntPtr.Zero;
        }
    }
}