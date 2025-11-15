using System.Runtime.InteropServices;

namespace MagikaNet;

internal static class NativeMagika
{
    const string Lib =
#if Windows
        "magika_ffi";
#elif Macos
        "libmagika_ffi.dylib";
#else
        "libmagika_ffi.so";
#endif
    
    [DllImport(Lib, EntryPoint = "magika_session_new", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr NewSession();
    
    [DllImport(Lib, EntryPoint = "magika_identify_path_json", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr DetectPathJson(IntPtr handle, string path);
    
    [DllImport(Lib, EntryPoint = "magika_string_free", CallingConvention = CallingConvention.Cdecl)]
    public static extern void StringFree(IntPtr s);
    
    [DllImport(Lib, EntryPoint = "magika_session_free", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Free(IntPtr s);
}

public sealed class MagikaClient : IDisposable
{
    private IntPtr _handle;

    public MagikaClient()
    {
        // _h = NativeMagika.Create(modelsDir);
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