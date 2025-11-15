namespace MagikaNet;

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.CompilerServices;

public static class NativeResolver
{
    [ModuleInitializer]
    public static void Init()
    {
        NativeLibrary.SetDllImportResolver(typeof(NativeResolver).Assembly, Resolver);
    }
    
    private static IntPtr Resolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        // Console.WriteLine($"Resolving native library: {libraryName} in runtime {RuntimeInformation.RuntimeIdentifier}");
    
        // prefer full path in app folder (adjust subpath as you packaged it)
        string baseDir = AppContext.BaseDirectory;
        var fullDir = Path.Combine(baseDir, "runtimes", RuntimeInformation.RuntimeIdentifier ?? "", "native",
            libraryName);

        if (File.Exists(fullDir))
        {
            if (NativeLibrary.TryLoad(fullDir, out var handle))
                return handle;
        }
    
        // fall back to default resolution
        return IntPtr.Zero;
    }
}