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
        string libFileName;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            libFileName = "libmagika_ffi.dylib";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            libFileName = "libmagika_ffi.so";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && RuntimeInformation.ProcessArchitecture == Architecture.X64)
            libFileName = "magika_ffi.dll";
        else
            throw new PlatformNotSupportedException();
    
        // prefer full path in app folder (adjust subpath as you packaged it)
        string baseDir = AppContext.BaseDirectory;
        var fullDir = Path.Combine(baseDir, "runtimes", RuntimeInformation.RuntimeIdentifier ?? "", "native",
            libFileName);
        
        // Console.WriteLine("Trying "+fullDir);

        //Prefer the one bundled
        if (File.Exists(fullDir))
        {
            if (NativeLibrary.TryLoad(fullDir, out var handle))
                return handle;
        }
        
        //Try other places
        if (NativeLibrary.TryLoad(libFileName, out var globalHandle))
            return globalHandle;
    
        // fall back to default resolution
        return IntPtr.Zero;
    }
}