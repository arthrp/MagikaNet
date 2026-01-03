using System.Runtime.InteropServices;
using System.Text.Json;

namespace MagikaNet;

public sealed class MagikaClient : IDisposable
{
    private IntPtr _handle;

    public MagikaClient()
    {
        _handle = NativeMagika.NewSession();
        if (_handle == IntPtr.Zero) throw new InvalidOperationException("Failed to init Magika.");
    }

    /// <summary>
    /// Identify file from path and return json result as a string
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public string DetectPathJson(string path)
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

    /// <summary>
    /// Identify file from path and return object with results
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public DetectionResult? DetectPath(string path)
    {
        var json = DetectPathJson(path);
        var result = JsonSerializer.Deserialize<DetectionResult>(json);
        return result;
    }

    /// <summary>
    /// Identify file from bytes and return json result as a string
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public string DetectBytesJson(byte[] arr)
    {
        var sPtr = NativeMagika.DetectBytesJson(_handle, arr, arr.Length);
        
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

    /// <summary>
    /// Identify file from bytes and return object with results
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public DetectionResult? DetectBytes(byte[] arr)
    {
        var json = DetectBytesJson(arr);
        var result = JsonSerializer.Deserialize<DetectionResult>(json);
        return result;
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