# MagikaNet

.NET wrapper for [magika](https://github.com/google/magika) - AI-powered library for file type detection. Powered by [magika_ffi](https://github.com/arthrp/magika_ffi) that exposes magika functions via FFI.

## Features

* Batteries included - binaries are provided for all major platforms (linux_x64, win_x64, macos_arm64). 
* Tests

Sample usage:
```csharp
        using var m = new MagikaClient();

        var fileInfo = m.DetectPath("/tmp/file.jpg");
```
