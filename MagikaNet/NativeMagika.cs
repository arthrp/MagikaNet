using System.Runtime.InteropServices;

namespace MagikaNet;

internal static class NativeMagika
{
    public const string LibName = "libmagika_ffi";
    
    [DllImport(LibName, EntryPoint = "magika_session_new", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr NewSession();
    
    [DllImport(LibName, EntryPoint = "magika_identify_path_json", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr DetectPathJson(IntPtr handle, string path);

    [DllImport(LibName, EntryPoint = "magika_identify_content_json", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr DetectBytesJson(IntPtr handle, byte[] arr, int len);
    
    [DllImport(LibName, EntryPoint = "magika_string_free", CallingConvention = CallingConvention.Cdecl)]
    public static extern void StringFree(IntPtr s);
    
    [DllImport(LibName, EntryPoint = "magika_session_free", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Free(IntPtr s);
}