# MagikaNet

.NET bindings to [magika](https://github.com/google/magika) - AI-powered library for file type detection.

Sample usage:
```csharp
        using var m = new MagikaClient();

        var fileInfo = m.DetectPath("/tmp/file.jpg");
```