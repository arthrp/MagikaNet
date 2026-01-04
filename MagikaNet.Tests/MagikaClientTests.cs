using System.Text;

namespace MagikaNet.Tests;

public class MagikaClientTests
{
    [Test]
    public void JpegFileJson_DetectedSuccessfully()
    {
        using var m = new MagikaClient();

        var d = m.DetectPathJson("samples/forest.jpg");
        
        Assert.That(d, Contains.Substring("\"mime_type\":\"image/jpeg\""));
    }

    [Test]
    public void JpegFile_DetectedSuccessfully()
    {
        using var m = new MagikaClient();

        var r = m.DetectPath("samples/forest.jpg");
        
        Assert.That(r, Is.Not.Null);
        Assert.That(r.Status, Is.EqualTo("ok"));
        Assert.That(r.FileType, Is.EqualTo("file"));
        Assert.That(r.Value.Output.Label, Is.EqualTo("jpeg"));
        Assert.That(r.Value.Output.MimeType, Is.EqualTo("image/jpeg"));
    }

    [Test]
    public void ShellBytesJson_DetectedSuccessfully()
    {
        using var m = new MagikaClient();

        var str = "#!/bin/sh\necho hello\n";
        var d = m.DetectBytesJson(Encoding.UTF8.GetBytes(str));
        
        Assert.That(d, Contains.Substring("\"label\":\"shell\""));
    }

    [Test]
    public void ShellBytes_DetectedSuccessfully()
    {
        using var m = new MagikaClient();

        var str = "#!/bin/sh\necho hello\n";
        var r = m.DetectBytes(Encoding.UTF8.GetBytes(str));
        
        Assert.That(r, Is.Not.Null);
        Assert.That(r.Status, Is.EqualTo("ok"));
        Assert.That(r.FileType, Is.EqualTo("file"));
        Assert.That(r.Value.Output.Label, Is.EqualTo("shell"));
    }

    [Test]
    public void EmptyBytes_ReturnsEmptyOutput()
    {
        using var m = new MagikaClient();
        byte[] bytes = { };

        var d = m.DetectBytesJson(bytes);
        Assert.That(d, Contains.Substring("label\":\"empty"));
    }
}